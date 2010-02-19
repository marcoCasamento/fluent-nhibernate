using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using FluentNHibernate.Utils;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.Collections
{
    public class ElementMapping : MappingBase
    {
        private readonly IDefaultableList<ColumnMapping> columns = new DefaultableList<ColumnMapping>();
        private readonly AttributeStore attributes;

        public ElementMapping()
            : this(new AttributeStore())
        {}

        public ElementMapping(AttributeStore underlyingStore)
        {
            attributes = underlyingStore.Clone();
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessElement(this);

            foreach (var column in columns)
                visitor.Visit(column);
        }

        public TypeReference Type
        {
            get { return attributes.Get<TypeReference>(Attr.Type); }
            set { attributes.Set(Attr.Type, value); }
        }

        public string Formula
        {
            get { return attributes.Get(Attr.Formula); }
            set { attributes.Set(Attr.Formula, value); }
        }

        public int Length
        {
            get { return attributes.Get<int>(Attr.Length); }
            set { attributes.Set(Attr.Length, value); }
        }

        public void AddColumn(ColumnMapping mapping)
        {
            columns.Add(mapping);
        }

        public void AddDefaultColumn(ColumnMapping mapping)
        {
            columns.AddDefault(mapping);
        }

        public IEnumerable<ColumnMapping> Columns
        {
            get { return columns; }
        }

        public Type ContainingEntityType { get; set; }

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

        public bool Equals(ElementMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.columns.ContentEquals(columns) &&
                Equals(other.attributes, attributes) &&
                Equals(other.ContainingEntityType, ContainingEntityType);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(ElementMapping)) return false;
            return Equals((ElementMapping)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = (columns != null ? columns.GetHashCode() : 0);
                result = (result * 397) ^ (attributes != null ? attributes.GetHashCode() : 0);
                result = (result * 397) ^ (ContainingEntityType != null ? ContainingEntityType.GetHashCode() : 0);
                return result;
            }
        }
    }
}