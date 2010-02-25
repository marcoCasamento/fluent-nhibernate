using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using FluentNHibernate.Mapping;
using FluentNHibernate.Utils;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.Collections
{
    public class CompositeElementMapping : MappingBase
    {
        private readonly MappedMembers mappedMembers;
        protected readonly AttributeStore attributes;

        public CompositeElementMapping()
            : this(new AttributeStore())
        { }

        public CompositeElementMapping(AttributeStore store)
        {
            attributes = store.Clone();
            mappedMembers = new MappedMembers();
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessCompositeElement(this);

            if (Parent != null)
                visitor.Visit(Parent);

            mappedMembers.AcceptVisitor(visitor);
        }

        public TypeReference Class
        {
            get { return attributes.Get<TypeReference>(Attr.Class); }
            set { attributes.Set(Attr.Class, value); }
        }

        public ParentMapping Parent
        {
            get { return attributes.Get<ParentMapping>(Attr.Parent); }
            set { attributes.Set(Attr.Parent, value); }
        }

        public IEnumerable<PropertyMapping> Properties
        {
            get { return mappedMembers.Properties; }
        }

        public void AddProperty(PropertyMapping property)
        {
            mappedMembers.AddProperty(property);
        }

        public IEnumerable<ManyToOneMapping> References
        {
            get { return mappedMembers.References; }
        }

        public Type ContainingEntityType { get; set; }

        public void AddReference(ManyToOneMapping manyToOne)
        {
            mappedMembers.AddReference(manyToOne);
        }

        public bool Equals(CompositeElementMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.mappedMembers, mappedMembers) && Equals(other.attributes, attributes) && Equals(other.ContainingEntityType, ContainingEntityType);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(CompositeElementMapping)) return false;
            return Equals((CompositeElementMapping)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = (mappedMembers != null ? mappedMembers.GetHashCode() : 0);
                result = (result * 397) ^ (attributes != null ? attributes.GetHashCode() : 0);
                result = (result * 397) ^ (ContainingEntityType != null ? ContainingEntityType.GetHashCode() : 0);
                return result;
            }
        }
    }
}