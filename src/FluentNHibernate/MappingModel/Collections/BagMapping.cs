using System;
using System.Linq.Expressions;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.Collections
{
    public class BagMapping : CollectionMappingBase
    {
        public BagMapping()
            : this(new AttributeStore())
        {}

        public BagMapping(AttributeStore underlyingStore)
            : base(underlyingStore)
        {
            attributes = underlyingStore.Clone();
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessBag(this);
            base.AcceptVisitor(visitor);
        }

        public override string OrderBy
        {
            get { return attributes.Get(Attr.OrderBy); }
            set { attributes.Set(Attr.OrderBy, value); }
        }

        public bool Equals(BagMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && Equals(other.attributes, attributes);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as BagMapping);
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
