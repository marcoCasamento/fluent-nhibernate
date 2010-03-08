using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel
{
    public class ColumnMapping : MappingBase, IMapping
    {
        readonly ValueStore values = new ValueStore();

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessColumn(this);
        }

        public Member Member { get; set; }

        public string Name
        {
            get { return values.Get(Attr.Name); }
            set { values.Set(Attr.Name, value); }
        }

        public int Length
        {
            get { return values.Get<int>(Attr.Length); }
            set { values.Set(Attr.Length, value); }
        }

        public bool NotNull
        {
            get { return values.Get<bool>(Attr.NotNull); }
            set { values.Set(Attr.NotNull, value); }
        }

        public bool Unique
        {
            get { return values.Get<bool>(Attr.Unique); }
            set { values.Set(Attr.Unique, value); }
        }

        public string UniqueKey
        {
            get { return values.Get(Attr.UniqueKey); }
            set { values.Set(Attr.UniqueKey, value); }
        }

        public string SqlType
        {
            get { return values.Get(Attr.SqlType); }
            set { values.Set(Attr.SqlType, value); }
        }

        public string Index
        {
            get { return values.Get(Attr.Index); }
            set { values.Set(Attr.Index, value); }
        }

        public string Check
        {
            get { return values.Get(Attr.Check); }
            set { values.Set(Attr.Check, value); }
        }

        public int Precision
        {
            get { return values.Get<int>(Attr.Precision); }
            set { values.Set(Attr.Precision, value); }
        }

        public int Scale
        {
            get { return values.Get<int>(Attr.Scale); }
            set { values.Set(Attr.Scale, value); }
        }

        public string Default
        {
            get { return values.Get(Attr.Default); }
            set { values.Set(Attr.Default, value); }
        }

        public override bool IsSpecified(string property)
        {
            return false;
        }

        public bool HasValue(Attr attr)
        {
            return values.HasValue(attr);
        }

        public bool Equals(ColumnMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.values, values) && Equals(other.Member, Member);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(ColumnMapping)) return false;
            return Equals((ColumnMapping)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((values != null ? values.GetHashCode() : 0) * 397) ^ (Member != null ? Member.GetHashCode() : 0);
            }
        }

        public void AddChild(IMapping child)
        {
        }

        public void UpdateValues(IEnumerable<KeyValuePair<Attr, object>> otherValues)
        {
            values.Merge(otherValues);
        }
    }
}