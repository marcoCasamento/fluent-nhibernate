using System;
using System.Linq.Expressions;
using System.Reflection;
using FluentNHibernate.Utils;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel
{
    public class KeyMapping : MappingBase, IHasColumnMappings
    {
        private readonly AttributeStore attributes;
        private readonly IDefaultableList<ColumnMapping> columns = new DefaultableList<ColumnMapping>();
        public Type ContainingEntityType { get; set; }

        public KeyMapping()
            : this(new AttributeStore())
        {}

        public KeyMapping(AttributeStore underlyingStore)
        {
            attributes = underlyingStore.Clone();
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessKey(this);

            foreach (var column in columns)
                visitor.Visit(column);
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

        public string OnDelete
        {
            get { return attributes.Get(Attr.OnDelete); }
            set { attributes.Set(Attr.OnDelete, value); }
        }

        public bool NotNull
        {
            get { return attributes.Get<bool>(Attr.NotNull); }
            set { attributes.Set(Attr.NotNull, value); }
        }

        public bool Update
        {
            get { return attributes.Get<bool>(Attr.Update); }
            set { attributes.Set(Attr.Update, value); }
        }

        public bool Unique
        {
            get { return attributes.Get<bool>(Attr.Unique); }
            set { attributes.Set(Attr.Unique, value); }
        }

        public IDefaultableEnumerable<ColumnMapping> Columns
        {
            get { return columns; }
        }

        public void AddColumn(ColumnMapping mapping)
        {
            columns.Add(mapping);
        }

        public void AddDefaultColumn(ColumnMapping mapping)
        {
            columns.AddDefault(mapping);
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

        public bool Equals(KeyMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.attributes, attributes) &&
                other.columns.ContentEquals(columns) &&
                Equals(other.ContainingEntityType, ContainingEntityType);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(KeyMapping)) return false;
            return Equals((KeyMapping)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = (attributes != null ? attributes.GetHashCode() : 0);
                result = (result * 397) ^ (columns != null ? columns.GetHashCode() : 0);
                result = (result * 397) ^ (ContainingEntityType != null ? ContainingEntityType.GetHashCode() : 0);
                return result;
            }
        }
    }
}