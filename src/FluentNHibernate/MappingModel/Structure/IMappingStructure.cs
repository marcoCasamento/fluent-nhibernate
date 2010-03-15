using System;
using System.Collections.Generic;

namespace FluentNHibernate.MappingModel.Structure
{
    public interface IMappingStructure
    {
        IEnumerable<KeyValuePair<Attr, object>> Values { get; }
        IMappingStructure Parent { get; set; }
        IEnumerable<IMappingStructure> Children { get; }
        void AddChild(IMappingStructure child);
        void SetValue(Attr key, object value);
        T GetValue<T>(Attr key);
        void RemoveChildrenMatching(Predicate<IMappingStructure> predicate);
        IMapping CreateMappingNode();
        void ApplyCustomisations();
    }

    public interface IMappingStructure<T> : IMappingStructure
        where T : IMapping
    {
        void Alter(Action<T> alteration);
    }

    public interface ITypeMappingStructure : IMappingStructure
    {
        Type Type { get; }
    }
}