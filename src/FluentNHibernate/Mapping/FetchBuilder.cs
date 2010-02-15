using System;

namespace FluentNHibernate.Mapping
{
    public class FetchBuilder
    {
        private readonly Action<string> setter;

        public FetchBuilder(Action<string> setter)
        {
            this.setter = setter;
        }

        public void Join()
        {
            setter("join");
        }

        public void Select()
        {
            setter("select");
        }

        public void Subselect()
        {
            setter("subselect");
        }
    }

	public class FetchBuilder<TParent> 
	{
	    private readonly TParent parent;
	    readonly FetchBuilder innerBuilder;

	    public FetchBuilder(TParent parent, FetchBuilder innerBuilder)
		{
		    this.parent = parent;
		    this.innerBuilder = innerBuilder;
		}

	    public TParent Join()
		{
		    innerBuilder.Join();
            return parent;
		}

		public TParent Select()
		{
		    innerBuilder.Select();
            return parent;
		}

        public TParent Subselect()
        {
            innerBuilder.Subselect();
            return parent;
        }
	}
}
