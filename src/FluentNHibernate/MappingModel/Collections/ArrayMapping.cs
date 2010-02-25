using System;
using System.Linq.Expressions;
using System.Reflection;
using FluentNHibernate.Utils;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.Collections
{
    public class ArrayMapping : CollectionMappingBase, IIndexedCollectionMapping
    {
        public IIndexMapping Index
        {
            get { return (IIndexMapping)GetAttribute(Attr.Index); }
            set { SetAttribute(Attr.Index, value); }
        }

        public ArrayMapping()
            : this(null)
        {}

        public ArrayMapping(AttributeStore underlyingStore)
            : base(underlyingStore)
        {
            if (underlyingStore != null)
                ReplaceAttributes(underlyingStore);
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
    }
}
