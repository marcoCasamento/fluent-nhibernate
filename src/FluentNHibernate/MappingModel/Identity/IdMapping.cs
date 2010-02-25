using System;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.Identity
{
    public class IdMapping : ColumnBasedMappingBase, IIdentityMapping
    {
        private readonly MemberNameFormatter memberNameFormatter = new MemberNameFormatter();
        private readonly MemberTypeSelector memberTypeSelector = new MemberTypeSelector();

        public IdMapping()
            : this(null)
        {}

        public IdMapping(AttributeStore underlyingStore)
            : base(underlyingStore)
        {}

        public Member Member { get; private set; }

        public GeneratorMapping Generator
        {
            get { return (GeneratorMapping)GetAttribute(Attr.Generator); }
            set { SetAttribute(Attr.Generator, value); }
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessId(this);

            foreach (var column in Columns)
                visitor.Visit(column);

            if (Generator != null)
                visitor.Visit(Generator);
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

        public Type ContainingEntityType { get; set; }

        public bool Equals(IdMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && Equals(other.Member, Member) && Equals(other.ContainingEntityType, ContainingEntityType);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as IdMapping);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = base.GetHashCode();
                result = (result * 397) ^ (Member != null ? Member.GetHashCode() : 0);
                result = (result * 397) ^ (ContainingEntityType != null ? ContainingEntityType.GetHashCode() : 0);
                return result;
            }
        }

        public void SetMember(Member member)
        {
            Member = member;
            SetDefaultAttribute(Attr.Name, memberNameFormatter.Format(member));
            SetDefaultAttribute(Attr.Type, Type ?? memberTypeSelector.GetType(member));
        }
    }
}