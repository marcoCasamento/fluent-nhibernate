using System;
using System.Linq.Expressions;
using System.Reflection;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.ClassBased
{
    public class ComponentMapping : ComponentMappingBase
    {
        public ComponentMapping()
            : this(new AttributeStore())
        {}

        public ComponentMapping(AttributeStore store)
            : base(store)
        {
            attributes = store.Clone();
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessComponent(this);

            base.AcceptVisitor(visitor);
        }

        public TypeReference Class
        {
            get { return attributes.Get<TypeReference>(Attr.Class); }
            set { attributes.Set(Attr.Class, value); }
        }

        public bool Lazy
        {
            get { return attributes.Get<bool>(Attr.Lazy); }
            set { attributes.Set(Attr.Lazy, value); }
        }

        public bool Equals(ComponentMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) &&
                Equals(other.attributes, attributes);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as ComponentMapping);
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
