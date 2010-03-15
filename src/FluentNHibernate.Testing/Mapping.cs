using System;
using System.Collections.Generic;
using FluentNHibernate.MappingModel;

namespace FluentNHibernate.Testing
{
    public static class TestMappings
    {
        public static T Create<T>(IDictionary<Attr, object> values)
            where T : IMapping, new()
        {
            var mapping = new T();

            mapping.UpdateValues(values);

            return mapping;
        }

        public static ColumnMapping CreateColumn(string name)
        {
            return CreateColumn(new Dictionary<Attr, object> {{Attr.Name, name}});
        }

        public static ColumnMapping CreateColumn(IDictionary<Attr, object> values)
        {
            return Create<ColumnMapping>(values);
        }
    }
}