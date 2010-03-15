using System.Collections.Generic;
using FluentNHibernate.MappingModel;

namespace FluentNHibernate.Testing.ConventionsTests.Inspection
{
    public static class MappingExtensions
    {
        public static void SetValue(this IMapping mapping, Attr attr, object value)
        {
            mapping.UpdateValues(new[] { new KeyValuePair<Attr, object>(attr, value), });
        }
    }
}