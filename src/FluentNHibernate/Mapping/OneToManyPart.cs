using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using FluentNHibernate.Mapping.Providers;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Collections;
using NHibernate.Persister.Entity;

namespace FluentNHibernate.Mapping
{
    public interface HasManyArrayBuilder<TChild> : ICollectionMappingProvider
    {
        AccessStrategyBuilder<HasManyArrayBuilder<TChild>> Access { get; }
    }

    public interface HasManyElementBagBuilder<TChild> : ICollectionMappingProvider
    {
        HasManyElementBagBuilder<TChild> ValueColumn(string valueColumn);
        HasManyElementSetBuilder<TChild> AsSet();
        HasManyElementListBuilder<TChild> AsList();
        AccessStrategyBuilder<HasManyElementBagBuilder<TChild>> Access { get; }
        CollectionCascadeBuilder<HasManyElementBagBuilder<TChild>> Cascade { get; }
        FetchBuilder<HasManyElementBagBuilder<TChild>> Fetch { get; }
        OptimisticLockBuilder<HasManyElementBagBuilder<TChild>> OptimisticLock { get; }
        HasManyElementBagBuilder<TChild> BatchSize(int batchSize);
        HasManyElementBagBuilder<TChild> Check(string constraint);
        HasManyElementBagBuilder<TChild> CollectionType(string type);
        HasManyElementBagBuilder<TChild> CollectionType(Type type);
        HasManyElementBagBuilder<TChild> CollectionType<TCollection>();
        HasManyElementBagBuilder<TChild> Inverse();
        HasManyElementBagBuilder<TChild> LazyLoad();
        HasManyElementBagBuilder<TChild> ReadOnly();
        HasManyElementBagBuilder<TChild> OrderBy(string orderBy);
        HasManyElementBagBuilder<TChild> Persister(string type);
        HasManyElementBagBuilder<TChild> Persister(Type type);
        HasManyElementBagBuilder<TChild> Persister<TPersister>() where TPersister : IEntityPersister;
        HasManyElementBagBuilder<TChild> Schema(string schema);
        HasManyElementBagBuilder<TChild> Not { get; }
        HasManyElementBagBuilder<TChild> Subselect(string subselect);
        HasManyElementBagBuilder<TChild> Table(string table);
        HasManyElementBagBuilder<TChild> Where(string clause);
        HasManyElementBagBuilder<TChild> Where(Expression<Predicate<TChild>> clause);
    }

    public interface HasManyElementSetBuilder<TChild> : ICollectionMappingProvider
    {
        HasManyElementSetBuilder<TChild> ValueColumn(string valueColumn);
        HasManyElementBagBuilder<TChild> AsBag();
        HasManyElementListBuilder<TChild> AsList();
    }

    public interface HasManyElementListBuilder<TChild> : ICollectionMappingProvider
    {
        HasManyElementListBuilder<TChild> ValueColumn(string valueColumn);
        HasManyElementBagBuilder<TChild> AsBag();
        HasManyElementSetBuilder<TChild> AsSet();
    }

