using System;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.Collections
{
    public class ArrayMapping : CollectionMappingBase, IIndexedCollectionMapping
    {
        readonly Member member;
        private readonly ValueStore values;

        public IIndexMapping Index { get; set; }

        public ArrayMapping(Member member)
            : this(member, new ValueStore())
        {}

        ArrayMapping(Member member, ValueStore values)
            : base(member, values)
        {
            this.member = member;
            this.values = values;
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessArray(this);

            if (Index != null)
                visitor.Visit(Index);

            base.AcceptVisitor(visitor);
        }

    	public override string OrderBy
    	{
			get { return null; }
    		set { /* no-op */  }
    	}

        public new bool IsSpecified(string property)
        {
            return false;
        }

        public bool Equals(ArrayMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && Equals(other.values, values);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as ArrayMapping);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                {
                    return (base.GetHashCode() * 397) ^ (values != null ? values.GetHashCode() : 0);
                }
            }
        }

        public override void AddChild(IMapping child)
        {
        }
    }
}