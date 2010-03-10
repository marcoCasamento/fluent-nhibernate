using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using FluentNHibernate.Mapping;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.ClassBased
{
    public interface IComponentMapping
    {
        void AcceptVisitor(IMappingModelVisitor visitor);
        ParentMapping Parent { get; set; }
        bool Insert { get; set; }
        bool Update { get; set; }
        string Access { get; set; }
        Type ContainingEntityType { get; }
        string Name { get; set; }
        Member Member { get; }
        Type Type { get; }
        bool OptimisticLock { get; set; }
        bool Unique { get; }
        IEnumerable<ManyToOneMapping> References { get; }
        IEnumerable<CollectionMapping> Collections { get; }
        IEnumerable<PropertyMapping> Properties { get; }
        IEnumerable<IComponentMapping> Components { get; }
        IEnumerable<OneToOneMapping> OneToOnes { get; }
        IEnumerable<AnyMapping> Anys { get; }
        ComponentType ComponentType { get; }
        TypeReference Class { get; set; }
        bool Lazy { get; set; }
        void AddProperty(PropertyMapping mapping);
        void AddComponent(IComponentMapping mapping);
        void AddOneToOne(OneToOneMapping mapping);
        void AddCollection(CollectionMapping mapping);
        void AddReference(ManyToOneMapping mapping);
        void AddAny(AnyMapping mapping);
        bool HasValue(Attr attr);
    }
}