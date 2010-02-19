using System;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Collections;

namespace FluentNHibernate.Conventions.Instances
{
    public class SetInstance : SetInspector, ISetInstance
    {
        private readonly SetMapping mapping;
        public SetInstance(SetMapping mapping)
            : base(mapping)
        {
            this.mapping = mapping;
        }

        public new void OrderBy(string orderBy)
        {
            if (mapping.IsSpecified(Attr.OrderBy))
                return;

            mapping.OrderBy = orderBy;
        }

        public new void Sort(string sort)
        {
            if (mapping.IsSpecified(Attr.Sort))
                return;

            mapping.Sort = sort;
        }

        public new IAccessInstance Access
        {
            get
            {
                return new AccessInstance(value =>
                {
                    if (!mapping.IsSpecified(Attr.Access))
                        mapping.Access = value;
                });
            }
        }

        public void OverrideInferredChildType(Type type)
        {
            mapping.ChildType = type;
        }
    }
}
