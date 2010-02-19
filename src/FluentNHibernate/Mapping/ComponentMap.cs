using System;
using FluentNHibernate.Mapping.Providers;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;

namespace FluentNHibernate.Mapping
{
    public class ComponentMap<T> : ComponentPartBase<T>, IExternalComponentMappingProvider
    {
        private readonly AttributeStore attributes;

        public ComponentMap()
            : this(new AttributeStore())
        {}

        internal ComponentMap(AttributeStore underlyingStore)
            : base(underlyingStore, "")
        {
            attributes = underlyingStore.Clone();
        }

        protected override IComponentMapping CreateComponentMappingRoot(AttributeStore store)
        {
            return new ExternalComponentMapping(attributes.Clone());
        }

        ExternalComponentMapping IExternalComponentMappingProvider.GetComponentMapping()
        {
            return (ExternalComponentMapping)CreateComponentMapping();
        }

        Type IExternalComponentMappingProvider.Type
        {
            get { return typeof(T); }
        }
    }
}