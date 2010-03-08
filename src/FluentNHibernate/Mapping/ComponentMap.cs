using System;
using FluentNHibernate.Mapping.Providers;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;

namespace FluentNHibernate.Mapping
{
    public class ComponentMap<T> : ComponentPartBase<T>, IExternalComponentMappingProvider
    {
        readonly IMappingStructure<ComponentMapping> structure;

        public ComponentMap()
            : this(new TypeStructure<ComponentMapping>(typeof(T)))
        {}

        ComponentMap(IMappingStructure<ComponentMapping> structure)
            : base(structure)
        {
            this.structure = structure;
        }

        IUserDefinedMapping IMappingProvider.GetUserDefinedMappings()
        {
            return new FluentMapUserDefinedMappings(typeof(T), structure);
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