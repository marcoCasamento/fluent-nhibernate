using System;
using System.Linq.Expressions;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel
{
    public class ParentMapping : MappingBase
    {
        private readonly AttributeStore attributes;

        public ParentMapping()
            : this(new AttributeStore())
        {}

        protected ParentMapping(AttributeStore underlyingStore)
        {
            attributes = underlyingStore.Clone();
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessParent(this);
        }

        public string Name
        {
            get { return attributes.Get(Attr.Name); }
            set { attributes.Set(Attr.Name, value); }
        }

        public Type ContainingEntityType { get; set; }

        public override bool IsSpecified(Attr property)
        {
            return attributes.HasUserValue(property);
        }

        public bool HasValue(Attr property)
        {
            return attributes.HasAnyValue(property);
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(ParentMapping)) return false;
            
            return Equals((ParentMapping)obj);
        }

        public bool Equals(ParentMapping other)
        {
            return Equals(other.attributes, attributes) && Equals(other.ContainingEntityType, ContainingEntityType);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((attributes != null ? attributes.GetHashCode() : 0) * 397) ^
                    (ContainingEntityType != null ? ContainingEntityType.GetHashCode() : 0);
            }
        }
    }
}