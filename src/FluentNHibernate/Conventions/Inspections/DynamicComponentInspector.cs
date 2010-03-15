using System.Reflection;
using FluentNHibernate.Mapping;
using FluentNHibernate.MappingModel.ClassBased;

namespace FluentNHibernate.Conventions.Inspections
{
    public class DynamicComponentInspector : ComponentBaseInspector, IDynamicComponentInspector
    {
        private readonly InspectorMapper<IDynamicComponentInspector, ComponentMapping> mappedProperties = new InspectorMapper<IDynamicComponentInspector, ComponentMapping>();
        private readonly ComponentMapping mapping;

        public DynamicComponentInspector(ComponentMapping mapping)
            : base(mapping)
        {
            this.mapping = mapping;
        }

        public override bool IsSet(Member property)
        {
            return mapping.IsSpecified(mappedProperties.Get(property));
        }
    }
}