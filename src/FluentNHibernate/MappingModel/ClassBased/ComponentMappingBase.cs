using System;
using System.Linq.Expressions;
using System.Reflection;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.ClassBased
{
    public abstract class ComponentMappingBase : ClassMappingBase, IComponentMapping
    {
        private AttributeStore attributes;

        protected ComponentMappingBase(AttributeStore store)
        {
            attributes = store.Clone();
            attributes.SetDefault(Attr.Unique, false);
            attributes.SetDefault(Attr.Update, true);
            attributes.SetDefault(Attr.Insert, true);
            attributes.SetDefault(Attr.OptimisticLock, true);
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            if (Parent != null)
                visitor.Visit(Parent);

            base.AcceptVisitor(visitor);
        }

        public Type ContainingEntityType { get; set; }
        public Member Member { get; set; }

        public ParentMapping Parent
        {
            get { return attributes.Get<ParentMapping>(Attr.Parent); }
            set { attributes.Set(Attr.Parent, value); }
        }

        public bool Unique
        {
            get { return attributes.Get<bool>(Attr.Unique); }
            set { attributes.Set(Attr.Unique, value); }
        }

        public bool Insert
        {
            get { return attributes.Get<bool>(Attr.Insert); }
            set { attributes.Set(Attr.Insert, value); }
        }

        public bool Update
        {
            get { return attributes.Get<bool>(Attr.Update); }
            set { attributes.Set(Attr.Update, value); }
        }

        public string Access
        {
            get { return attributes.Get(Attr.Access); }
            set { attributes.Set(Attr.Access, value); }
        }

        public bool OptimisticLock
        {
            get { return attributes.Get<bool>(Attr.OptimisticLock); }
            set { attributes.Set(Attr.OptimisticLock, value); }
        }

        public bool Equals(ComponentMappingBase other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) &&
                Equals(other.attributes, attributes) &&
                Equals(other.ContainingEntityType, ContainingEntityType) &&
                Equals(other.Member, Member);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as ComponentMappingBase);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = base.GetHashCode();
                result = (result * 397) ^ (attributes != null ? attributes.GetHashCode() : 0);
                result = (result * 397) ^ (ContainingEntityType != null ? ContainingEntityType.GetHashCode() : 0);
                result = (result * 397) ^ (Member != null ? Member.GetHashCode() : 0);
                return result;
            }
        }
    }
}