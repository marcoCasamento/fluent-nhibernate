using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using FluentNHibernate.Mapping.Providers;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.Utils;
using NHibernate.Persister.Entity;

namespace FluentNHibernate.Mapping
{
    public class ClassMap<T> : ClasslikeMapBase<T>, IMappingProvider
    {
        protected readonly AttributeStore attributes = new AttributeStore();
        private readonly IList<JoinMapping> joins = new List<JoinMapping>();
        private readonly OptimisticLockBuilder optimisticLock;

        /// <summary>
        /// Specify caching for this entity.
        /// </summary>
        public CachePart Cache { get; private set; }
        protected IIdentityMappingProvider id;

        private readonly IList<ImportPart> imports = new List<ImportPart>();
        private bool nextBool = true;

        protected DiscriminatorPart discriminator;
        protected IVersionMappingProvider version;
        protected ICompositeIdMappingProvider compositeId;
        private readonly HibernateMappingPart hibernateMappingPart = new HibernateMappingPart();
        private readonly PolymorphismBuilder<ClassMap<T>> polymorphism;
        private SchemaActionBuilder<ClassMap<T>> schemaAction;
        protected TuplizerMapping tuplizerMapping;

        public ClassMap()
        {
            optimisticLock = new OptimisticLockBuilder(value => attributes.Set(Attr.OptimisticLock, value));
            polymorphism = new PolymorphismBuilder<ClassMap<T>>(this, value => attributes.Set(Attr.Polymorphism, value));
            schemaAction = new SchemaActionBuilder<ClassMap<T>>(this, value => attributes.Set(Attr.SchemaAction, value));
            Cache = new CachePart(typeof(T));
        }

        ClassMapping IMappingProvider.GetClassMapping()
        {
		    var mapping = new ClassMapping(attributes.Clone(), typeof(T));

            foreach (var property in properties)
                mapping.AddProperty(property.GetPropertyMapping());

            foreach (var component in components)
                mapping.AddComponent(component.GetComponentMapping());

            if (version != null)
                mapping.Version = version.GetVersionMapping();

            foreach (var oneToOne in oneToOnes)
                mapping.AddOneToOne(oneToOne.GetOneToOneMapping());

            foreach (var collection in collections)
                mapping.AddCollection(collection.GetCollectionMapping());

            foreach (var reference in references)
                mapping.AddReference(reference.GetManyToOneMapping());

            foreach (var any in anys)
                mapping.AddAny(any.GetAnyMapping());

            foreach (var subclass in subclasses.Values)
                mapping.AddSubclass(subclass.GetSubclassMapping());

		    foreach (var join in joins)
		        mapping.AddJoin(join);

            if (discriminator != null)
                mapping.Discriminator = ((IDiscriminatorMappingProvider)discriminator).GetDiscriminatorMapping();

            if (Cache.IsDirty)
                mapping.Cache = ((ICacheMappingProvider)Cache).GetCacheMapping();

            if (id != null)
                mapping.Id = id.GetIdentityMapping();

            if (compositeId != null)
                mapping.Id = compositeId.GetCompositeIdMapping();

            foreach (var filter in filters)
                mapping.AddFilter(filter.GetFilterMapping());

            foreach (var storedProcedure in storedProcedures)
                mapping.AddStoredProcedure(storedProcedure.GetStoredProcedureMapping());

            mapping.Tuplizer = tuplizerMapping;

            return mapping;
        }

        public HibernateMapping GetHibernateMapping()
        {
            var hibernateMapping = ((IHibernateMappingProvider)hibernateMappingPart).GetHibernateMapping();

            foreach (var import in imports)
                hibernateMapping.AddImport(import.GetImportMapping());

            return hibernateMapping;
        }

        IEnumerable<string> IMappingProvider.GetIgnoredProperties()
        {
            return new string[0];
        }

        public HibernateMappingPart HibernateMapping
        {
            get { return hibernateMappingPart; }
        }

        public virtual CompositeIdentityPart<T> CompositeId()
        {
            var part = new CompositeIdentityPart<T>();

            compositeId = part;

            return part;
        }

