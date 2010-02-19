using System;
using System.Linq.Expressions;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel
{
    public class TuplizerMapping : MappingBase
    {
        private readonly AttributeStore attributes;

        public TuplizerMapping()
            : this(new AttributeStore())
        {}

        public TuplizerMapping(AttributeStore underlyingStore)
        {
            attributes = underlyingStore.Clone();
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessTuplizer(this);
        }

        public TuplizerMode Mode
        {
            get { return attributes.Get<TuplizerMode>(Attr.Mode); }
            set { attributes.Set(Attr.Mode, value); }
        }

        public TypeReference Type
        {
            get { return attributes.Get<TypeReference>(Attr.Type); }
            set { attributes.Set(Attr.Type, value); }
        }

        public override bool IsSpecified(Attr property)
        {
            return attributes.HasUserValue(property);            
        }

        public bool HasValue(Attr property)
        {
            return attributes.HasAnyValue(property);
        }

        public bool Equals(TuplizerMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.attributes, attributes);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(TuplizerMapping)) return false;
            return Equals((TuplizerMapping)obj);
        }

        public override int GetHashCode()
        {
            return (attributes != null ? attributes.GetHashCode() : 0);
        }
    }

    public enum TuplizerMode
    {
        Poco,
        Xml,
        DynamicMap
    }
}