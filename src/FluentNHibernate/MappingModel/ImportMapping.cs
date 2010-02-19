using System;
using System.Linq.Expressions;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel
{
    public class ImportMapping : MappingBase
    {
        private readonly AttributeStore attributes = new AttributeStore();

        public ImportMapping()
            : this(new AttributeStore())
        {}

        public ImportMapping(AttributeStore underlyingStore)
        {
            attributes = underlyingStore.Clone();
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessImport(this);
        }

        public string Rename
        {
            get { return attributes.Get(Attr.Rename); }
            set { attributes.Set(Attr.Rename, value); }
        }

        public TypeReference Class
        {
            get { return attributes.Get<TypeReference>(Attr.Class); }
            set { attributes.Set(Attr.Class, value); }
        }

        public override bool IsSpecified(Attr property)
        {
            return attributes.HasUserValue(property);
        }

        public bool HasValue(Attr property)
        {
            return attributes.HasAnyValue(property);
        }

        public bool Equals(ImportMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.attributes, attributes);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(ImportMapping)) return false;
            return Equals((ImportMapping)obj);
        }

        public override int GetHashCode()
        {
            return (attributes != null ? attributes.GetHashCode() : 0);
        }
    }
}