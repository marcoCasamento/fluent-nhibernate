using System;
using System.Linq.Expressions;
using FluentNHibernate.Utils;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.Collections
{
    public class SetMapping : CollectionMappingBase
    {
        public SetMapping()
            : this(null)
        {}
        
        public SetMapping(AttributeStore underlyingStore)
            : base(underlyingStore)
        {
            if (underlyingStore != null)
                ReplaceAttributes(underlyingStore);
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessSet(this);
            base.AcceptVisitor(visitor);
        }

        public override string OrderBy
        {
            get { return (string)GetAttribute(Attr.OrderBy); }
            set { SetAttribute(Attr.OrderBy, value); }
        }

        public string Sort
        {
            get { return (string)GetAttribute(Attr.Sort); }
            set { SetAttribute(Attr.Sort, value); }
        }
    }
}