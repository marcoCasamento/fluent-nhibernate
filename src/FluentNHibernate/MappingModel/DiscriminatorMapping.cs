using System;
using System.Linq.Expressions;
using System.Reflection;
using FluentNHibernate.Utils;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel
{
    public class DiscriminatorMapping : ColumnBasedMappingBase
    {
        public DiscriminatorMapping(TypeReference discriminatorType)
            : this(null, discriminatorType)
        {}

        public DiscriminatorMapping(AttributeStore underlyingStore, TypeReference discriminatorType)
            : base(underlyingStore)
        {
            SetDefaultAttribute(Attr.Type, discriminatorType);
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessDiscriminator(this);

            columns.Each(visitor.Visit);
        }

        public bool Force
        {
            get { return (bool)GetAttribute(Attr.Force); }
            set { SetAttribute(Attr.Force, value); }
        }

        public bool Insert
        {
            get { return (bool)GetAttribute(Attr.Insert); }
            set { SetAttribute(Attr.Insert, value); }
        }

        public string Formula
        {
            get { return (string)GetAttribute(Attr.Formula); }
            set { SetAttribute(Attr.Formula, value); }
        }

        public TypeReference Type
        {
            get { return (TypeReference)GetAttribute(Attr.Type); }
            set { SetAttribute(Attr.Type, value); }
        }

        public Type ContainingEntityType { get; set; }

        public bool Equals(DiscriminatorMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.ContainingEntityType, ContainingEntityType) &&
                other.columns.ContentEquals(columns) &&
                base.Equals(other);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(DiscriminatorMapping)) return false;
            return Equals((DiscriminatorMapping)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((ContainingEntityType != null ? ContainingEntityType.GetHashCode() : 0) * 397) ^ ((columns != null ? columns.GetHashCode() : 0) * 397) ^ base.GetHashCode();
            }
        }
    }
}
