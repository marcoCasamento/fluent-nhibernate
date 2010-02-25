using System;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.ClassBased
{
    public class DynamicComponentMapping : ComponentMappingBase
    {
        public DynamicComponentMapping()
            : this(null)
        { }

        public DynamicComponentMapping(AttributeStore store)
            : base(store)
        {}

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessComponent(this);

            base.AcceptVisitor(visitor);
        }
    }
}
