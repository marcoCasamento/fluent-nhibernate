using System;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.ClassBased
{
    public class DynamicComponentMapping : ComponentMappingBase
    {
        public DynamicComponentMapping()
            : this(new AttributeStore())
        { }

        public DynamicComponentMapping(AttributeStore store)
            : base(store)
        {
            attributes = store.Clone();
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessComponent(this);

            base.AcceptVisitor(visitor);
        }

        public bool Equals(DynamicComponentMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && Equals(other.attributes, attributes);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as DynamicComponentMapping);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                {
                    return (base.GetHashCode() * 397) ^ (attributes != null ? attributes.GetHashCode() : 0);
                }
            }
        }
    }
}
