using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FluentNHibernate.MappingModel;
using FluentNHibernate.Utils;

namespace FluentNHibernate.Conventions.Inspections
{
    public class InspectorMapper<TInspector, TModel>
    {
        public string Get(Member property)
        {
            throw new NotImplementedException();
        }

        public void Map(Func<TInspector, object> func, Func<TModel, object> func2)
        {
            throw new NotImplementedException();
        }
        public void Map(Func<TInspector, object> func, string notnull)
        {
            throw new NotImplementedException();
        }
    }

    public interface IInspectorMapper
    {
        Attr Get(Member member);
    }

    public class InspectorMapper<TInspector> : IInspectorMapper
    {
        private readonly IDictionary<string, Attr> mappings = new Dictionary<string, Attr>();

        public void Map(Expression<Func<TInspector, object>> inspectorProperty, Attr attr)
        {
            mappings[inspectorProperty.ToMember().Name] = attr;
        }

        public Attr Get(Member property)
        {
            if (mappings.ContainsKey(property.Name))
                return mappings[property.Name];

            return (Attr)Enum.Parse(typeof(Attr), property.Name);
        }
    }
}