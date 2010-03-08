using System;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Collections;

namespace FluentNHibernate.Mapping
{
    public class OneToManyPart<TChild> : ToManyBase<OneToManyPart<TChild>, TChild, OneToManyMapping>
    {
        readonly IMappingStructure<ICollectionMapping> structure;
        readonly IMappingStructure<KeyMapping> keyStructure;
        private readonly ColumnMappingCollection<OneToManyPart<TChild>> keyColumns;
        private readonly CollectionCascadeExpression<OneToManyPart<TChild>> cascade;
        private readonly NotFoundExpression<OneToManyPart<TChild>> notFound;

        public OneToManyPart(IMappingStructure<ICollectionMapping> structure)
            : this(structure, new BucketStructure<KeyMapping>())
        {}

        OneToManyPart(IMappingStructure<ICollectionMapping> structure, IMappingStructure<KeyMapping> keyStructure)
            : base(structure, keyStructure)
        {
            this.structure = structure;
            this.keyStructure = keyStructure;

            keyColumns = new ColumnMappingCollection<OneToManyPart<TChild>>(this, structure);
            cascade = new CollectionCascadeExpression<OneToManyPart<TChild>>(this, value => structure.SetValue(Attr.Cascade, value));
            notFound = new NotFoundExpression<OneToManyPart<TChild>>(this, value => structure.SetValue(Attr.NotFound, value));

            CreateRelationship();
        }

        void CreateRelationship()
        {
            var relationshipStructure = new BucketStructure<OneToManyMapping>();
            relationshipStructure.SetValue(Attr.Class, new TypeReference(typeof(TChild)));
            structure.AddChild(relationshipStructure);
        }

        public NotFoundExpression<OneToManyPart<TChild>> NotFound
        {
            get { return notFound; }
        }

        public new CollectionCascadeExpression<OneToManyPart<TChild>> Cascade
        {
            get { return cascade; }
        }

        private void EnsureGenericDictionary()
        {
            var childType = typeof(TChild);
            if (!(childType.IsGenericType && childType.GetGenericTypeDefinition() == typeof(IDictionary<,>)))
                throw new ArgumentException(" must be of type IDictionary<> to be used in a ternary assocation. Type was: " + childType);
        }

        public OneToManyPart<TChild> AsTernaryAssociation()
        {
            var childType = typeof(TChild);
            var keyType = childType.GetGenericArguments()[0];
            return AsTernaryAssociation(keyType.Name + "_id");
        }

        public OneToManyPart<TChild> AsTernaryAssociation(string indexColumnName)
        {
            EnsureGenericDictionary();

            var childType = typeof(TChild);
            var keyType = childType.GetGenericArguments()[0];
            var valType = childType.GetGenericArguments()[1];

            var indexStructure = new BucketStructure<IndexManyToManyMapping>();
            var part = new IndexManyToManyPart(indexStructure);
            part.Column(indexColumnName);
            part.Type(keyType);

            return this;
        }

        public OneToManyPart<TChild> AsEntityMap()
        {
            // The argument to AsMap will be ignored as the ternary association will overwrite the index mapping for the map.
            // Therefore just pass null.
            return AsMap(null).AsTernaryAssociation();
        }

        public OneToManyPart<TChild> AsEntityMap(string indexColumnName)
        {
            return AsMap(null).AsTernaryAssociation(indexColumnName);
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
            keyStructure.SetValue(Attr.ForeignKey, foreignKeyName);
            return this;
        }

        /// <summary>
<<<<<<< HEAD
=======
        /// This method is used to set a different key column in this table to be used for joins.
        /// The output is set as the property-ref attribute in the "key" subelement of the collection
        /// </summary>
        /// <param name="propertyRef">The name of the column in this table which is linked to the foreign key</param>
        /// <returns>OneToManyPart</returns>
        public OneToManyPart<TChild> PropertyRef(string propertyRef)
        {
            keyStructure.SetValue(Attr.PropertyRef, propertyRef);
            return this;
        }

        /// <summary>
>>>>>>> Updated all the FI classes to create a structure
        /// Sets the order-by clause for this one-to-many relationship.
        /// </summary>
        public OneToManyPart<TChild> OrderBy(string orderBy)
        {
            structure.SetValue(Attr.OrderBy, orderBy);
            return this;
        }

        public OneToManyPart<TChild> ReadOnly()
        {
            structure.SetValue(Attr.Mutable, !nextBool);
            nextBool = true;
            return this;
        }

        public OneToManyPart<TChild> Subselect(string subselect)
        {
            structure.SetValue(Attr.Subselect, subselect);
            return this;
        }

        public OneToManyPart<TChild> KeyUpdate()
        {
            keyStructure.SetValue(Attr.Update, nextBool);
            nextBool = true;
            return this;
        }

        public OneToManyPart<TChild> KeyNullable()
        {
            keyStructure.SetValue(Attr.NotNull, !nextBool);
            nextBool = true;
            return this;
        }
    }
}
