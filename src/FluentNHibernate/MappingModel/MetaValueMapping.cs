using System;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel
{
    public class MetaValueMapping : MappingBase
    {
        public MetaValueMapping()
            : this(null)
        {}

        protected MetaValueMapping(AttributeStore underlyingStore)
        {
            if (underlyingStore != null)
                ReplaceAttributes(underlyingStore);
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessMetaValue(this);
        }

        public string Value
        {
            get { return (string)GetAttribute(Attr.Value); }
            set { SetAttribute(Attr.Value, value); }
        }

        public TypeReference Class
        {
            get { return (TypeReference)GetAttribute(Attr.Class); }
            set { SetAttribute(Attr.Class, value); }
        }

        public Type ContainingEntityType { get; set; }

        public bool Equals(MetaValueMapping other)
        {
            return base.Equals(other) && Equals(other.ContainingEntityType, ContainingEntityType);
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(MetaValueMapping)) return false;
            return Equals((MetaValueMapping)obj);
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