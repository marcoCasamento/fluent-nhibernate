using System;
using System.Collections.Generic;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel
{
    public class AnyMapping : MappingBase
    {
        private readonly IDefaultableList<ColumnMapping> typeColumns = new DefaultableList<ColumnMapping>();
        private readonly IDefaultableList<ColumnMapping> identifierColumns = new DefaultableList<ColumnMapping>();
        private readonly IList<MetaValueMapping> metaValues = new List<MetaValueMapping>();

        public AnyMapping()
            : this(null)
        {}

        public AnyMapping(AttributeStore underlyingStore)
        {
            if (underlyingStore != null)
                ReplaceAttributes(underlyingStore);

            SetDefaultAttribute(Attr.Insert, true);
            SetDefaultAttribute(Attr.Update, true);
            SetDefaultAttribute(Attr.OptimisticLock, true);
            SetDefaultAttribute(Attr.Lazy, false);
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessAny(this);

            foreach (var metaValue in metaValues)
                visitor.Visit(metaValue);

            foreach (var column in typeColumns)
                visitor.Visit(column);

            foreach (var column in identifierColumns)
                visitor.Visit(column);
        }

        public string Name
        {
            get { return GetAttribute<string>(Attr.Name); }
            set { SetAttribute(Attr.Name, value); }
        }

        public string IdType
        {
            get { return GetAttribute<string>(Attr.IdType); }
            set { SetAttribute(Attr.IdType, value); }
        }

        public TypeReference MetaType
        {
            get { return (TypeReference)GetAttribute(Attr.MetaType); }
            set { SetAttribute(Attr.MetaType, value); }
        }

        public string Access
        {
            get { return (string)GetAttribute(Attr.Access); }
            set { SetAttribute(Attr.Access, value); }
        }

        public bool Insert
        {
            get { return (bool)GetAttribute(Attr.Insert); }
            set { SetAttribute(Attr.Insert, value); }
        }

        public bool Update
        {
            get { return (bool)GetAttribute(Attr.Update); }
            set { SetAttribute(Attr.Update, value); }
        }

        public string Cascade
        {
            get { return (string)GetAttribute(Attr.Cascade); }
            set { SetAttribute(Attr.Cascade, value); }
        }

        public bool Lazy
        {
            get { return (bool)GetAttribute(Attr.Lazy); }
            set { SetAttribute(Attr.Lazy, value); }
        }

        public bool OptimisticLock
        {
            get { return (bool)GetAttribute(Attr.OptimisticLock); }
            set { SetAttribute(Attr.OptimisticLock, value); }
        }

        public IDefaultableEnumerable<ColumnMapping> TypeColumns
        {
            get { return typeColumns; }
        }

        public IDefaultableEnumerable<ColumnMapping> IdentifierColumns
        {
            get { return identifierColumns; }
        }

        public IEnumerable<MetaValueMapping> MetaValues
        {
            get { return metaValues; }
        }

        public Type ContainingEntityType { get; set; }

        public void AddTypeDefaultColumn(ColumnMapping column)
        {
            typeColumns.AddDefault(column);
        }

        public void AddTypeColumn(ColumnMapping column)
        {
            typeColumns.Add(column);
        }

        public void AddIdentifierDefaultColumn(ColumnMapping column)
        {
            identifierColumns.AddDefault(column);
        }

        public void AddIdentifierColumn(ColumnMapping column)
        {
            identifierColumns.Add(column);
        }

        public void AddMetaValue(MetaValueMapping metaValue)
        {
            metaValues.Add(metaValue);
        }

        public bool Equals(AnyMapping other)
        {
            return base.Equals(other) &&
                other.typeColumns.ContentEquals(typeColumns) &&
                other.identifierColumns.ContentEquals(identifierColumns) &&
                other.metaValues.ContentEquals(metaValues) &&
                Equals(other.ContainingEntityType, ContainingEntityType);
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(AnyMapping)) return false;
            return Equals((AnyMapping)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var result = base.GetHashCode();
                result = (result * 397) ^ (typeColumns != null ? typeColumns.GetHashCode() : 0);
                result = (result * 397) ^ (identifierColumns != null ? identifierColumns.GetHashCode() : 0);
                result = (result * 397) ^ (metaValues != null ? metaValues.GetHashCode() : 0);
                result = (result * 397) ^ (ContainingEntityType != null ? ContainingEntityType.GetHashCode() : 0);
                return result;
            }
        }
    }
}