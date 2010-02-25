using System;
using System.Linq.Expressions;
using System.Reflection;
using FluentNHibernate.Utils;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.Collections
{
    public class IndexManyToManyMapping : MappingBase, IIndexMapping, IHasColumnMappings
    {
        private readonly AttributeStore attributes;
        private readonly IDefaultableList<ColumnMapping> columns = new DefaultableList<ColumnMapping>();

        public IndexManyToManyMapping()
            : this(new AttributeStore())
        {}

        public IndexManyToManyMapping(AttributeStore underlyingStore)
        {
            attributes = underlyingStore.Clone();
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessIndex(this);

            foreach (var column in columns)
                visitor.Visit(column);
        }

        public Type ContainingEntityType { get; set; }

        public TypeReference Class
        {
            get { return attributes.Get<TypeReference>(Attr.Class); }
            set { attributes.Set(Attr.Class, value); }
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

        public string ForeignKey
        {
            get { return attributes.Get(Attr.ForeignKey); }
            set { attributes.Set(Attr.ForeignKey, value); }
        }

        public string EntityName
        {
            get { return attributes.Get(Attr.EntityName); }
            set { attributes.Set(Attr.EntityName, value); }
        }     

        public bool Equals(IndexManyToManyMapping other)
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
            if (obj.GetType() != typeof(IndexManyToManyMapping)) return false;
            return Equals((IndexManyToManyMapping)obj);
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