        public virtual CompositeIdentityPart<TId> CompositeId<TId>(Expression<Func<T, TId>> expression)
        {
            var part = new CompositeIdentityPart<TId>(expression.ToMember().Name);

            compositeId = part;

            return part;
        }

        public VersionPart Version(Expression<Func<T, object>> expression)
        {
            return Version(expression.ToMember());
        }

        protected virtual VersionPart Version(Member property)
        {
            var versionPart = new VersionPart(typeof(T), property);

            version = versionPart;

            return versionPart;
        }

        public virtual DiscriminatorPart DiscriminateSubClassesOnColumn<TDiscriminator>(string columnName, TDiscriminator baseClassDiscriminator)
        {
            var part = new DiscriminatorPart(columnName, typeof(T), subclasses.Add, new TypeReference(typeof(TDiscriminator)));

            discriminator = part;

            attributes.Set(Attr.DiscriminatorValue, baseClassDiscriminator);

            return part;
        }

        public virtual DiscriminatorPart DiscriminateSubClassesOnColumn<TDiscriminator>(string columnName)
        {
            var part = new DiscriminatorPart(columnName, typeof(T), subclasses.Add, new TypeReference(typeof(TDiscriminator)));

            discriminator = part;

            return part;
        }

        public virtual DiscriminatorPart DiscriminateSubClassesOnColumn(string columnName)
        {
            return DiscriminateSubClassesOnColumn<string>(columnName);
        }

        public virtual IdentityPart Id(Expression<Func<T, object>> expression)
        {
            return Id(expression, null);
        }

        public virtual IdentityPart Id(Expression<Func<T, object>> expression, string column)
        {
            var member = expression.ToMember();
            var part = new IdentityPart(EntityType, member);

            if (column != null)
                part.Column(column);

            id = part;
            
            return part;
        }

        public virtual IdentityPart Id<TColumn>(string column)
        {
            var part = new IdentityPart(typeof(T), null, typeof(TColumn), column);
            
            if (column != null)
                part.Column(column);
            
            id = part;

            return part;
        } 

        [Obsolete("Inline definitions of subclasses are depreciated. Please create a derived class from SubclassMap in the same way you do with ClassMap.")]
        public virtual void JoinedSubClass<TSubclass>(string keyColumn, Action<JoinedSubClassPart<TSubclass>> action) where TSubclass : T
        {
            var subclass = new JoinedSubClassPart<TSubclass>(keyColumn);

            action(subclass);

            subclasses[typeof(TSubclass)] = subclass;
        }

        /// <summary>
        /// Sets the hibernate-mapping schema for this class.
        /// </summary>
        /// <param name="schema">Schema name</param>
        public void Schema(string schema)
        {
            attributes.Set(Attr.Schema, schema);
        }

        /// <summary>
        /// Sets the table for the class.
        /// </summary>
        /// <param name="tableName">Table name</param>
        public void Table(string tableName)
        {
            attributes.Set(Attr.Table, tableName);
        }

        /// <summary>
        /// Inverse next boolean
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public ClassMap<T> Not
        {
            get
            {
                nextBool = !nextBool;
                return this;
            }
        }

        /// <summary>
        /// Sets this entity to be lazy-loaded (overrides the default lazy load configuration).
        /// </summary>
        public void LazyLoad()
        {
            attributes.Set(Attr.Lazy, nextBool);
            nextBool = true;
        }

        /// <summary>
        /// Sets additional tables for the class via the NH 2.0 Join element.
        /// </summary>
        /// <param name="tableName">Joined table name</param>
        /// <param name="action">Joined table mapping</param>
        public void Join(string tableName, Action<JoinPart<T>> action)
        {
            var join = new JoinPart<T>(tableName);

            action(join);

            joins.Add(((IJoinMappingProvider)join).GetJoinMapping());
        }

        /// <summary>
        /// Imports an existing type for use in the mapping.
        /// </summary>
        /// <typeparam name="TImport">Type to import.</typeparam>
        public ImportPart ImportType<TImport>()
        {
            var part = new ImportPart(typeof(TImport));
            
            imports.Add(part);

            return part;
        }

        /// <summary>
        /// Set the mutability of this class, sets the mutable attribute.
        /// </summary>
        public void ReadOnly()
        {
            attributes.Set(Attr.Mutable, !nextBool);
            nextBool = true;
        }

