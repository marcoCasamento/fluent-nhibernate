using System;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel
{
    public class ManyToOneMapping : MappingBase, IHasColumnMappings
    {
        private readonly MemberNameFormatter memberNameFormatter = new MemberNameFormatter();
        private readonly MemberTypeSelector memberTypeSelector = new MemberTypeSelector();
        private readonly IDefaultableList<ColumnMapping> columns = new DefaultableList<ColumnMapping>();

        public ManyToOneMapping()
            : this(null)
        {}

        public ManyToOneMapping(AttributeStore underlyingStore)
        {
            if (underlyingStore != null)
                ReplaceAttributes(underlyingStore);
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessManyToOne(this);

            foreach (var column in columns)
                visitor.Visit(column);
        }

        public Type ContainingEntityType { get; set; }
        public Member Member { get; private set; }

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

        public string Fetch
        {
            get { return (string)GetAttribute(Attr.Fetch); }
            set { SetAttribute(Attr.Fetch, value); }
        }

        public bool Update
        {
            get { return (bool)GetAttribute(Attr.Update); }
            set { SetAttribute(Attr.Update, value); }
        }

        public bool Insert
        {
            get { return (bool)GetAttribute(Attr.Insert); }
            set { SetAttribute(Attr.Insert, value); }
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

        public string NotFound
        {
            get { return (string)GetAttribute(Attr.NotFound); }
            set { SetAttribute(Attr.NotFound, value); }
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

        public IDefaultableEnumerable<ColumnMapping> Columns
        {
            get { return columns; }
        }

        public void AddColumn(ColumnMapping column)
        {
            columns.Add(column);
        }

        public void AddDefaultColumn(ColumnMapping column)
        {
            columns.AddDefault(column);
        }

        public void ClearColumns()
        {
            columns.Clear();
        }

        public bool Equals(ManyToOneMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) &&
                other.columns.ContentEquals(columns) &&
                Equals(other.ContainingEntityType, ContainingEntityType) &&
                Equals(other.Member, Member);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(ManyToOneMapping)) return false;
            return Equals((ManyToOneMapping)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = base.GetHashCode();
                result = (result * 397) ^ (columns != null ? columns.GetHashCode() : 0);
                result = (result * 397) ^ (ContainingEntityType != null ? ContainingEntityType.GetHashCode() : 0);
                result = (result * 397) ^ (Member != null ? Member.GetHashCode() : 0);
                return result;
            }
        }

        public void SetMember(Member member)
        {
            Member = member;
            SetDefaultAttribute(Attr.Name, memberNameFormatter.Format(member));
            SetDefaultAttribute(Attr.Class, memberTypeSelector.GetType(member));
        }
    }
}