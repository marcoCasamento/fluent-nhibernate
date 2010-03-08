using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using FluentNHibernate.Mapping.Providers;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.MappingModel.Identity;
using FluentNHibernate.Utils;
using NHibernate.Persister.Entity;

namespace FluentNHibernate.Mapping
{
    public enum Attr
    {
        Name,
        Lazy,
        Access,
        Cascade,
        IdType,
        Insert,
        Update,
        OptimisticLock,
        Usage,
        Region,
        Include,
        Polymorphism,
        SchemaAction,
        DiscriminatorValue,
        Schema,
        Table,
        Mutable,
        DynamicUpdate,
        DynamicInsert,
        BatchSize,
        Check,
        Persister,
        Proxy,
        SelectBeforeUpdate,
        Where,
        Subselect,
        EntityName,
        Parent,
        Unique,
        Mapped,
        UnsavedValue,
        Force,
        Formula,
        Precision,
        Length,
        Scale,
        NotNull,
        UniqueKey,
        Index,
        Default,
        Type,
        DefaultCascade,
        DefaultAccess,
        AutoImport,
        DefaultLazy,
        Catalog,
        Namespace,
        Assembly,
        Generator,
        SqlType,
        Rename,
        Class,
        Abstract,
        Optional,
        Inverse,
        Key,
        Fetch,
        ForeignKey,
        NotFound,
        OrderBy,
        PropertyRef,
        MetaType,
        Version,
        EntityType,
        Id,
        Discriminator,
        Cache,
        ChildType,
        CollectionType,
        CompositeElement,
        Element,
        Generic,
        Relationship,
        Nullable,
        Extends,
        OnDelete,
        ParentType,
        Sort,
        Value,
        Constrained,
        Generated,
        Tuplizer,
        Condition,
        SPType,
        Query,
        Mode
    }

    public interface IMappingStructure
    {
        IEnumerable<KeyValuePair<Attr, object>> Values { get; }
        IEnumerable<IMappingStructure> Children { get; }
        void AddChild(IMappingStructure child);
        void SetValue(Attr key, object value);
        void RemoveChildrenMatching(Predicate<IMappingStructure> predicate);
    }

    public interface IMappingStructure<T> : IMappingStructure
        where T : IMapping // ????
    {
        T CreateMappingNode(IMappingFactory factory);
    }

    public abstract class MappingStructure<T> : IMappingStructure<T>
        where T : IMapping
    {
        readonly Dictionary<Attr, object> values = new Dictionary<Attr, object>();
        readonly List<IMappingStructure> children = new List<IMappingStructure>();

        public abstract T CreateMappingNode(IMappingFactory factory);
        
        public void SetValue(Attr key, object value)
        {
            values[key] = value;
        }

        public void RemoveChildrenMatching(Predicate<IMappingStructure> predicate)
        {
            children.RemoveAll(predicate);
        }

        public IEnumerable<KeyValuePair<Attr, object>> Values
        {
            get { return values; }
        }

        public IEnumerable<IMappingStructure> Children
        {
            get { return children.AsReadOnly(); }
        }

        public void AddChild(IMappingStructure child)
        {
            children.Add(child);
        }
    }

    public class TypeStructure<T> : MappingStructure<T>
        where T : IMapping, ITypeMapping
    {
        readonly Type type;

        public TypeStructure(Type type)
        {
            this.type = type;
        }

        public override T CreateMappingNode(IMappingFactory factory)
        {
            return factory.CreateTypeMapping<T>(type, Values);
        }
    }

    /// <summary>
    /// Structure that has no real category (key, discriminator...)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BucketStructure<T> : MappingStructure<T>
        where T : IMapping
    {
        public override T CreateMappingNode(IMappingFactory factory)
        {
            return factory.CreateMapping<T>(Values);
        }
    }

    public class MemberStructure<T> : MappingStructure<T>
        where T : IMapping, IMemberMapping
    {
        readonly Member member;

        public MemberStructure(Member member)
        {
            this.member = member;
        }

        public override T CreateMappingNode(IMappingFactory factory)
        {
            return factory.CreateMemberMapping<T>(member, Values);
        }
    }

    public class ColumnStructure : MappingStructure<ColumnMapping>
    {
        readonly IMappingStructure parent;

        public ColumnStructure(IMappingStructure parent)
        {
            this.parent = parent;
        }

        public override ColumnMapping CreateMappingNode(IMappingFactory factory)
        {
            return factory.CreateColumnMapping(Merge(parent.Values, Values));
        }

        /// <summary>
        /// Merges two key collections, overwriting any values in the left with ones from the
        /// right.
        /// </summary>
        private IEnumerable<KeyValuePair<Attr, object>> Merge(IEnumerable<KeyValuePair<Attr, object>> left, IEnumerable<KeyValuePair<Attr, object>> right)
        {
            var merged = new Dictionary<Attr, object>();

            left.Each(x => merged[x.Key] = x.Value);
            right.Each(x => merged[x.Key] = x.Value);

            return merged;
        }
    }