        /// <summary>
        /// Sets this entity to be dynamic update
        /// </summary>
        public void DynamicUpdate()
        {
            attributes.Set(Attr.DynamicUpdate, nextBool);
            nextBool = true;
        }

        /// <summary>
        /// Sets this entity to be dynamic insert
        /// </summary>
        public void DynamicInsert()
        {
            attributes.Set(Attr.DynamicInsert, nextBool);
            nextBool = true;
        }

        public ClassMap<T> BatchSize(int size)
        {
            attributes.Set(Attr.BatchSize, size);
            return this;
        }

        /// <summary>
        /// Sets the optimistic locking strategy
        /// </summary>
        public OptimisticLockBuilder<ClassMap<T>> OptimisticLock
        {
            get { return new OptimisticLockBuilder<ClassMap<T>>(this, optimisticLock); }
        }

        public PolymorphismBuilder<ClassMap<T>> Polymorphism
        {
            get { return polymorphism; }
        }

        public SchemaActionBuilder<ClassMap<T>> SchemaAction
        {
            get { return schemaAction; }
        }

        public void CheckConstraint(string constraint)
        {
            attributes.Set(Attr.Check, constraint);
        }

        public void Persister<TPersister>() where TPersister : IEntityPersister
        {
            Persister(typeof(TPersister));
        }

        private void Persister(Type type)
        {
            Persister(type.AssemblyQualifiedName);
        }

        private void Persister(string type)
        {
            attributes.Set(Attr.Persister, type);
        }

        public void Proxy<TProxy>()
        {
            Proxy(typeof(TProxy));
        }

        public void Proxy(Type type)
        {
            Proxy(type.AssemblyQualifiedName);
        }

        public void Proxy(string type)
        {
            attributes.Set(Attr.Proxy, type);
        }

        public void SelectBeforeUpdate()
        {
            attributes.Set(Attr.SelectBeforeUpdate, nextBool);
            nextBool = true;
        }

		/// <summary>
		/// Defines a SQL 'where' clause used when retrieving objects of this type.
		/// </summary>
    	public void Where(string where)
    	{
            attributes.Set(Attr.Where, where);
    	}

        /// <summary>
        /// Sets the SQL statement used in subselect fetching.
        /// </summary>
        /// <param name="subselectSql">Subselect SQL Query</param>
        public void Subselect(string subselectSql)
        {
            attributes.Set(Attr.Subselect, subselectSql);
        }

        /// <summary>
        /// Specifies an entity-name.
        /// </summary>
        /// <remarks>See http://nhforge.org/blogs/nhibernate/archive/2008/10/21/entity-name-in-action-a-strongly-typed-entity.aspx</remarks>
        public void EntityName(string entityName)
        {
            attributes.Set(Attr.EntityName, entityName);
        }

        /// <overloads>
        /// Applies a named filter to this one-to-many.
        /// </overloads>
        /// <summary>
        /// Applies a named filter to this one-to-many.
        /// </summary>
        /// <param name="condition">The condition to apply</param>
        /// <typeparam name="TFilter">
        /// The type of a <see cref="FilterDefinition"/> implementation
        /// defining the filter to apply.
        /// </typeparam>
        public ClassMap<T> ApplyFilter<TFilter>(string condition) where TFilter : FilterDefinition, new()
        {
            var part = new FilterPart(new TFilter().Name, condition);
            filters.Add(part);
            return this;
        }

        /// <summary>
        /// Applies a named filter to this one-to-many.
        /// </summary>
        /// <typeparam name="TFilter">
        /// The type of a <see cref="FilterDefinition"/> implementation
        /// defining the filter to apply.
        /// </typeparam>
        public ClassMap<T> ApplyFilter<TFilter>() where TFilter : FilterDefinition, new()
        {
            return ApplyFilter<TFilter>(null);
        }

        public ClassMap<T> Tuplizer(TuplizerMode mode, Type tuplizerType)
        {
            tuplizerMapping = new TuplizerMapping();
            tuplizerMapping.Mode = mode;
            tuplizerMapping.Type = new TypeReference(tuplizerType);

            return this;
        }
    }
}