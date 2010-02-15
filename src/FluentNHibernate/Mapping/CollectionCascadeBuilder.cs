using System;

namespace FluentNHibernate.Mapping
{
    public class CollectionCascadeBuilder<TParent> : CascadeBuilder<TParent>
	{
        private readonly TParent parent;
        readonly CascadeBuilder innerBuilder;

        public CollectionCascadeBuilder(TParent parent, CascadeBuilder innerBuilder)
			: base(parent, innerBuilder)
        {
            this.parent = parent;
            this.innerBuilder = innerBuilder;
        }

        public TParent AllDeleteOrphan()
		{
            innerBuilder.AllDeleteOrphan();
			return parent;
		}

        public TParent DeleteOrphan()
        {
            innerBuilder.DeleteOrphan();
            return parent;
        }
	}
}
