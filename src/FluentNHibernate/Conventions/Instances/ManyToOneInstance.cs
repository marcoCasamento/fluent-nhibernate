using System;
using System.Diagnostics;
using System.Linq;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.MappingModel;

namespace FluentNHibernate.Conventions.Instances
{
    public class ManyToOneInstance : ManyToOneInspector, IManyToOneInstance
    {
        private readonly ManyToOneMapping mapping;
        private bool nextBool = true;

        public ManyToOneInstance(ManyToOneMapping mapping)
            : base(mapping)
        {
            this.mapping = mapping;
        }

        public void Column(string columnName)
        {
            if (mapping.Columns.UserDefined.Count() > 0)
                return;

            var originalColumn = mapping.Columns.FirstOrDefault();
            var column = originalColumn == null ? new ColumnMapping() : originalColumn.Clone();

            column.Name = columnName;

            mapping.ClearColumns();
            mapping.AddColumn(column);
        }

        public void CustomClass<T>()
        {
            if (!mapping.IsSpecified(Attr.Class))
                mapping.Class = new TypeReference(typeof(T));
        }

        public void CustomClass(Type type)
        {
            if (!mapping.IsSpecified(Attr.Class))
                mapping.Class = new TypeReference(type);
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

        public new ICascadeInstance Cascade
        {
            get
            {
                return new CascadeInstance(value =>
                {
                    if (!mapping.IsSpecified(Attr.Cascade))
                        mapping.Cascade = value;
                });
            }
        }

        new public IFetchInstance Fetch
        {
            get
            {
                return new FetchInstance(value =>
                {
                    if (!mapping.IsSpecified(Attr.Fetch))
                        mapping.Fetch = value;
                });
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public IManyToOneInstance Not
        {
            get
            {
                nextBool = !nextBool;
                return this;
            }
        }

        public new INotFoundInstance NotFound
        {
            get
            {
                return new NotFoundInstance(value =>
                {
                    if (!mapping.IsSpecified(Attr.NotFound))
                        mapping.NotFound = value;
                });
            }
        }

        public void Index(string index)
        {
            if (mapping.Columns.First().IsSpecified(Attr.Index))
                return;

            foreach (var column in mapping.Columns)
                column.Index = index;
        }

        public new void Insert()
        {
            if (!mapping.IsSpecified(Attr.Insert))
                mapping.Insert = nextBool;
            nextBool = true;
        }

        public new void LazyLoad()
        {
            if (!mapping.IsSpecified(Attr.Lazy))
                mapping.Lazy = nextBool;
            nextBool = true;
        }

        public void Nullable()
        {
            if (!mapping.Columns.First().IsSpecified(Attr.NotNull))
                foreach (var column in mapping.Columns)
                    column.NotNull = !nextBool;

            nextBool = true;
        }

        public new void PropertyRef(string property)
        {
            if (!mapping.IsSpecified(Attr.PropertyRef))
                mapping.PropertyRef = property;
        }

        public void ReadOnly()
        {
            if (!mapping.IsSpecified(Attr.Insert) && !mapping.IsSpecified(Attr.Update))
            {
                mapping.Insert = !nextBool;
                mapping.Update = !nextBool;
            }
            nextBool = true;
        }

        public void Unique()
        {
            if (!mapping.Columns.First().IsSpecified(Attr.Unique))
                foreach (var column in mapping.Columns)
                    column.Unique = nextBool;

            nextBool = true;
        }

        public void UniqueKey(string key)
        {
            if (mapping.Columns.First().IsSpecified(Attr.UniqueKey))
                return;

            foreach (var column in mapping.Columns)
                column.UniqueKey = key;
        }

        public new void Update()
        {
            if (!mapping.IsSpecified(Attr.Update))
                mapping.Update = nextBool;
            nextBool = true;
        }

        public new void ForeignKey(string key)
        {
            if (!mapping.IsSpecified(Attr.ForeignKey))
                mapping.ForeignKey = key;
        }

        public void OverrideInferredClass(Type type)
        {
            mapping.Class = new TypeReference(type);
        }
    }
}