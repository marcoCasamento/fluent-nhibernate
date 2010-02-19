using System;
using FluentNHibernate.Utils;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel
{
    public class PropertyMapping : ColumnBasedMappingBase
    {
        public PropertyMapping()
            : this(new AttributeStore())
        {}

        public PropertyMapping(AttributeStore underlyingStore)
            : base(underlyingStore)
        {}

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessProperty(this);

            foreach (var column in columns)
                visitor.Visit(column);
        }

        public Type ContainingEntityType { get; set; }

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

        public string Formula
        {
            get { return attributes.Get(Attr.Formula); }
            set { attributes.Set(Attr.Formula, value); }
        }

        public bool Lazy
        {
            get { return attributes.Get<bool>(Attr.Lazy); }
            set { attributes.Set(Attr.Lazy, value); }
        }

        public bool OptimisticLock
        {
            get { return attributes.Get<bool>(Attr.OptimisticLock); }
            set { attributes.Set(Attr.OptimisticLock, value); }
        }

        public string Generated
        {
            get { return attributes.Get(Attr.Generated); }
            set { attributes.Set(Attr.Generated, value); }
        }

        public TypeReference Type
        {
            get { return attributes.Get<TypeReference>(Attr.Type); }
            set { attributes.Set(Attr.Type, value); }
        }

        public Member Member { get; set; }

        public bool Equals(PropertyMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) &&
                Equals(other.ContainingEntityType, ContainingEntityType) &&
                Equals(other.Member, Member);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(PropertyMapping)) return false;
            return Equals((PropertyMapping)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((ContainingEntityType != null ? ContainingEntityType.GetHashCode() : 0) * 397) ^ (Member != null ? Member.GetHashCode() : 0);
            }
        }
    }
}