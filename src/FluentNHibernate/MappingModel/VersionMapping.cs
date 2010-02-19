using System;
using System.Linq.Expressions;
using FluentNHibernate.Utils;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel
{
    public class VersionMapping : ColumnBasedMappingBase
    {
        public VersionMapping()
            : this(new AttributeStore())
        {}

        public VersionMapping(AttributeStore underlyingStore)
            : base(underlyingStore)
        {}

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessVersion(this);

            columns.Each(visitor.Visit);
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

        public TypeReference Type
        {
            get { return attributes.Get<TypeReference>(Attr.Type); }
            set { attributes.Set(Attr.Type, value); }
        }

        public string UnsavedValue
        {
            get { return attributes.Get(Attr.UnsavedValue); }
            set { attributes.Set(Attr.UnsavedValue, value); }
        }

        public string Generated
        {
            get { return attributes.Get(Attr.Generated); }
            set { attributes.Set(Attr.Generated, value); }
        }

        public Type ContainingEntityType { get; set; }

        public bool Equals(VersionMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && Equals(other.ContainingEntityType, ContainingEntityType);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as VersionMapping);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                {
                    return (base.GetHashCode() * 397) ^ (ContainingEntityType != null ? ContainingEntityType.GetHashCode() : 0);
                }
            }
        }
    }
}