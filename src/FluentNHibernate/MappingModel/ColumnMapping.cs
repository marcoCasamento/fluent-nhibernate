using System;
using FluentNHibernate.Utils;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel
{
    public class ColumnMapping : MappingBase
    {
        public ColumnMapping()
            : this(null)
        {}

        public ColumnMapping(AttributeStore underlyingStore)
        {
            if (underlyingStore != null)
                ReplaceAttributes(underlyingStore);
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessColumn(this);
        }

        public Member Member { get; set; }

        public string Name
        {
            get { return (string)GetAttribute(Attr.Name); }
            set { SetAttribute(Attr.Name, value); }
        }

        public int Length
        {
            get { return (int)GetAttribute(Attr.Length); }
            set { SetAttribute(Attr.Length, value); }
        }

        public bool NotNull
        {
            get { return GetAttribute<bool>(Attr.NotNull); }
            set { SetAttribute(Attr.NotNull, value); }
        }

        public bool Unique
        {
            get { return (bool)GetAttribute(Attr.Unique); }
            set { SetAttribute(Attr.Unique, value); }
        }

        public string UniqueKey
        {
            get { return (string)GetAttribute(Attr.UniqueKey); }
            set { SetAttribute(Attr.UniqueKey, value); }
        }

        public string SqlType
        {
            get { return (string)GetAttribute(Attr.SqlType); }
            set { SetAttribute(Attr.SqlType, value); }
        }

        public string Index
        {
            get { return (string)GetAttribute(Attr.Index); }
            set { SetAttribute(Attr.Index, value); }
        }

        public string Check
        {
            get { return (string)GetAttribute(Attr.Check); }
            set { SetAttribute(Attr.Check, value); }
        }

        public int Precision
        {
            get { return (int)GetAttribute(Attr.Precision); }
            set { SetAttribute(Attr.Precision, value); }
        }

        public int Scale
        {
            get { return (int)GetAttribute(Attr.Scale); }
            set { SetAttribute(Attr.Scale, value); }
        }

        public string Default
        {
            get { return (string)GetAttribute(Attr.Default); }
            set { SetAttribute(Attr.Default, value); }
        }

        internal void MergeAttributes(AttributeStore store)
        {
            store.Defaults.Each(SetDefaultAttribute);
            store.UserDefined.Each(SetAttribute);
        }

        public ColumnMapping Clone()
        {
            throw new NotImplementedException();
            //return new ColumnMapping(attributes.Clone());
        }

        public bool Equals(ColumnMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && Equals(other.Member, Member);
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
                return (base.GetHashCode() * 397) ^ (Member != null ? Member.GetHashCode() : 0);
            }
        }
    }
}