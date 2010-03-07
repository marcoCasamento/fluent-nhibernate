using System;
using System.Collections.Generic;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;

namespace FluentNHibernate
{
    public interface IMappingProvider
    {
        IUserDefinedMapping GetUserDefinedMappings();
        // HACK: In place just to keep compatibility until verdict is made
        HibernateMapping GetHibernateMapping();
        //IEnumerable<string> GetIgnoredProperties();
    }

    public interface IUserDefinedMapping
    {
        object Mapping { get; }
        Type Type { get; }
    }

    public class FluentMapUserDefinedMappings : IUserDefinedMapping
    {
        public FluentMapUserDefinedMappings(Type entityType, object mapping)
        {
            Mapping = mapping;
            Type = entityType;
        }

        public object Mapping { get; private set; }
        public Type Type { get; private set; }
    }
}