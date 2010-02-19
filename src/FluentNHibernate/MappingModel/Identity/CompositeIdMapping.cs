using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.Identity
{
    public class CompositeIdMapping : MappingBase, IIdentityMapping
    {
        private readonly AttributeStore attributes;
        private readonly IList<KeyPropertyMapping> keyProperties = new List<KeyPropertyMapping>();
        private readonly IList<KeyManyToOneMapping> keyManyToOnes = new List<KeyManyToOneMapping>();

        public CompositeIdMapping()
            : this(new AttributeStore())
        {}

        public CompositeIdMapping(AttributeStore underlyingStore)
        {
            attributes = underlyingStore.Clone();
            attributes.SetDefault(Attr.Mapped, false);
            attributes.SetDefault(Attr.UnsavedValue, "undefined");
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessCompositeId(this);

            foreach (var key in keyProperties)
                visitor.Visit(key);

            foreach (var key in keyManyToOnes)
                visitor.Visit(key);
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

        public bool Mapped
        {
            get { return attributes.Get<bool>(Attr.Mapped); }
            set { attributes.Set(Attr.Mapped, value); }
        }

        public TypeReference Class
        {
            get { return attributes.Get<TypeReference>(Attr.Class); }
            set { attributes.Set(Attr.Class, value); }
        }

        public string UnsavedValue
        {
            get { return attributes.Get(Attr.UnsavedValue); }
            set { attributes.Set(Attr.UnsavedValue, value); }
        }

        public IEnumerable<KeyPropertyMapping> KeyProperties
        {
            get { return keyProperties; }
        }

        public IEnumerable<KeyManyToOneMapping> KeyManyToOnes
        {
            get { return keyManyToOnes; }
        }

        public Type ContainingEntityType { get; set; }

        public void AddKeyProperty(KeyPropertyMapping mapping)
        {
            keyProperties.Add(mapping);
        }

        public void AddKeyManyToOne(KeyManyToOneMapping mapping)
        {
            keyManyToOnes.Add(mapping);
        }

        public override bool IsSpecified(Attr property)
        {
            return attributes.HasUserValue(property);
        }

        public bool HasValue(Attr property)
        {
            return attributes.HasAnyValue(property);
        }

        public bool Equals(CompositeIdMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.attributes, attributes) &&
                other.keyProperties.ContentEquals(keyProperties) &&
                other.keyManyToOnes.ContentEquals(keyManyToOnes) &&
                Equals(other.ContainingEntityType, ContainingEntityType);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(CompositeIdMapping)) return false;
            return Equals((CompositeIdMapping)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = (attributes != null ? attributes.GetHashCode() : 0);
                result = (result * 397) ^ (keyProperties != null ? keyProperties.GetHashCode() : 0);
                result = (result * 397) ^ (keyManyToOnes != null ? keyManyToOnes.GetHashCode() : 0);
                result = (result * 397) ^ (ContainingEntityType != null ? ContainingEntityType.GetHashCode() : 0);
                return result;
            }
        }
    }
}