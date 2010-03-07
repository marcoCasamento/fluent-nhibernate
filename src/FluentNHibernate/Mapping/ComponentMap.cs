using System;
using FluentNHibernate.Mapping.Providers;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;

namespace FluentNHibernate.Mapping
{
    public class ComponentMap<T> : ComponentPartBase<T>, IExternalComponentMappingProvider
    {
        private readonly AttributeStore<ComponentMapping> attributes;

        public ComponentMap()
            : this(new AttributeStore())
        {}

        internal ComponentMap(AttributeStore underlyingStore)
            : base(underlyingStore, "")
        {
            attributes = new AttributeStore<ComponentMapping>(underlyingStore);
        }

        protected override ComponentMapping CreateComponentMappingRoot(AttributeStore store)
        {
            return new ExternalComponentMapping(ComponentType.Component, attributes.CloneInner());
        }

        IUserDefinedMapping IMappingProvider.GetUserDefinedMappings()
        {
            return new FluentMapUserDefinedMappings(typeof(T), CreateComponentMapping());
        }

        public HibernateMapping GetHibernateMapping()
        {
            throw new NotImplementedException();
        }

        Type IExternalComponentMappingProvider.Type
        {
            get { return typeof(T); }
        }
    }
}