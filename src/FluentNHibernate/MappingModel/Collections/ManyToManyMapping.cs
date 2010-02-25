using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FluentNHibernate.Utils;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.Collections
{
    public class ManyToManyMapping : MappingBase, ICollectionRelationshipMapping, IHasColumnMappings
    {
        private readonly AttributeStore attributes;
        private readonly IDefaultableList<ColumnMapping> columns = new DefaultableList<ColumnMapping>();
        
        public ManyToManyMapping()
            : this(new AttributeStore())
        {}

        public ManyToManyMapping(AttributeStore underlyingStore)
        {
            attributes = underlyingStore.Clone();
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessManyToMany(this);

            foreach (var column in columns)
                visitor.Visit(column);
        }

        public Type ChildType
        {
            get { return attributes.Get<Type>(Attr.ChildType); }
            set { attributes.Set(Attr.ChildType, value); }
        }

        public Type ParentType
        {
            get { return attributes.Get<Type>(Attr.ParentType); }
            set { attributes.Set(Attr.ParentType, value); }
        }

        public TypeReference Class
        {
            get { return attributes.Get<TypeReference>(Attr.Class); }
            set { attributes.Set(Attr.Class, value); }
        }

        public string ForeignKey
        {
            get { return attributes.Get(Attr.ForeignKey); }
            set { attributes.Set(Attr.ForeignKey, value); }
        }

        public string Fetch
        {
            get { return attributes.Get(Attr.Fetch); }
            set { attributes.Set(Attr.Fetch, value); }
        }

        public string NotFound
        {
            get { return attributes.Get(Attr.NotFound); }
            set { attributes.Set(Attr.NotFound, value); }
        }

        public string Where
        {
            get { return attributes.Get(Attr.Where); }
            set { attributes.Set(Attr.Where, value); }
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

        public Type ContainingEntityType { get; set; }

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

        public bool Equals(ManyToManyMapping other)
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
            if (obj.GetType() != typeof(ManyToManyMapping)) return false;
            return Equals((ManyToManyMapping)obj);
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
