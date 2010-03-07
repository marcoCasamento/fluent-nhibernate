using System;
using FluentNHibernate.Mapping;
using FluentNHibernate.Mapping.Providers;
using FluentNHibernate.MappingModel.ClassBased;

namespace FluentNHibernate.Specs.FluentInterface
{
    public abstract class ProviderSpec
    {
        public static ClassMapping map_as_class<T>(Action<ClassMap<T>> setup)
        {
            var provider = new ClassMap<T>();

            setup(provider);

            return (ClassMapping)((IMappingProvider)provider).GetUserDefinedMappings().Mapping;
        }

        public static SubclassMapping map_as_subclass<T>(Action<SubclassMap<T>> setup)
        {
            var provider = new SubclassMap<T>();

            setup(provider);

            var userMappings = ((IIndeterminateSubclassMappingProvider)provider).GetUserDefinedMappings();
            var mapping = (SubclassMapping)userMappings.Mapping;
            mapping.SubclassType = SubclassType.Subclass;

            return mapping;
        }
    }
}