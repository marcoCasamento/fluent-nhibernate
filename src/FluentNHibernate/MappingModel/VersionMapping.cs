using System;
using System.Linq.Expressions;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.Utils;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel
{
    public class VersionMapping : ColumnBasedMappingBase
    {
        private readonly MemberNameFormatter memberNameFormatter = new MemberNameFormatter();
        private readonly MemberTypeSelector memberTypeSelector = new MemberTypeSelector();

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
            get { return (string)GetAttribute(Attr.Name); }
            set { SetAttribute(Attr.Name, value); }
        }

        public string Access
        {
            get { return (string)GetAttribute(Attr.Access); }
            set { SetAttribute(Attr.Access, value); }
        }

        public TypeReference Type
        {
            get { return (TypeReference)GetAttribute(Attr.Type); }
            set { SetAttribute(Attr.Type, value); }
        }

        public string UnsavedValue
        {
            get { return (string)GetAttribute(Attr.UnsavedValue); }
            set { SetAttribute(Attr.UnsavedValue, value); }
        }

        public string Generated
        {
            get { return (string)GetAttribute(Attr.Generated); }
            set { SetAttribute(Attr.Generated, value); }
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

        public void SetMember(Member member)
        {
            Member = member;
            SetDefaultAttribute(Attr.Name, memberNameFormatter.Format(member));
            SetDefaultAttribute(Attr.Type, GetMemberType(member));
        }

        private bool IsSqlTimestamp(Member property)
        {
            return property.PropertyType == typeof(byte[]);
        }

        private bool IsTimestamp(Member member)
        {
            return member.PropertyType == typeof(DateTime);
        }

        private TypeReference GetMemberType(Member member)
        {
            if (IsSqlTimestamp(member))
                return new TypeReference("BinaryBlob");
            if (IsTimestamp(member))
                return new TypeReference("timestamp");

            return memberTypeSelector.GetType(member);
        }

        public Member Member { get; private set; }
    }
}