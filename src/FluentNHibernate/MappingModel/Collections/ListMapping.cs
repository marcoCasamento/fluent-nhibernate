using System;
using System.Linq.Expressions;
using FluentNHibernate.Utils;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.Collections
{
    public class ListMapping : CollectionMappingBase, IIndexedCollectionMapping
    {
        public IIndexMapping Index
        {
            get { return attributes.Get<IIndexMapping>(Attr.Index); }
            set { attributes.Set(Attr.Index, value); }
        }

        public ListMapping()
            : this(new AttributeStore())
        {}

        public ListMapping(AttributeStore underlyingStore)
            : base(underlyingStore)
        {
            attributes = underlyingStore.Clone();
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessList(this);

            if(Index != null)
                visitor.Visit(Index);

            base.AcceptVisitor(visitor);
        }

    	public override string OrderBy
    	{
			get { return null; }
			set { /* no-op */ }
    	}

        public new bool IsSpecified(Attr property)
        {
            return attributes.HasUserValue(property);
        }

        public bool Equals(ListMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && Equals(other.attributes, attributes);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as ListMapping);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                {
                    return (base.GetHashCode() * 397) ^ (attributes != null ? attributes.GetHashCode() : 0);
                }
            }
        }
    }
}
