using System;
using System.Reflection;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;

namespace FluentNHibernate.Conventions.Inspections
{
    public class ComponentInspector : ComponentBaseInspector, IComponentInspector
    {
        private readonly InspectorMapper<IComponentInspector> mappedProperties = new InspectorMapper<IComponentInspector>();
        private readonly ComponentMapping mapping;

        public ComponentInspector(ComponentMapping mapping)
            : base(mapping)
        {
            this.mapping = mapping;
            mappedProperties.Map(x => x.LazyLoad, Attr.Lazy);
        }

        public override bool IsSet(Member property)
        {
            throw new NotImplementedException();
            //return mapping.IsSpecified(mappedProperties.Get(property));
        }

        public bool LazyLoad
        {
            get { return mapping.Lazy; }
        }
    }
}