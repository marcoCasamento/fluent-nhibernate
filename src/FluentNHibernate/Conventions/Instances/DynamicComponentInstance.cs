using System.Diagnostics;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;

namespace FluentNHibernate.Conventions.Instances
{
    public class DynamicComponentInstance : DynamicComponentInspector, IDynamicComponentInstance
    {
        private readonly DynamicComponentMapping mapping;
        private bool nextBool;

        public DynamicComponentInstance(DynamicComponentMapping mapping)
            : base(mapping)
        {
            this.mapping = mapping;
            nextBool = true;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public IDynamicComponentInstance Not
        {
            get
            {
                nextBool = !nextBool;
                return this;
            }
        }


        public new IAccessInstance Access
        {
            get
            {
                return new AccessInstance(value =>
                {
                    if (!mapping.IsSpecified(Attr.Access))
                        mapping.Access = value;
                });
            }
        }

        public new void Update()
        {
            if (!mapping.IsSpecified(Attr.Update))
                mapping.Update = nextBool;
            nextBool = true;
        }

        public new void Insert()
        {
            if (!mapping.IsSpecified(Attr.Insert))
                mapping.Insert = nextBool;
            nextBool = true;
        }

        public new void Unique()
        {
            if (!mapping.IsSpecified(Attr.Unique))
                mapping.Unique = nextBool;
            nextBool = true;
        }

        public new void OptimisticLock()
        {
            if (!mapping.IsSpecified(Attr.OptimisticLock))
                mapping.OptimisticLock = nextBool;
            nextBool = true;
        }
    }
}