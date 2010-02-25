using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.Collections
{
    public class MapMapping : CollectionMappingBase, IIndexedCollectionMapping
    {
        public IIndexMapping Index
        {
            get { return (IIndexMapping)GetAttribute(Attr.Index); }
            set { SetAttribute(Attr.Index, value); }
        }

        public MapMapping()
            : this(new AttributeStore())
        {}

        public MapMapping(AttributeStore underlyingStore)
            : base(underlyingStore)
        {
            if (underlyingStore != null)
                ReplaceAttributes(underlyingStore);
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessMap(this);

            if (Index != null)
                visitor.Visit(Index);

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