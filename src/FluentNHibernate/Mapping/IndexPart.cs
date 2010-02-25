using System;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.Utils;

namespace FluentNHibernate.Mapping
{
    public class IndexPart
    {
        private readonly Type entity;
        private readonly DefaultableList<string> columns = new DefaultableList<string>();
        private readonly AttributeStore attributes = new AttributeStore();

        public IndexPart(Type entity, string defaultColumnName, Type defaultIndexType)
        {
            this.entity = entity;

            attributes.SetDefault(Attr.Type, new TypeReference(defaultIndexType));
            columns.AddDefault(defaultColumnName);
        }

        public IndexPart Column(string indexColumnName)
        {
            columns.Add(indexColumnName);
            return this;
        }

        public IndexPart Type<TIndex>()
        {
            attributes.Set(Attr.Type, new TypeReference(typeof(TIndex)));
            return this;
        }

        public IndexPart Type(Type type)
        {
            attributes.Set(Attr.Type, new TypeReference(type));
            return this;
        }

        public IndexMapping GetIndexMapping()
        {
            var mapping = new IndexMapping(attributes.Clone());

            mapping.ContainingEntityType = entity;

            columns.Defaults.Each(x => mapping.AddDefaultColumn(new ColumnMapping { Name = x }));
            columns.UserDefined.Each(x => mapping.AddColumn(new ColumnMapping { Name = x }));

            return mapping;
        }
    }
}
