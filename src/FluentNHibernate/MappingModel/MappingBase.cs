using System;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel
{
    public abstract class MappingBase : IMappingBase
    {
        private AttributeStore attributes = new AttributeStore();

        public abstract void AcceptVisitor(IMappingModelVisitor visitor);
        
        public virtual bool IsSpecified(Attr property)
        {
            return attributes.HasUserValue(property);
        }

        public bool HasValue(Attr property)
        {
            return attributes.HasAnyValue(property);
        }

        protected object GetAttribute(Attr attribute)
        {
            return attributes.Get<object>(attribute);
        }

        protected TReturn GetAttribute<TReturn>(Attr attribute)
        {
            return attributes.Get<TReturn>(attribute);
        }

        protected void SetAttribute<T>(Attr attribute, T value)
        {
            attributes.Set(attribute, value);
        }

        protected void SetDefaultAttribute<T>(Attr attribute, T value)
        {
            attributes.SetDefault(attribute, value);
        }

        protected void ReplaceAttributes(AttributeStore replacement)
        {
            attributes = replacement.Clone();
        }

        public bool Equals(MappingBase other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.attributes, attributes);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(MappingBase)) return false;
            return Equals((MappingBase)obj);
        }

        public override int GetHashCode()
        {
            return attributes.GetHashCode();
        }
    }
}