using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.Identity
{
    public class KeyManyToOneMapping : MappingBase
    {
        private readonly IList<ColumnMapping> columns = new List<ColumnMapping>();

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessKeyManyToOne(this);

            foreach (var column in columns)
                visitor.Visit(column);
        }

        public string Access
        {
            get { return (string)GetAttribute(Attr.Access); }
            set { SetAttribute(Attr.Access, value); }
        }

        public string Name
        {
            get { return (string)GetAttribute(Attr.Name); }
            set { SetAttribute(Attr.Name, value); }
        }

        public TypeReference Class
        {
            get { return (TypeReference)GetAttribute(Attr.Class); }
            set { SetAttribute(Attr.Class, value); }
        }

        public string ForeignKey
        {
            get { return (string)GetAttribute(Attr.ForeignKey); }
            set { SetAttribute(Attr.ForeignKey, value); }
        }

        public bool Lazy
        {
            get { return (bool)GetAttribute(Attr.Lazy); }
            set { SetAttribute(Attr.Lazy, value); }
        }

        public string NotFound
        {
            get { return (string)GetAttribute(Attr.NotFound); }
            set { SetAttribute(Attr.NotFound, value); }
        }

        public string EntityName
        {
            get { return (string)GetAttribute(Attr.EntityName); }
            set { SetAttribute(Attr.EntityName, value); }
        }

        public IEnumerable<ColumnMapping> Columns
        {
            get
            {
                return columns;
            }
        }

        public Type ContainingEntityType { get; set; }

        public void AddColumn(ColumnMapping mapping)
        {
            columns.Add(mapping);
        }

        public bool Equals(KeyManyToOneMapping other)
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
            if (obj.GetType() != typeof(KeyManyToOneMapping)) return false;
            return Equals((KeyManyToOneMapping)obj);
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