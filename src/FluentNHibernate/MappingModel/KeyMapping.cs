using System;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel
{
    public class KeyMapping : MappingBase, IHasColumnMappings
    {
        private readonly IDefaultableList<ColumnMapping> columns = new DefaultableList<ColumnMapping>();
        public Type ContainingEntityType { get; set; }

        public KeyMapping()
            : this(new AttributeStore())
        {}

        public KeyMapping(AttributeStore underlyingStore)
        {
            if (underlyingStore != null)
                ReplaceAttributes(underlyingStore);
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessKey(this);

            foreach (var column in columns)
                visitor.Visit(column);
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

        public string OnDelete
        {
            get { return (string)GetAttribute(Attr.OnDelete); }
            set { SetAttribute(Attr.OnDelete, value); }
        }

        public bool NotNull
        {
            get { return (bool)GetAttribute(Attr.NotNull); }
            set { SetAttribute(Attr.NotNull, value); }
        }

        public bool Update
        {
            get { return (bool)GetAttribute(Attr.Update); }
            set { SetAttribute(Attr.Update, value); }
        }

        public bool Unique
        {
            get { return (bool)GetAttribute(Attr.Unique); }
            set { SetAttribute(Attr.Unique, value); }
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

        public bool Equals(KeyMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) &&
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
                int result = base.GetHashCode();
                result = (result * 397) ^ (columns != null ? columns.GetHashCode() : 0);
                result = (result * 397) ^ (ContainingEntityType != null ? ContainingEntityType.GetHashCode() : 0);
                return result;
            }
        }
    }
}