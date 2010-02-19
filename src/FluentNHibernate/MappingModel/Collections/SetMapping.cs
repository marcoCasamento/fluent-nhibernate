using System;
using System.Linq.Expressions;
using FluentNHibernate.Utils;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.Collections
{
    public class SetMapping : CollectionMappingBase
    {
        public SetMapping()
            : this(new AttributeStore())
        {}
        
        public SetMapping(AttributeStore underlyingStore)
            : base(underlyingStore)
        {
            attributes = underlyingStore.Clone();
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessSet(this);
            base.AcceptVisitor(visitor);
        }

        public override string OrderBy
        {
            get { return attributes.Get(Attr.OrderBy); }
            set { attributes.Set(Attr.OrderBy, value); }
        }

        public string Sort
        {
            get { return attributes.Get(Attr.Sort); }
            set { attributes.Set(Attr.Sort, value); }
        }

        public new bool IsSpecified(Attr property)
        {
            return attributes.HasUserValue(property);
        }

        public new bool HasValue(Attr property)
        {
            return attributes.HasAnyValue(property);
        }

        public bool Equals(SetMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && Equals(other.attributes, attributes);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as SetMapping);
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