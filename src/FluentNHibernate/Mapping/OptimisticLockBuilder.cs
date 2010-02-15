using System;

namespace FluentNHibernate.Mapping
{
    public class OptimisticLockBuilder
    {
        private readonly Action<string> setter;

        public OptimisticLockBuilder(Action<string> setter)
        {
            this.setter = setter;
        }

        public void None()
        {
            setter("none");
        }

        public void Version()
        {
            setter("version");
        }

        public void Dirty()
        {
            setter("dirty");
        }

        public void All()
        {
            setter("all");
        }
    }

    public class OptimisticLockBuilder<TParent>
    {
        private readonly TParent parent;
        readonly OptimisticLockBuilder innerBuilder;

        public OptimisticLockBuilder(TParent parent, OptimisticLockBuilder innerBuilder)
        {
            this.parent = parent;
            this.innerBuilder = innerBuilder;
        }

        /// <summary>
        /// Use no locking strategy
        /// </summary>
        public TParent None()
        {
            innerBuilder.None();
            return parent;
        }

        /// <summary>
        /// Use version locking
        /// </summary>
        public TParent Version()
        {
            innerBuilder.Version();
            return parent;
        }

        /// <summary>
        /// Use dirty locking
        /// </summary>
        public TParent Dirty()
        {
            innerBuilder.Dirty();
            return parent;
        }

        /// <summary>
        /// Use all locking
        /// </summary>
        public TParent All()
        {
            innerBuilder.All();
            return parent;
        }
    }
}