using System;
using FluentNHibernate.Mapping;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.Utils;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel
{
    public class PropertyMapping : ColumnBasedMappingBase
    {
        private readonly MemberTypeSelector memberTypeSelector = new MemberTypeSelector();
        private readonly MemberNameFormatter memberNameFormatter = new MemberNameFormatter();

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
            get { return (string)GetAttribute(Attr.Name); }
            set { SetAttribute(Attr.Name, value); }
        }

        public string Access
        {
            get { return (string)GetAttribute(Attr.Access); }
            set { SetAttribute(Attr.Access, value); }
        }

        public bool Insert
        {
            get { return (bool)GetAttribute(Attr.Insert); }
            set { SetAttribute(Attr.Insert, value); }
        }

        public bool Update
        {
            get { return (bool)GetAttribute(Attr.Update); }
            set { SetAttribute(Attr.Update, value); }
        }

        public string Formula
        {
            get { return (string)GetAttribute(Attr.Formula); }
            set { SetAttribute(Attr.Formula, value); }
        }

        public bool Lazy
        {
            get { return (bool)GetAttribute(Attr.Lazy); }
            set { SetAttribute(Attr.Lazy, value); }
        }

        public bool OptimisticLock
        {
            get { return (bool)GetAttribute(Attr.OptimisticLock); }
            set { SetAttribute(Attr.OptimisticLock, value); }
        }

        public string Generated
        {
            get { return (string)GetAttribute(Attr.Generated); }
            set { SetAttribute(Attr.Generated, value); }
        }

        public TypeReference Type
        {
            get { return (TypeReference)GetAttribute(Attr.Type); }
            set { SetAttribute(Attr.Type, value); }
        }

        public Member Member { get; private set; }

        public void SetMember(Member member)
        {
            Member = member;
            SetDefaultAttribute(Attr.Name, memberNameFormatter.Format(member));
            SetDefaultAttribute(Attr.Type, memberTypeSelector.GetType(member));
        }

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