    public class HasManyElementBuilderImpl<TChild> : OneToManyPart<TChild>,
        HasManyElementBagBuilder<TChild>,
        HasManyElementListBuilder<TChild>,
        HasManyElementSetBuilder<TChild>
    {
        public HasManyElementBuilderImpl(Type entity, Member property)
            : base(entity, property)
        {
            Element();
        }

        public void ValueColumn(string valueColumn)
        {
            Element(valueColumn);
        }

        #region set

        HasManyElementBagBuilder<TChild> HasManyElementSetBuilder<TChild>.AsBag()
        {
            AsBag();
            return this;
        }

        HasManyElementListBuilder<TChild> HasManyElementSetBuilder<TChild>.AsList()
        {
            AsList();
            return this;
        }

        HasManyElementSetBuilder<TChild> HasManyElementSetBuilder<TChild>.ValueColumn(string valueColumn)
        {
            ValueColumn(valueColumn);
            return this;
        }

        #endregion

        #region bag

        HasManyElementBagBuilder<TChild> HasManyElementBagBuilder<TChild>.ValueColumn(string valueColumn)
        {
            ValueColumn(valueColumn);
            return this;
        }

        AccessStrategyBuilder<HasManyElementBagBuilder<TChild>> HasManyElementBagBuilder<TChild>.Access
        {
            get { return new AccessStrategyBuilder<HasManyElementBagBuilder<TChild>>(this, access); }
        }

        CollectionCascadeBuilder<HasManyElementBagBuilder<TChild>> HasManyElementBagBuilder<TChild>.Cascade
        {
            get { return new CollectionCascadeBuilder<HasManyElementBagBuilder<TChild>>(this, cascade); }
        }

        FetchBuilder<HasManyElementBagBuilder<TChild>> HasManyElementBagBuilder<TChild>.Fetch
        {
            get { return new FetchBuilder<HasManyElementBagBuilder<TChild>>(this, fetch); }
        }

        OptimisticLockBuilder<HasManyElementBagBuilder<TChild>> HasManyElementBagBuilder<TChild>.OptimisticLock
        {
            get { return new OptimisticLockBuilder<HasManyElementBagBuilder<TChild>>(this, optimisticLock); }
        }

        HasManyElementBagBuilder<TChild> HasManyElementBagBuilder<TChild>.BatchSize(int batchSize)
        {
            BatchSize(batchSize);
            return this;
        }

        HasManyElementBagBuilder<TChild> HasManyElementBagBuilder<TChild>.Check(string constraint)
        {
            Check(constraint);
            return this;
        }

        HasManyElementBagBuilder<TChild> HasManyElementBagBuilder<TChild>.CollectionType(string type)
        {
            CollectionType(type);
            return this;
        }

        HasManyElementBagBuilder<TChild> HasManyElementBagBuilder<TChild>.CollectionType(Type type)
        {
            CollectionType(type);
            return this;
        }

        HasManyElementBagBuilder<TChild> HasManyElementBagBuilder<TChild>.CollectionType<TCollection>()
        {
            CollectionType<TCollection>();
            return this;
        }

        HasManyElementBagBuilder<TChild> HasManyElementBagBuilder<TChild>.Inverse()
        {
            Inverse();
            return this;
        }

        HasManyElementBagBuilder<TChild> HasManyElementBagBuilder<TChild>.LazyLoad()
        {
            LazyLoad();
            return this;
        }

        HasManyElementBagBuilder<TChild> HasManyElementBagBuilder<TChild>.ReadOnly()
        {
            ReadOnly();
            return this;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        HasManyElementBagBuilder<TChild> HasManyElementBagBuilder<TChild>.Not
        {
            get
            {
                var tmp = Not;
                return this;
            }
        }

        HasManyElementBagBuilder<TChild> HasManyElementBagBuilder<TChild>.OrderBy(string orderBy)
        {
            OrderBy(orderBy);
            return this;
        }

        HasManyElementBagBuilder<TChild> HasManyElementBagBuilder<TChild>.Persister(string type)
        {
            Persister(type);
            return this;
        }

        HasManyElementBagBuilder<TChild> HasManyElementBagBuilder<TChild>.Persister(Type type)
        {
            Persister(type);
            return this;
        }

        HasManyElementBagBuilder<TChild> HasManyElementBagBuilder<TChild>.Persister<TPersister>()
        {
            Persister<TPersister>();
            return this;
        }

        HasManyElementBagBuilder<TChild> HasManyElementBagBuilder<TChild>.Schema(string schema)
        {
            Schema(schema);
            return this;
        }

        HasManyElementBagBuilder<TChild> HasManyElementBagBuilder<TChild>.Subselect(string subselect)
        {
            Subselect(subselect);
            return this;
        }

        HasManyElementBagBuilder<TChild> HasManyElementBagBuilder<TChild>.Table(string table)
        {
            Table(table);
            return this;
        }

        HasManyElementBagBuilder<TChild> HasManyElementBagBuilder<TChild>.Where(string clause)
        {
            Where(clause);
            return this;
        }

        HasManyElementBagBuilder<TChild> HasManyElementBagBuilder<TChild>.Where(Expression<Predicate<TChild>> clause)
        {
            Where(clause);
            return this;
        }

        HasManyElementBagBuilder<TChild> HasManyElementListBuilder<TChild>.AsBag()
        {
            AsBag();
            return this;
        }

        HasManyElementSetBuilder<TChild> HasManyElementListBuilder<TChild>.AsSet()
        {
            AsSet();
            return this;
        }

        HasManyElementListBuilder<TChild> HasManyElementListBuilder<TChild>.ValueColumn(string valueColumn)
        {
            ValueColumn(valueColumn);
            return this;
        }

        HasManyElementSetBuilder<TChild> HasManyElementBagBuilder<TChild>.AsSet()
        {
            AsSet();
            return this;
        }

        HasManyElementListBuilder<TChild> HasManyElementBagBuilder<TChild>.AsList()
        {
            AsList();
            return this;
        }

        #endregion
    }

    public abstract class HasManySetBuilderImpl<TChild> : OneToManyPart<TChild>
    {
        protected HasManySetBuilderImpl(Type entity, Member property)
            : base(entity, property)
        {
            AsSet();
        }
    }

    public abstract class HasManyListBuilderImpl<TChild> : OneToManyPart<TChild>
    {
        protected HasManyListBuilderImpl(Type entity, Member property)
            : base(entity, property)
        {
            AsList();
        }
    }

    public interface HasManyBagBuilder<TChild>
    {
        HasManySetBuilder<TChild> AsSet();
        HasManySetBuilder<TChild> AsSet<TComparer>() where TComparer : IComparer<TChild>;
        HasManySetBuilder<TChild> AsSet(SortType natural);
        HasManyListBuilder<TChild> AsList();
        HasManyListBuilder<TChild> AsList(Action<IndexBuilder> index);
    }

    public interface HasManyListBuilder<TChild>
    {}

    public interface HasManySetBuilder<TChild>
    { }

    public interface IndexBuilder
    {
        void Column(string indexColumnName);
        void Type<T>();
        void Type(Type type);
    }

    public class OneToManyPart<TChild> : ToManyBase<OneToManyPart<TChild>, TChild, OneToManyMapping>
    {
        private readonly Type entity;
        private readonly ColumnMappingCollection<OneToManyPart<TChild>> keyColumns;
        protected readonly CascadeBuilder cascade;
        private readonly NotFoundExpression<OneToManyPart<TChild>> notFound;
        private IndexManyToManyPart manyToManyIndex;
        private readonly Type childType;
        private Type valueType;
        private bool isTernary;

        public OneToManyPart(Type entity, Member member)
            : this(entity, member, member.PropertyType, new AttributeStore(), new AttributeStore())
        {}

        protected OneToManyPart(Type entity, Member member, Type collectionType, AttributeStore underlyingCollectionAttributes, AttributeStore underlyingRelationshipAttributes)
            : base(entity, member, collectionType)
        {
            this.entity = entity;
            childType = collectionType;

            keyColumns = new ColumnMappingCollection<OneToManyPart<TChild>>(this);
            cascade = new CascadeBuilder(value => collectionAttributes.Set(x => x.Cascade, value));
            notFound = new NotFoundExpression<OneToManyPart<TChild>>(this, value => relationshipAttributes.Set(x => x.NotFound, value));

            collectionAttributes.SetDefault(x => x.Name, member.Name);
        }

        public NotFoundExpression<OneToManyPart<TChild>> NotFound
        {
            get { return notFound; }
        }

        public new CollectionCascadeBuilder<OneToManyPart<TChild>> Cascade
        {
            get { return new CollectionCascadeBuilder<OneToManyPart<TChild>>(this, cascade); }
        }

        public override ICollectionMapping GetCollectionMapping()
        {
            var collection = base.GetCollectionMapping();

            if (keyColumns.Count() == 0)
                collection.Key.AddDefaultColumn(new ColumnMapping { Name = entity.Name + "_id" });

            foreach (var column in keyColumns)
            {
                collection.Key.AddColumn(column);
            }

            // HACK: shouldn't have to do this!
            if (manyToManyIndex != null && collection is MapMapping)
                ((MapMapping)collection).Index = manyToManyIndex.GetIndexMapping();

            return collection;
        }

        private void EnsureGenericDictionary()
        {
            if (!(childType.IsGenericType && childType.GetGenericTypeDefinition() == typeof(IDictionary<,>)))
                throw new ArgumentException(member.Name + " must be of type IDictionary<> to be used in a ternary assocation. Type was: " + childType);
        }

        public OneToManyPart<TChild> AsTernaryAssociation()
        {
            var keyType = childType.GetGenericArguments()[0];
            return AsTernaryAssociation(keyType.Name + "_id");
        }

        public OneToManyPart<TChild> AsTernaryAssociation(string indexColumnName)
        {
            EnsureGenericDictionary();

            var keyType = childType.GetGenericArguments()[0];
            var valType = childType.GetGenericArguments()[1];

            manyToManyIndex = new IndexManyToManyPart(typeof(ManyToManyPart<TChild>));
            manyToManyIndex.Column(indexColumnName);
            manyToManyIndex.Type(keyType);

            valueType = valType;
            isTernary = true;

            return this;
        }

        public OneToManyPart<TChild> AsEntityMap()
        {
            // The argument to AsMap will be ignored as the ternary association will overwrite the index mapping for the map.
            // Therefore just pass null.
            //return AsMap(null).AsTernaryAssociation();
            throw new NotImplementedException();
        }

        public OneToManyPart<TChild> AsEntityMap(string indexColumnName)
        {
            throw new NotImplementedException();
            //return AsMap(null).AsTernaryAssociation(indexColumnName);
        }

        protected override ICollectionRelationshipMapping GetRelationship()
        {
            var mapping = new OneToManyMapping(relationshipAttributes.CloneInner())
            {
                ContainingEntityType = entity
            };

            if (isTernary && valueType != null)
                mapping.Class = new TypeReference(valueType);

            return mapping;
        }

        public OneToManyPart<TChild> KeyColumn(string columnName)
        {
            KeyColumns.Clear();
            KeyColumns.Add(columnName);
            return this;
        }

        public ColumnMappingCollection<OneToManyPart<TChild>> KeyColumns
        {
            get { return keyColumns; }
        }

        public OneToManyPart<TChild> ForeignKeyConstraintName(string foreignKeyName)
        {
            keyMapping.ForeignKey = foreignKeyName;
            return this;
        }

        /// <summary>
        /// This method is used to set a different key column in this table to be used for joins.
        /// The output is set as the property-ref attribute in the "key" subelement of the collection
        /// </summary>
        /// <param name="propertyRef">The name of the column in this table which is linked to the foreign key</param>
        /// <returns>OneToManyPart</returns>
        public OneToManyPart<TChild> PropertyRef(string propertyRef)
        {
            keyMapping.PropertyRef = propertyRef;
            return this;
        }

        /// <summary>
        /// Sets the order-by clause for this one-to-many relationship.
        /// </summary>
        public OneToManyPart<TChild> OrderBy(string orderBy)
        {
            collectionAttributes.Set(x => x.OrderBy, orderBy);
            return this;
        }

        public OneToManyPart<TChild> ReadOnly()
        {
            collectionAttributes.Set(x => x.Mutable, !nextBool);
            nextBool = true;
            return this;
        }

        public OneToManyPart<TChild> Subselect(string subselect)
        {
            collectionAttributes.Set(x => x.Subselect, subselect);
            return this;
        }

        public OneToManyPart<TChild> KeyUpdate()
        {
            keyMapping.Update = nextBool;
            nextBool = true;
            return this;
        }

        public OneToManyPart<TChild> KeyNullable()
        {
            keyMapping.NotNull = !nextBool;
            nextBool = true;
            return this;
        }
    }
}
