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
            get { return (IIndexMapping)GetAttribute(Attr.Index); }
            set { SetAttribute(Attr.Index, value); }
        }

        public ListMapping()
            : this(null)
        {}

        public ListMapping(AttributeStore underlyingStore)
            : base(underlyingStore)
        {
            if (underlyingStore != null)
                ReplaceAttributes(underlyingStore);
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
    }
}
