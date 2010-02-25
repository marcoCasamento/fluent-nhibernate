using System;
using System.Linq.Expressions;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.Collections
{
    public class BagMapping : CollectionMappingBase
    {
        public BagMapping()
            : this(null)
        {}

        public BagMapping(AttributeStore underlyingStore)
            : base(underlyingStore)
        {
            if (underlyingStore != null)
                ReplaceAttributes(underlyingStore);
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessBag(this);
            base.AcceptVisitor(visitor);
        }

        public override string OrderBy
        {
            get { return (string)GetAttribute(Attr.OrderBy); }
            set { SetAttribute(Attr.OrderBy, value); }
        }
    }
}
