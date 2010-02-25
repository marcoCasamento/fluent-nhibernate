using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.Identity
{
    public class KeyPropertyMapping : MappingBase
    {
        private readonly IList<ColumnMapping> columns = new List<ColumnMapping>();

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessKeyProperty(this);

            foreach (var column in columns)
                visitor.Visit(column);
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

        public IEnumerable<ColumnMapping> Columns
        {
            get { return columns; }
        }

        public Type ContainingEntityType { get; set; }

        public void AddColumn(ColumnMapping mapping)
        {
            columns.Add(mapping);
        }

        public bool Equals(KeyPropertyMapping other)
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
            if (obj.GetType() != typeof(KeyPropertyMapping)) return false;
            return Equals((KeyPropertyMapping)obj);
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