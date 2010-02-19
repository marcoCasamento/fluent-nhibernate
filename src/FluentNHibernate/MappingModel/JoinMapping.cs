using System;
using System.Collections.Generic;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel
{
    public class JoinMapping : IMappingBase
    {
        private readonly AttributeStore attributes;

        private readonly MappedMembers mappedMembers;

        public JoinMapping()
            : this(new AttributeStore())
        {}

        public JoinMapping(AttributeStore underlyingStore)
        {
            attributes = underlyingStore.Clone();
            mappedMembers = new MappedMembers();
        }

        public KeyMapping Key
        {
            get { return attributes.Get<KeyMapping>(Attr.Key); }
            set { attributes.Set(Attr.Key, value); }
        }

        public IEnumerable<PropertyMapping> Properties
        {
            get { return mappedMembers.Properties; }
        }

        public IEnumerable<ManyToOneMapping> References
        {
            get { return mappedMembers.References; }
        }

        public IEnumerable<IComponentMapping> Components
        {
            get { return mappedMembers.Components; }
        }

        public IEnumerable<AnyMapping> Anys
        {
            get { return mappedMembers.Anys; }
        }

        public void AddProperty(PropertyMapping property)
        {
            mappedMembers.AddProperty(property);
        }

        public void AddReference(ManyToOneMapping manyToOne)
        {
            mappedMembers.AddReference(manyToOne);
        }

        public void AddComponent(IComponentMapping componentMapping)
        {
            mappedMembers.AddComponent(componentMapping);
        }

        public void AddAny(AnyMapping mapping)
        {
            mappedMembers.AddAny(mapping);
        }

        public string TableName
        {
            get { return attributes.Get(Attr.Table); }
            set { attributes.Set(Attr.Table, value); }
        }

        public string Schema
        {
            get { return attributes.Get(Attr.Schema); }
            set { attributes.Set(Attr.Schema, value); }
        }

        public string Catalog
        {
            get { return attributes.Get(Attr.Catalog); }
            set { attributes.Set(Attr.Catalog, value); }
        }

        public string Subselect
        {
            get { return attributes.Get(Attr.Subselect); }
            set { attributes.Set(Attr.Subselect, value); }
        }

        public string Fetch
        {
            get { return attributes.Get(Attr.Fetch); }
            set { attributes.Set(Attr.Fetch, value); }
        }

        public bool Inverse
        {
            get { return attributes.Get<bool>(Attr.Inverse); }
            set { attributes.Set(Attr.Inverse, value); }
        }

        public bool Optional
        {
            get { return attributes.Get<bool>(Attr.Optional); }
            set { attributes.Set(Attr.Optional, value); }
        }

        public Type ContainingEntityType { get; set; }

        public void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessJoin(this);

            if (Key != null)
                visitor.Visit(Key);

            mappedMembers.AcceptVisitor(visitor);
        }

        public bool IsSpecified(Attr property)
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

        public bool Equals(JoinMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.attributes, attributes) &&
                Equals(other.mappedMembers, mappedMembers) &&
                Equals(other.ContainingEntityType, ContainingEntityType);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(JoinMapping)) return false;
            return Equals((JoinMapping)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = (attributes != null ? attributes.GetHashCode() : 0);
                result = (result * 397) ^ (mappedMembers != null ? mappedMembers.GetHashCode() : 0);
                result = (result * 397) ^ (ContainingEntityType != null ? ContainingEntityType.GetHashCode() : 0);
                return result;
            }
        }
    }
}
