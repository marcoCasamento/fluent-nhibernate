using System;

namespace FluentNHibernate.Mapping
{
    public class CascadeBuilder
    {
        private readonly Action<string> setter;

        public CascadeBuilder(Action<string> setter)
        {
            this.setter = setter;
        }

        public void All()
        {
            setter("all");
        }

        public void None()
        {
            setter("none");
        }

        public void SaveUpdate()
        {
            setter("save-update");
        }

        public void Delete()
        {
            setter("delete");
        }

        public void AllDeleteOrphan()
        {
            setter("all-delete-orphan");
        }

        public void DeleteOrphan()
        {
            setter("delete-orphan");
        }
    }

    public class CascadeBuilder<TParent>
	{
        private readonly TParent parent;
        readonly CascadeBuilder innerBuilder;

        public CascadeBuilder(TParent parent, CascadeBuilder innerBuilder)
        {
            this.parent = parent;
            this.innerBuilder = innerBuilder;
        }

        public TParent All()
		{
			innerBuilder.All();
			return parent;
		}

		public TParent None()
		{
			innerBuilder.None();
            return parent;
		}

		public TParent SaveUpdate()
		{
			innerBuilder.SaveUpdate();
            return parent;
		}

		public TParent Delete()
		{
			innerBuilder.Delete();
            return parent;
		}
	}
}
