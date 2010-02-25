using System;
using System.Linq.Expressions;
using System.Reflection;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.ClassBased
{
    public class ComponentMapping : ComponentMappingBase
    {
        public ComponentMapping()
            : this(null)
        {}

        public ComponentMapping(AttributeStore store)
            : base(store)
        {}

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessComponent(this);

            base.AcceptVisitor(visitor);
        }

        public TypeReference Class
        {
            get { return (TypeReference)GetAttribute(Attr.Class); }
            set { SetAttribute(Attr.Class, value); }
        }

        public bool Lazy
        {
            get { return GetAttribute<bool>(Attr.Lazy); }
            set { SetAttribute(Attr.Lazy, value); }
        }
    }
}
