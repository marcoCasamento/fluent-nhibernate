using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using FluentNHibernate.Conventions;
using FluentNHibernate.Mapping.Providers;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.Utils;
using NHibernate.Persister.Entity;

namespace FluentNHibernate.Mapping
{
    public abstract class ToManyBase<T, TChild, TRelationshipAttributes> : ICollectionMappingProvider
        where T : ToManyBase<T, TChild, TRelationshipAttributes>, ICollectionMappingProvider
        where TRelationshipAttributes : ICollectionRelationshipMapping
    {
        protected readonly AccessStrategyBuilder access;
        protected readonly FetchBuilder fetch;
        protected readonly OptimisticLockBuilder optimisticLock;
        private readonly CascadeBuilder cascade;
        protected ElementPart elementPart;
        protected ICompositeElementMappingProvider componentMapping;
        protected bool nextBool = true;

        protected readonly AttributeStore collectionAttributes = new AttributeStore();
        protected readonly KeyMapping keyMapping = new KeyMapping();
        protected readonly AttributeStore relationshipAttributes = new AttributeStore();
        private readonly IList<FilterPart> filters = new List<FilterPart>();
        private Func<AttributeStore, ICollectionMapping> collectionBuilder;
        private IndexMapping indexMapping;
        protected Member member;
        private Type entity;

        protected ToManyBase(Type entity, Member member, Type type)
        {
            this.entity = entity;
            this.member = member;
            AsBag();
            access = new AccessStrategyBuilder(value => collectionAttributes.Set(Attr.Access, value));
            fetch = new FetchBuilder(value => collectionAttributes.Set(Attr.Fetch, value));
            optimisticLock = new OptimisticLockBuilder(value => collectionAttributes.Set(Attr.OptimisticLock, value));
            cascade = new CascadeBuilder(value => collectionAttributes.Set(Attr.Cascade, value));

            SetDefaultCollectionType(type);
            SetCustomCollectionType(type);
            Cache = new CachePart(entity);

            collectionAttributes.SetDefault(Attr.Name, member.Name);
            relationshipAttributes.SetDefault(Attr.Class, new TypeReference(typeof(TChild)));
        }

        private void SetDefaultCollectionType(Type type)
        {
            if (type.Namespace == "Iesi.Collections.Generic" || type.Closes(typeof(HashSet<>)))
                AsSet();
        }

        private void SetCustomCollectionType(Type type)
        {
            if (type.Namespace.StartsWith("Iesi") || type.Namespace.StartsWith("System") || type.IsArray)
                return;

            collectionAttributes.Set(Attr.CollectionType, new TypeReference(type));
        }

        public virtual ICollectionMapping GetCollectionMapping()
        {
            var mapping = collectionBuilder(collectionAttributes.Clone());

            mapping.ContainingEntityType = entity;
            mapping.ChildType = typeof(TChild);
            mapping.SetMember(member);
            mapping.Key = keyMapping;
            mapping.Key.ContainingEntityType = entity;
            mapping.Relationship = GetRelationship();

            if (Cache.IsDirty)
                mapping.Cache = ((ICacheMappingProvider)Cache).GetCacheMapping();

            if (componentMapping != null)
            {
                mapping.CompositeElement = componentMapping.GetCompositeElementMapping();
                mapping.Relationship = null; // HACK: bad design
            }

            // HACK: Index only on list and map - shouldn't have to do this!
            if (indexMapping != null && mapping is IIndexedCollectionMapping)
                ((IIndexedCollectionMapping)mapping).Index = indexMapping;

            if (elementPart != null)
            {
                mapping.Element = elementPart.GetElementMapping();
                mapping.Relationship = null;
            }

            foreach (var filterPart in Filters)
                mapping.Filters.Add(filterPart.GetFilterMapping());

            return mapping;
        }

        protected abstract ICollectionRelationshipMapping GetRelationship();

        /// <summary>
        /// Specify caching for this entity.
        /// </summary>
        public CachePart Cache { get; private set; }

        public T LazyLoad()
        {
            collectionAttributes.Set(Attr.Lazy, nextBool);
            nextBool = true;
            return (T)this;
        }

        public T Inverse()
        {
            collectionAttributes.Set(Attr.Inverse, nextBool);
            nextBool = true;
            return (T)this;
        }

        public CollectionCascadeBuilder<T> Cascade
        {
            get { return new CollectionCascadeBuilder<T>((T)this, cascade); }
        }

        public T AsSet()
        {
            collectionBuilder = attrs => new SetMapping(attrs);
            return (T)this;
        }

        public T AsSet(SortType sort)
        {
            collectionBuilder = attrs => new SetMapping(attrs) { Sort = sort.ToString().ToLowerInvariant() };
            return (T)this;
        }

        public T AsSet<TComparer>() where TComparer : IComparer<TChild>
        {
            collectionBuilder = attrs => new SetMapping(attrs) { Sort = typeof(TComparer).AssemblyQualifiedName };
            return (T)this;
        }

        public T AsBag()
        {
            collectionBuilder = attrs => new BagMapping(attrs);
            return (T)this;
        }

        public T AsList()
        {
            collectionBuilder = attrs => new ListMapping(attrs);
            CreateIndexMapping(null, "Index", typeof(int));

            return (T)this;
        }

        public T AsList(Action<IndexPart> customIndexMapping)
        {
            collectionBuilder = attrs => new ListMapping(attrs);
            CreateIndexMapping(customIndexMapping, "Index", typeof(int));

            return (T)this;
        }

        public T AsMap<TIndex>(Expression<Func<TChild, TIndex>> indexSelector)
        {
            return AsMap(indexSelector, null);
        }

        public T AsMap<TIndex>(Expression<Func<TChild, TIndex>> indexSelector, SortType sort)
        {
            return AsMap(indexSelector, null, sort);
        }

        public T AsMap(string indexColumnName)
        {
            collectionBuilder = attrs => new MapMapping(attrs);
            AsIndexedCollection<Int32>(indexColumnName, null);
            return (T)this;
        }

        public T AsMap(string indexColumnName, SortType sort)
        {
            collectionBuilder = attrs => new MapMapping(attrs) { Sort = sort.ToString().ToLowerInvariant() };
            AsIndexedCollection<Int32>(indexColumnName, null);
            return (T)this;
        }

        public T AsMap<TIndex>(string indexColumnName)
        {
            collectionBuilder = attrs => new MapMapping(attrs);
            AsIndexedCollection<TIndex>(indexColumnName, null);
            return (T)this;
        }

        public T AsMap<TIndex>(string indexColumnName, SortType sort)
        {
            collectionBuilder = attrs => new MapMapping(attrs) { Sort = sort.ToString().ToLowerInvariant() };
            AsIndexedCollection<TIndex>(indexColumnName, null);
            return (T)this;
        }

        public T AsMap<TIndex, TComparer>(string indexColumnName) where TComparer : IComparer<TChild>
        {
            collectionBuilder = attrs => new MapMapping(attrs) { Sort = typeof(TComparer).AssemblyQualifiedName };
            AsIndexedCollection<TIndex>(indexColumnName, null);
            return (T)this;
        }

        public T AsMap<TIndex>(Expression<Func<TChild, TIndex>> indexSelector, Action<IndexPart> customIndexMapping)
        {
            collectionBuilder = attrs => new MapMapping(attrs);
            return AsIndexedCollection(indexSelector, customIndexMapping);
        }

        public T AsMap<TIndex>(Expression<Func<TChild, TIndex>> indexSelector, Action<IndexPart> customIndexMapping, SortType sort)
        {
            collectionBuilder = attrs => new MapMapping(attrs) { Sort = sort.ToString().ToLowerInvariant() };
            return AsIndexedCollection(indexSelector, customIndexMapping);
        }

        // I'm not proud of this. The fluent interface for maps really needs to be rethought. But I've let maps sit unsupported for way too long
        // so a hack is better than nothing.
        public T AsMap<TIndex>(Action<IndexPart> customIndexMapping, Action<ElementPart> customElementMapping)
        {
            collectionBuilder = attrs => new MapMapping(attrs);
            AsIndexedCollection<TIndex>(string.Empty, customIndexMapping);
            Element(string.Empty);
            customElementMapping(elementPart);
            return (T)this;
        }

        public T AsArray<TIndex>(Expression<Func<TChild, TIndex>> indexSelector)
        {
            return AsArray(indexSelector, null);
        }

        public T AsArray<TIndex>(Expression<Func<TChild, TIndex>> indexSelector, Action<IndexPart> customIndexMapping)
        {
            collectionBuilder = attrs => new ArrayMapping(attrs);
            return AsIndexedCollection(indexSelector, customIndexMapping);
        }

        public T AsIndexedCollection<TIndex>(Expression<Func<TChild, TIndex>> indexSelector, Action<IndexPart> customIndexMapping)
        {
            var indexMember = indexSelector.ToMember();
            return AsIndexedCollection<TIndex>(indexMember.Name, customIndexMapping);
        }

        public T AsIndexedCollection<TIndex>(string indexColumn, Action<IndexPart> customIndexMapping)
        {
            CreateIndexMapping(customIndexMapping, indexColumn, typeof(TIndex));

            return (T)this;
        }

        private void CreateIndexMapping(Action<IndexPart> customIndex, string indexColumn, Type indexType)
        {
            var indexPart = new IndexPart(typeof(T), indexColumn, indexType);

            if (customIndex != null)
                customIndex(indexPart);

            indexMapping = indexPart.GetIndexMapping();
        }

        public ElementPart Element()
        {
            return (elementPart = new ElementPart(typeof(T), typeof(TChild)));
        }

        public T Element(string columnName)
        {
            elementPart = Element();

            if (!string.IsNullOrEmpty(columnName))
                elementPart.Column(columnName);

            return (T)this;
        }

        public T Element(string columnName, Action<ElementPart> customElementMapping)
        {
            Element(columnName);
            if (customElementMapping != null) customElementMapping(elementPart);
            return (T)this;
        }

        /// <summary>
        /// Maps this collection as a collection of components.
        /// </summary>
        /// <param name="action">Component mapping</param>
        public T Component(Action<CompositeElementPart<TChild>> action)
        {
            var part = new CompositeElementPart<TChild>(typeof(T));

            action(part);

            componentMapping = part;

            return (T)this;
        }

        /// <summary>
        /// Sets the table name for this one-to-many.
        /// </summary>
        /// <param name="name">Table name</param>
        public T Table(string name)
        {
            collectionAttributes.Set(Attr.Table, name);
            return (T)this;
        }

        public T ForeignKeyCascadeOnDelete()
        {
            keyMapping.OnDelete = "cascade";
            return (T)this;
        }

        public FetchBuilder<T> Fetch
        {
            get { return new FetchBuilder<T>((T)this, fetch); }
        }

        /// <summary>
        /// Set the access and naming strategy for this one-to-many.
        /// </summary>
        public AccessStrategyBuilder<T> Access
        {
            get { return new AccessStrategyBuilder<T>((T)this, access); }
        }

        public OptimisticLockBuilder<T> OptimisticLock
        {
            get { return new OptimisticLockBuilder<T>((T)this, optimisticLock); }
        }

        public T Persister<TPersister>() where TPersister : IEntityPersister
        {
            Persister(typeof(TPersister));
            return (T)this;
        }

        public T Persister(Type type)
        {
            collectionAttributes.Set(Attr.Persister, new TypeReference(type));
            return (T)this;
        }

        public T Persister(string type)
        {
            collectionAttributes.Set(Attr.Persister, new TypeReference(type));
            return (T)this;
        }

        public T Check(string checkSql)
        {
            collectionAttributes.Set(Attr.Check, checkSql);
            return (T)this;
        }

        public T Generic()
        {
            collectionAttributes.Set(Attr.Generic, nextBool);
            nextBool = true;
            return (T)this;
        }

        /// <summary>
        /// Sets the where clause for this one-to-many relationship.
        /// Note: This only supports simple cases, use the string overload for more complex clauses.
        /// </summary>
        public T Where(Expression<Predicate<TChild>> where)
        {
            var sql = ExpressionToSql.Convert(where);

            return Where(sql);
        }

        /// <summary>
        /// Sets the where clause for this one-to-many relationship.
        /// </summary>
        public T Where(string where)
        {
            collectionAttributes.Set(Attr.Where, where);
            return (T)this;
        }

        public T BatchSize(int size)
        {
            collectionAttributes.Set(Attr.BatchSize, size);
            return (T)this;
        }

        /// <summary>
        /// Inverts the next boolean
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public T Not
        {
            get
            {
                nextBool = !nextBool;
                return (T)this;
            }
        }

        /// <summary>
        /// Sets a custom collection type
        /// </summary>
        public T CollectionType<TCollection>()
        {
            return CollectionType(typeof(TCollection));
        }

        /// <summary>
        /// Sets a custom collection type
        /// </summary>
        public T CollectionType(Type type)
        {
            return CollectionType(new TypeReference(type));
        }

        /// <summary>
        /// Sets a custom collection type
        /// </summary>
        public T CollectionType(string type)
        {
            return CollectionType(new TypeReference(type));
        }

        /// <summary>
        /// Sets a custom collection type
        /// </summary>
        public T CollectionType(TypeReference type)
        {
            collectionAttributes.Set(Attr.CollectionType, type);
            return (T)this;
        }

        public T Schema(string schema)
        {
            collectionAttributes.Set(Attr.Schema, schema);
            return (T)this;
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
        public T ApplyFilter<TFilter>(string condition) where TFilter : FilterDefinition, new()
        {
            var part = new FilterPart(new TFilter().Name, condition);
            Filters.Add(part);
            return (T)this;
        }

        /// <summary>
        /// Applies a named filter to this one-to-many.
        /// </summary>
        /// <typeparam name="TFilter">
        /// The type of a <see cref="FilterDefinition"/> implementation
        /// defining the filter to apply.
        /// </typeparam>
        public T ApplyFilter<TFilter>() where TFilter : FilterDefinition, new()
        {
            return ApplyFilter<TFilter>(null);
        }

        protected IList<FilterPart> Filters
        {
            get { return filters; }
        }


    }
}
