using System;
using System.Reflection;
using FluentNHibernate.Mapping;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Collections;

namespace FluentNHibernate.Conventions.Inspections
{
    public class BagInspector : CollectionInspector, IBagInspector
    {
        private readonly InspectorMapper<IBagInspector> mappedProperties = new InspectorMapper<IBagInspector>();
        private readonly CollectionMapping mapping;

        public BagInspector(CollectionMapping mapping)
            : base(mapping)
        {
            this.mapping = mapping;
            mappedProperties.Map(x => x.LazyLoad, Attr.Lazy);
        }

        public new bool IsSet(Member property)
        {
            throw new NotImplementedException();
            //return mapping.IsSpecified(mappedProperties.Get(property));
        }

        public new string OrderBy
        {
            get { return mapping.OrderBy; }
        }
    }
}
