using System;
using System.Linq.Expressions;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel
{
    public class ParentMapping : MappingBase
    {
        public ParentMapping()
            : this(null)
        {}

        protected ParentMapping(AttributeStore underlyingStore)
        {
            if (underlyingStore != null)
                ReplaceAttributes(underlyingStore);
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessParent(this);
        }

        public string Name
        {
            get { return (string)GetAttribute(Attr.Name); }
            set { SetAttribute(Attr.Name, value); }
        }

        public Type ContainingEntityType { get; set; }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(ParentMapping)) return false;
            
            return Equals((ParentMapping)obj);
        }

        public bool Equals(ParentMapping other)
        {
            return base.Equals(other) && Equals(other.ContainingEntityType, ContainingEntityType);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode() * 397) ^
                    (ContainingEntityType != null ? ContainingEntityType.GetHashCode() : 0);
            }
        }
    }
}