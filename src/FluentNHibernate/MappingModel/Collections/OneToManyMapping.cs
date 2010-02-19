using System;
using System.Linq.Expressions;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.Collections
{
    public class OneToManyMapping : MappingBase, ICollectionRelationshipMapping
    {
        private readonly AttributeStore attributes;

        public OneToManyMapping()
            : this(new AttributeStore())
        {}

        public OneToManyMapping(AttributeStore underlyingStore)
        {
            attributes = underlyingStore.Clone();
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessOneToMany(this);
        }

        public Type ChildType
        {
            get { return attributes.Get<Type>(Attr.ChildType); }
            set { attributes.Set(Attr.ChildType, value); }
        }

        public TypeReference Class
        {
            get { return attributes.Get<TypeReference>(Attr.Class); }
            set { attributes.Set(Attr.Class, value); }
        }

        public string NotFound
        {
            get { return attributes.Get(Attr.NotFound); }
            set { attributes.Set(Attr.NotFound, value); }
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

        public bool Equals(OneToManyMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.attributes, attributes) && Equals(other.ContainingEntityType, ContainingEntityType);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(OneToManyMapping)) return false;
            return Equals((OneToManyMapping)obj);
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