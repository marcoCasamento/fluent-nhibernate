using System;
using System.Linq.Expressions;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel
{
    public class MetaValueMapping : MappingBase
    {
        private readonly AttributeStore attributes;

        public MetaValueMapping()
            : this(new AttributeStore())
        {}

        protected MetaValueMapping(AttributeStore underlyingStore)
        {
            attributes = underlyingStore.Clone();
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessMetaValue(this);
        }

        public string Value
        {
            get { return attributes.Get(Attr.Value); }
            set { attributes.Set(Attr.Value, value); }
        }

        public TypeReference Class
        {
            get { return attributes.Get<TypeReference>(Attr.Class); }
            set { attributes.Set(Attr.Class, value); }
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

        public bool Equals(MetaValueMapping other)
        {
            return Equals(other.attributes, attributes) && Equals(other.ContainingEntityType, ContainingEntityType);
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(MetaValueMapping)) return false;
            return Equals((MetaValueMapping)obj);
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