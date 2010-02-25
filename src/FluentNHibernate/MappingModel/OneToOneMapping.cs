using System;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel
{
    public class OneToOneMapping : MappingBase
    {
        private readonly MemberNameFormatter memberNameFormatter = new MemberNameFormatter();
        private readonly MemberTypeSelector memberTypeSelector = new MemberTypeSelector();

        public OneToOneMapping()
            : this(null)
        {}

        public OneToOneMapping(AttributeStore underlyingStore)
        {
            if (underlyingStore != null)
                ReplaceAttributes(underlyingStore);
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessOneToOne(this);
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

        public TypeReference Class
        {
            get { return (TypeReference)GetAttribute(Attr.Class); }
            set { SetAttribute(Attr.Class, value); }
        }

        public string Cascade
        {
            get { return (string)GetAttribute(Attr.Cascade); }
            set { SetAttribute(Attr.Cascade, value); }
        }

        public bool Constrained
        {
            get { return (bool)GetAttribute(Attr.Constrained); }
            set { SetAttribute(Attr.Constrained, value); }
        }

        public string Fetch
        {
            get { return (string)GetAttribute(Attr.Fetch); }
            set { SetAttribute(Attr.Fetch, value); }
        }

        public string ForeignKey
        {
            get { return (string)GetAttribute(Attr.ForeignKey); }
            set { SetAttribute(Attr.ForeignKey, value); }
        }

        public string PropertyRef
        {
            get { return (string)GetAttribute(Attr.PropertyRef); }
            set { SetAttribute(Attr.PropertyRef, value); }
        }

        public bool Lazy
        {
            get { return (bool)GetAttribute(Attr.Lazy); }
            set { SetAttribute(Attr.Lazy, value); }
        }

        public string EntityName
        {
            get { return (string)GetAttribute(Attr.EntityName); }
            set { SetAttribute(Attr.EntityName, value); }
        }

        public Type ContainingEntityType { get; set; }

        public bool Equals(OneToOneMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && Equals(other.ContainingEntityType, ContainingEntityType);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(OneToOneMapping)) return false;
            return Equals((OneToOneMapping)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode() * 397) ^ (ContainingEntityType != null ? ContainingEntityType.GetHashCode() : 0);
            }
        }

        public void SetMember(Member member)
        {
            Member = member;
            SetDefaultAttribute(Attr.Name, memberNameFormatter.Format(member));
            SetDefaultAttribute(Attr.Class, memberTypeSelector.GetType(member));
        }

        public Member Member { get; private set; }
    }
}