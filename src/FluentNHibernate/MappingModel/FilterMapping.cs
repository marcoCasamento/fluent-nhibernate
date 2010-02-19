using System;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel
{
    public class FilterMapping : IMappingBase
    {
        private readonly AttributeStore attributes;

        public FilterMapping()
            : this(new AttributeStore())
        { }

        public FilterMapping(AttributeStore underlyingStore)
        {
            attributes = underlyingStore.Clone();
        }

        public string Name
        {
            get { return attributes.Get(Attr.Name); }
            set { attributes.Set(Attr.Name, value); }
        }

        public string Condition
        {
            get { return attributes.Get(Attr.Condition); }
            set { attributes.Set(Attr.Condition, value); }
        }

        public void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessFilter(this);
        }

        public bool IsSpecified(Attr property)
        {
            return attributes.HasUserValue(property);
        }

        public bool Equals(FilterMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.attributes, attributes);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(FilterMapping)) return false;
            return Equals((FilterMapping)obj);
        }

        public override int GetHashCode()
        {
            return (attributes != null ? attributes.GetHashCode() : 0);
        }
    }
}
