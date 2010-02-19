using System;
using System.Collections.Generic;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.Utils;

namespace FluentNHibernate.Mapping
{
    public class IndexManyToManyPart
    {
        private readonly Type entity;
        private readonly IList<string> columns = new List<string>();
        private readonly AttributeStore attributes = new AttributeStore();

        public IndexManyToManyPart(Type entity)
        {
            this.entity = entity;
        }

        public IndexManyToManyPart Column(string indexColumnName)
        {
            columns.Add(indexColumnName);
            return this;
        }

        public IndexManyToManyPart Type<TIndex>()
        {
            attributes.Set(Attr.Class, new TypeReference(typeof(TIndex)));
            return this;
        }

        public IndexManyToManyPart Type(Type indexType)
        {
            attributes.Set(Attr.Class, new TypeReference(indexType));
            return this;
        }

        public IndexManyToManyMapping GetIndexMapping()
        {
            var mapping = new IndexManyToManyMapping(attributes.Clone());

            mapping.ContainingEntityType = entity;

            columns.Each(x => mapping.AddColumn(new ColumnMapping { Name = x }));

            return mapping;
        }
    }
}