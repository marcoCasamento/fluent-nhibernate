using System;
using System.Linq.Expressions;
using System.Reflection;
using FluentNHibernate.Utils;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel
{
    public class ManyToOneMapping : MappingBase, IHasColumnMappings
    {
        private readonly AttributeStore attributes;
        private readonly IDefaultableList<ColumnMapping> columns = new DefaultableList<ColumnMapping>();

        public ManyToOneMapping()
            : this(new AttributeStore())
        {}

        public ManyToOneMapping(AttributeStore underlyingStore)
        {
            attributes = underlyingStore.Clone();
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessManyToOne(this);

            foreach (var column in columns)
                visitor.Visit(column);
        }

        public Type ContainingEntityType { get; set; }
        public Member Member { get; set; }

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

        public TypeReference Class
        {
            get { return attributes.Get<TypeReference>(Attr.Class); }
            set { attributes.Set(Attr.Class, value); }
        }

        public string Cascade
        {
            get { return attributes.Get(Attr.Cascade); }
            set { attributes.Set(Attr.Cascade, value); }
        }

        public string Fetch
        {
            get { return attributes.Get(Attr.Fetch); }
            set { attributes.Set(Attr.Fetch, value); }
        }

        public bool Update
        {
            get { return attributes.Get<bool>(Attr.Update); }
            set { attributes.Set(Attr.Update, value); }
        }

        public bool Insert
        {
            get { return attributes.Get<bool>(Attr.Insert); }
            set { attributes.Set(Attr.Insert, value); }
        }

        public string ForeignKey
        {
            get { return attributes.Get(Attr.ForeignKey); }
            set { attributes.Set(Attr.ForeignKey, value); }
        }

        public string PropertyRef
        {
            get { return attributes.Get(Attr.PropertyRef); }
            set { attributes.Set(Attr.PropertyRef, value); }
        }

        public string NotFound
        {
            get { return attributes.Get(Attr.NotFound); }
            set { attributes.Set(Attr.NotFound, value); }
        }

        public bool Lazy
        {
            get { return attributes.Get<bool>(Attr.Lazy); }
            set { attributes.Set(Attr.Lazy, value); }
        }

        public string EntityName
        {
            get { return attributes.Get(Attr.EntityName); }
            set { attributes.Set(Attr.EntityName, value); }
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

        public override bool IsSpecified(Attr property)
        {
            return attributes.HasUserValue(property);
        }

        public bool HasValue(Attr property)
        {
            return attributes.HasAnyValue(property);
        }

        public void SetDefaultValue<TResult>(Attr property, TResult value)
        {
            attributes.SetDefault(property, value);
        }

        public bool Equals(ManyToOneMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.attributes, attributes) &&
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
                int result = (attributes != null ? attributes.GetHashCode() : 0);
                result = (result * 397) ^ (columns != null ? columns.GetHashCode() : 0);
                result = (result * 397) ^ (ContainingEntityType != null ? ContainingEntityType.GetHashCode() : 0);
                result = (result * 397) ^ (Member != null ? Member.GetHashCode() : 0);
                return result;
            }
        }
    }
}