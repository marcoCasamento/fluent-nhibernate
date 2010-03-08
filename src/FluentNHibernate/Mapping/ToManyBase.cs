using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.Utils;
using NHibernate.Persister.Entity;

namespace FluentNHibernate.Mapping
{
    public abstract class ToManyBase<T, TChild, TRelationshipAttributes>
        where T : ToManyBase<T, TChild, TRelationshipAttributes>
        where TRelationshipAttributes : ICollectionRelationshipMapping
    {
        readonly IMappingStructure<ICollectionMapping> structure;
        readonly IMappingStructure<KeyMapping> keyStructure;
        readonly AccessStrategyBuilder<T> access;
        readonly FetchTypeExpression<T> fetch;
        readonly OptimisticLockBuilder<T> optimisticLock;
        readonly CollectionCascadeExpression<T> cascade;
        protected bool nextBool = true;

        readonly IList<FilterPart> filters = new List<FilterPart>();
        Func<AttributeStore, ICollectionMapping> collectionBuilder;
        IMappingStructure<CacheMapping> cacheStructure;

        protected ToManyBase(IMappingStructure<ICollectionMapping> structure, IMappingStructure<KeyMapping> keyStructure)
        {
            this.structure = structure;
            this.keyStructure = keyStructure;
            structure.AddChild(keyStructure);

            access = new AccessStrategyBuilder<T>((T)this, value => structure.SetValue(Attr.Access, value));
            fetch = new FetchTypeExpression<T>((T)this, value => structure.SetValue(Attr.Fetch, value));
            optimisticLock = new OptimisticLockBuilder<T>((T)this, value => structure.SetValue(Attr.OptimisticLock, value));
            cascade = new CollectionCascadeExpression<T>((T)this, value => structure.SetValue(Attr.Cascade, value));

            AsBag();
        }

        /// <summary>
        /// This method is used to set a different key column in this table to be used for joins.
        /// The output is set as the property-ref attribute in the "key" subelement of the collection
        /// </summary>
        /// <param name="propertyRef">The name of the column in this table which is linked to the foreign key</param>
        /// <returns>OneToManyPart</returns>
        public T PropertyRef(string propertyRef)
        {
            keyStructure.SetValue(Attr.PropertyRef, propertyRef);
            return (T)this;
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

        public T LazyLoad()
        {
            structure.SetValue(Attr.Lazy, nextBool);
            nextBool = true;
            return (T)this;
        }

        public T Inverse()
        {
            structure.SetValue(Attr.Inverse, nextBool);
            nextBool = true;
            return (T)this;
        }

        public CollectionCascadeExpression<T> Cascade
        {
            get { return cascade; }
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
            CreateIndexMapping(null);
            return (T)this;
        }

        public T AsList(Action<IndexPart> customIndexMapping)
        {
            collectionBuilder = attrs => new ListMapping(attrs);
            CreateIndexMapping(customIndexMapping);
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
            Element(string.Empty, customElementMapping);
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
            CreateIndexMapping(customIndexMapping);
            return (T)this;
        }

        private void CreateIndexMapping(Action<IndexPart> customIndex)
        {
            var indexStructure = new BucketStructure<IndexMapping>();
            var indexPart = new IndexPart(indexStructure);

            if (customIndex != null)
                customIndex(indexPart);

            structure.AddChild(indexStructure);
        }

        public T Element(string columnName)
        {
            return Element(columnName, x => {});
        }

        public T Element(string columnName, Action<ElementPart> customElementMapping)
        {
            var elementStructure = new BucketStructure<ElementMapping>();
            var part = new ElementPart(elementStructure);
            part.Type<TChild>();

            if (!string.IsNullOrEmpty(columnName))
                part.Column(columnName);

            if (customElementMapping != null)
                customElementMapping(part);

            return (T)this;
        }

        /// <summary>
        /// Maps this collection as a collection of components.
        /// </summary>
        /// <param name="action">Component mapping</param>
        public T Component(Action<CompositeElementPart<TChild>> action)
        {
            var compositeElementStructure = new TypeStructure<CompositeElementMapping>(typeof(TChild));
            var part = new CompositeElementPart<TChild>(compositeElementStructure);

            action(part);

            structure.AddChild(compositeElementStructure);

            return (T)this;
        }

        /// <summary>
        /// Sets the table name for this one-to-many.
        /// </summary>
        /// <param name="name">Table name</param>
        public T Table(string name)
        {
            structure.SetValue(Attr.Table, name);
            return (T)this;
        }

        public T ForeignKeyCascadeOnDelete()
        {
            keyStructure.SetValue(Attr.OnDelete, "cascade");
            return (T)this;
        }

        public FetchTypeExpression<T> Fetch
        {
            get { return fetch; }
        }

        /// <summary>
        /// Set the access and naming strategy for this one-to-many.
        /// </summary>
        public AccessStrategyBuilder<T> Access
        {
            get { return access; }
        }

        public OptimisticLockBuilder<T> OptimisticLock
        {
            get { return optimisticLock; }
        }

        public T Persister<TPersister>() where TPersister : IEntityPersister
        {
            structure.SetValue(Attr.Persister, new TypeReference(typeof(TPersister)));
            return (T)this;
        }

        public T Check(string checkSql)
        {
            structure.SetValue(Attr.Check, checkSql);
            return (T)this;
        }

        public T Generic()
        {
            structure.SetValue(Attr.Generic, nextBool);
            nextBool = true;
            return (T)this;
        }

        /// <summary>
        /// Sets the where clause for this one-to-many relationship.
        /// Note: This only supports simple cases, use the string overload for more complex clauses.
        /// </summary>
        public T Where(Expression<Func<TChild, bool>> where)
        {
            var sql = ExpressionToSql.Convert(where);

            return Where(sql);
        }

        /// <summary>
        /// Sets the where clause for this one-to-many relationship.
        /// </summary>
        public T Where(string where)
        {
            structure.SetValue(Attr.Where, where);
            return (T)this;
        }

        public T BatchSize(int size)
        {
            structure.SetValue(Attr.BatchSize, size);
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
            structure.SetValue(Attr.CollectionType, type);
            return (T)this;
        }

        public T Schema(string schema)
        {
            structure.SetValue(Attr.Schema, schema);
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
