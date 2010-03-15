using System;
using System.Reflection;
using FluentNHibernate.MappingModel;

namespace FluentNHibernate.Conventions.Inspections
{
    public class CacheInspector : ICacheInspector
    {
        private readonly InspectorMapper<ICacheInspector> propertyMappings = new InspectorMapper<ICacheInspector>();
        private readonly CacheMapping mapping;

        public CacheInspector(CacheMapping mapping)
        {
            this.mapping = mapping;
        }

        public string Usage
        {
            get { return mapping.Usage; }
        }

        public string Region
        {
            get { return mapping.Region; }
        }

        public Include Include
        {
            get { return Include.FromString(mapping.Include); }
        }

        public Type EntityType
        {
            get { return mapping.ContainedEntityType; }
        }

        public string StringIdentifierForModel
        {
            get { return mapping.Usage; }
        }

        public bool IsSet(Member property)
        {
            throw new NotImplementedException();
            //return mapping.IsSpecified(propertyMappings.Get(property));
        }
    }
}