    public class DefaultMappingFactory : IMappingFactory
    {
        public T CreateMemberMapping<T>(Member member, IEnumerable<KeyValuePair<Attr, object>> values) where T : IMemberMapping
        {
            throw new NotImplementedException();
        }

        public T CreateTypeMapping<T>(Type type, IEnumerable<KeyValuePair<Attr, object>> values) where T : ITypeMapping
        {
            throw new NotImplementedException();
        }

        public ColumnMapping CreateColumnMapping(IEnumerable<KeyValuePair<Attr, object>> values)
        {
            throw new NotImplementedException();
        }

        public T CreateMapping<T>(IEnumerable<KeyValuePair<Attr, object>> values) where T : IMapping
        {
            throw new NotImplementedException();
        }
    }

    public interface IMappingFactory
    {
        T CreateMemberMapping<T>(Member member, IEnumerable<KeyValuePair<Attr, object>> values) where T : IMemberMapping;
        T CreateTypeMapping<T>(Type type, IEnumerable<KeyValuePair<Attr, object>> values) where T : ITypeMapping;
        ColumnMapping CreateColumnMapping(IEnumerable<KeyValuePair<Attr, object>> values);
        T CreateMapping<T>(IEnumerable<KeyValuePair<Attr, object>> values) where T : IMapping;
    } 

    public class ClassMap<T> : ClasslikeMapBase<T>, IMappingProvider
    {
        private readonly OptimisticLockBuilder<ClassMap<T>> optimisticLock;

        private readonly IList<ImportPart> imports = new List<ImportPart>();
        private bool nextBool = true;

        private readonly HibernateMappingPart hibernateMappingPart = new HibernateMappingPart();
        private readonly PolymorphismBuilder<ClassMap<T>> polymorphism;
        private SchemaActionBuilder<ClassMap<T>> schemaAction;
        protected TuplizerMapping tuplizerMapping;

        readonly IMappingStructure<ClassMapping> structure;
        IMappingStructure<CacheMapping> cacheStructure;

        public ClassMap()
            : this(new TypeStructure<ClassMapping>(typeof(T)))
        {}

        ClassMap(IMappingStructure<ClassMapping> structure)
            : base(structure)
        {
            this.structure = structure;

            optimisticLock = new OptimisticLockBuilder<ClassMap<T>>(this, value => structure.SetValue(Attr.OptimisticLock, value));
            polymorphism = new PolymorphismBuilder<ClassMap<T>>(this, value => structure.SetValue(Attr.Polymorphism, value));
            schemaAction = new SchemaActionBuilder<ClassMap<T>>(this, value => structure.SetValue(Attr.SchemaAction, value));
        }

        /// <summary>
        /// Specify caching for this entity.
        /// </summary>
        public CachePart Cache
        {
            get
            {
                if (cacheStructure == null)
                {
                    cacheStructure = new BucketStructure<CacheMapping>();
                    structure.AddChild(cacheStructure);
                }

                return new CachePart(cacheStructure);
            }
        }

        IUserDefinedMapping IMappingProvider.GetUserDefinedMappings()
        {
            return new FluentMapUserDefinedMappings(typeof(T), structure);
        }

        public HibernateMapping GetHibernateMapping()
        {
            var hibernateMapping = ((IHibernateMappingProvider)hibernateMappingPart).GetHibernateMapping();

            foreach (var import in imports)
                hibernateMapping.AddImport(import.GetImportMapping());

            return hibernateMapping;
        }

        public HibernateMappingPart HibernateMapping
        {
            get { return hibernateMappingPart; }
        }

        public virtual NaturalIdPart<T> NaturalId()
        {
            var natrualIdStructure = new BucketStructure<NaturalIdMapping>();
            var part = new NaturalIdPart<T>(natrualIdStructure);

            structure.AddChild(natrualIdStructure);

            return part;
        }

        public virtual CompositeIdentityPart<T> CompositeId()
        {
            var compositeIdStructure = new BucketStructure<CompositeIdMapping>();
            var part = new CompositeIdentityPart<T>(compositeIdStructure);

            structure.AddChild(compositeIdStructure);

            return part;
        }

        public virtual CompositeIdentityPart<TId> CompositeId<TId>(Expression<Func<T, TId>> expression)
        {
            var compositeIdStructure = new MemberStructure<CompositeIdMapping>(expression.ToMember());
            var part = new CompositeIdentityPart<TId>(compositeIdStructure);

            structure.AddChild(compositeIdStructure);

            return part;
        }

        public VersionPart Version(Expression<Func<T, object>> expression)
        {
            return Version(expression.ToMember());
        }

        protected virtual VersionPart Version(Member property)
        {
            var versionStructure = new MemberStructure<VersionMapping>(property);
            var versionPart = new VersionPart(versionStructure);

            structure.AddChild(versionStructure);

            return versionPart;
        }

        public virtual DiscriminatorPart DiscriminateSubClassesOnColumn<TDiscriminator>(string columnName, TDiscriminator baseClassDiscriminator)
        {
            var discriminatorStructure = new BucketStructure<DiscriminatorMapping>();
            var part = new DiscriminatorPart(discriminatorStructure);

            part.Column(columnName);

            return part;
        }

