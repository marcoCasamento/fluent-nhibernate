using System;
using System.Linq.Expressions;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel
{
    public class OneToOneMapping : MappingBase
    {
        private readonly AttributeStore attributes;

        public OneToOneMapping()
            : this(new AttributeStore())
        {}

        public OneToOneMapping(AttributeStore underlyingStore)
        {
            attributes = underlyingStore.Clone();
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessOneToOne(this);
        }

        public string Name
        {
            get { return attributes.Get(Attr.Name); }
            set { attributes.Set(Attr.Name, value); }
        }

        public string Access
        {
            get { return attributes.Get(Attr.Access); }
            set { attributes.Set(Attr.Access, value); }
        }

        public TypeReference Class
        {
            get { return attributes.Get<TypeReference>(Attr.Class); }
            set { attributes.Set(Attr.Class, value); }
        }

        public string Cascade
        {
            get { return attributes.Get(Attr.Cascade); }
            set { attributes.Set(Attr.Cascade, value); }
        }

        public bool Constrained
        {
            get { return attributes.Get<bool>(Attr.Constrained); }
            set { attributes.Set(Attr.Constrained, value); }
        }

        public string Fetch
        {
            get { return attributes.Get(Attr.Fetch); }
            set { attributes.Set(Attr.Fetch, value); }
        }

        public string ForeignKey
        {
            get { return attributes.Get(Attr.ForeignKey); }
            set { attributes.Set(Attr.ForeignKey, value); }
        }

        public string PropertyRef
        {
            get { return attributes.Get(Attr.PropertyRef); }
            set { attributes.Set(Attr.PropertyRef, value); }
        }

        public bool Lazy
        {
            get { return attributes.Get<bool>(Attr.Lazy); }
            set { attributes.Set(Attr.Lazy, value); }
        }

        public string EntityName
        {
            get { return attributes.Get(Attr.EntityName); }
            set { attributes.Set(Attr.EntityName, value); }
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

        public void SetDefaultValue<TResult>(Attr property, TResult value)
        {
            attributes.SetDefault(property, value);
        }

        public bool Equals(OneToOneMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.attributes, attributes) && Equals(other.ContainingEntityType, ContainingEntityType);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(OneToOneMapping)) return false;
            return Equals((OneToOneMapping)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((attributes != null ? attributes.GetHashCode() : 0) * 397) ^ (ContainingEntityType != null ? ContainingEntityType.GetHashCode() : 0);
            }
        }
    }
}