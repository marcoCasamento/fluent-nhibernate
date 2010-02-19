using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel
{
    public class ColumnMapping : MappingBase
    {
        private readonly AttributeStore attributes;

        public ColumnMapping()
            : this(new AttributeStore())
        {}

        public ColumnMapping(AttributeStore underlyingStore)
        {
            attributes = underlyingStore.Clone();
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessColumn(this);
        }

        public Member Member { get; set; }

        public string Name
        {
            get { return attributes.Get(Attr.Name); }
            set { attributes.Set(Attr.Name, value); }
        }

        public int Length
        {
            get { return attributes.Get<int>(Attr.Length); }
            set { attributes.Set(Attr.Length, value); }
        }

        public bool NotNull
        {
            get { return attributes.Get<bool>(Attr.NotNull); }
            set { attributes.Set(Attr.NotNull, value); }
        }

        public bool Unique
        {
            get { return attributes.Get<bool>(Attr.Unique); }
            set { attributes.Set(Attr.Unique, value); }
        }

        public string UniqueKey
        {
            get { return attributes.Get(Attr.UniqueKey); }
            set { attributes.Set(Attr.UniqueKey, value); }
        }

        public string SqlType
        {
            get { return attributes.Get(Attr.SqlType); }
            set { attributes.Set(Attr.SqlType, value); }
        }

        public string Index
        {
            get { return attributes.Get(Attr.Index); }
            set { attributes.Set(Attr.Index, value); }
        }

        public string Check
        {
            get { return attributes.Get(Attr.Check); }
            set { attributes.Set(Attr.Check, value); }
        }

        public int Precision
        {
            get { return attributes.Get<int>(Attr.Precision); }
            set { attributes.Set(Attr.Precision, value); }
        }

        public int Scale
        {
            get { return attributes.Get<int>(Attr.Scale); }
            set { attributes.Set(Attr.Scale, value); }
        }

        public string Default
        {
            get { return attributes.Get(Attr.Default); }
            set { attributes.Set(Attr.Default, value); }
        }

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

        internal void MergeAttributes(AttributeStore store)
        {
            attributes.Merge(store);
        }

        public ColumnMapping Clone()
        {
            return new ColumnMapping(attributes.Clone());
        }

        public bool Equals(ColumnMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.attributes, attributes) && Equals(other.Member, Member);
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
                return ((attributes != null ? attributes.GetHashCode() : 0) * 397) ^ (Member != null ? Member.GetHashCode() : 0);
            }
        }
    }
}