        public virtual DiscriminatorPart DiscriminateSubClassesOnColumn<TDiscriminator>(string columnName)
        {
            var discriminatorStructure = new BucketStructure<DiscriminatorMapping>();
            var part = new DiscriminatorPart(discriminatorStructure);

            part.Column(columnName);

            return part;
        }

        public virtual DiscriminatorPart DiscriminateSubClassesOnColumn(string columnName)
        {
            return DiscriminateSubClassesOnColumn<string>(columnName);
        }

        public virtual IdentityPart<TReturn> Id<TReturn>(Expression<Func<T, TReturn>> expression)
        {
            return Id(expression, null);
        }

        public virtual IdentityPart<TReturn> Id<TReturn>(Expression<Func<T, TReturn>> expression, string column)
        {
            var idStructure = new MemberStructure<IdMapping>(expression.ToMember());
            var part = new IdentityPart<TReturn>(idStructure);

            if (column != null)
                part.Column(column);

            structure.AddChild(idStructure);
            
            return part;
        }

        public virtual IdentityPart<TReturn> Id<TReturn>(string column)
        {
            var idStructure = new BucketStructure<IdMapping>();
            var part = new IdentityPart<TReturn>(idStructure);
            
            if (column != null)
                part.Column(column);

            structure.AddChild(idStructure);

            return part;
        } 

        [Obsolete("Inline definitions of subclasses are depreciated. Please create a derived class from SubclassMap in the same way you do with ClassMap.")]
        public virtual void JoinedSubClass<TSubclass>(string keyColumn, Action<JoinedSubClassPart<TSubclass>> action) where TSubclass : T
        {
            var subclassStructure = new TypeStructure<SubclassMapping>(typeof(TSubclass));
            var subclass = new JoinedSubClassPart<TSubclass>(subclassStructure);

            subclass.KeyColumns.Add(keyColumn);

            action(subclass);

            structure.AddChild(subclassStructure);
        }

        /// <summary>
        /// Sets the hibernate-mapping schema for this class.
        /// </summary>
        /// <param name="schema">Schema name</param>
        public void Schema(string schema)
        {
            structure.SetValue(Attr.Schema, schema);
        }

        /// <summary>
        /// Sets the table for the class.
        /// </summary>
        /// <param name="tableName">Table name</param>
        public void Table(string tableName)
        {
            structure.SetValue(Attr.Table, tableName);
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
            structure.SetValue(Attr.Lazy, nextBool);
            nextBool = true;
        }

        /// <summary>
        /// Sets additional tables for the class via the NH 2.0 Join element.
        /// </summary>
        /// <param name="tableName">Joined table name</param>
        /// <param name="action">Joined table mapping</param>
        public void Join(string tableName, Action<JoinPart<T>> action)
        {
            var joinStructure = new BucketStructure<JoinMapping>();
            var join = new JoinPart<T>(joinStructure);

            join.Table(tableName);

            action(join);

            structure.AddChild(joinStructure);
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
            structure.SetValue(Attr.Mutable, !nextBool);
            nextBool = true;
        }

        /// <summary>
        /// Sets this entity to be dynamic update
        /// </summary>
        public void DynamicUpdate()
        {
            structure.SetValue(Attr.DynamicUpdate, nextBool);
            nextBool = true;
        }

        /// <summary>
        /// Sets this entity to be dynamic insert
        /// </summary>
        public void DynamicInsert()
        {
            structure.SetValue(Attr.DynamicInsert, nextBool);
            nextBool = true;
        }

        public ClassMap<T> BatchSize(int size)
        {
            structure.SetValue(Attr.BatchSize, size);
            return this;
        }

        /// <summary>
        /// Sets the optimistic locking strategy
        /// </summary>
        public OptimisticLockBuilder<ClassMap<T>> OptimisticLock
        {
            get { return optimisticLock; }
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
            structure.SetValue(Attr.Check, constraint);
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
            structure.SetValue(Attr.Persister, type);
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
            structure.SetValue(Attr.Proxy, type);
        }

        public void SelectBeforeUpdate()
        {
            structure.SetValue(Attr.SelectBeforeUpdate, nextBool);
            nextBool = true;
        }

		/// <summary>
		/// Defines a SQL 'where' clause used when retrieving objects of this type.
		/// </summary>
    	public void Where(string where)
    	{
            structure.SetValue(Attr.Where, where);
    	}

        /// <summary>
        /// Sets the SQL statement used in subselect fetching.
        /// </summary>
        /// <param name="subselectSql">Subselect SQL Query</param>
        public void Subselect(string subselectSql)
        {
            structure.SetValue(Attr.Subselect, subselectSql);
        }

        /// <summary>
        /// Specifies an entity-name.
        /// </summary>
        /// <remarks>See http://nhforge.org/blogs/nhibernate/archive/2008/10/21/entity-name-in-action-a-strongly-typed-entity.aspx</remarks>
        public void EntityName(string entityName)
        {
            structure.SetValue(Attr.EntityName, entityName);
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
            //filters.Add(part);
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