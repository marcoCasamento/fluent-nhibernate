using System.Diagnostics;
using FluentNHibernate.MappingModel;

namespace FluentNHibernate.Mapping
{
    /// <summary>
    /// Maps to the Join element in NH 2.0
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JoinPart<T> : ClasslikeMapBase<T>
    {
        readonly IMappingStructure<JoinMapping> structure;
        readonly FetchTypeExpression<JoinPart<T>> fetch;
        readonly BucketStructure<KeyMapping> keyStructure;
        bool nextBool = true;

        public JoinPart(IMappingStructure<JoinMapping> structure)
            : base(structure)
        {
            this.structure = structure;
            this.keyStructure = new BucketStructure<KeyMapping>();
            structure.AddChild(keyStructure);

            fetch = new FetchTypeExpression<JoinPart<T>>(this, value => structure.SetValue(Attr.Fetch, value));
        }

        public JoinPart<T> KeyColumn(string columnName)
        {
            structure.RemoveChildrenMatching(x => x is IMappingStructure<ColumnMapping>);

            var column = new ColumnStructure(structure);

            new ColumnPart(column)
                .Name(columnName);

            structure.AddChild(column);

            return this;
        }

        public JoinPart<T> Schema(string schema)
        {
            structure.SetValue(Attr.Schema, schema);
            return this;
        }

        public FetchTypeExpression<JoinPart<T>> Fetch
        {
            get { return fetch; }
        }

        public JoinPart<T> Inverse()
        {
            structure.SetValue(Attr.Inverse, nextBool);
            nextBool = true;
            return this;
        }

        public JoinPart<T> Optional()
        {
            structure.SetValue(Attr.Optional, nextBool);
            nextBool = true;
            return this;
        }

        public JoinPart<T> Catalog(string catalog)
        {
            structure.SetValue(Attr.Catalog, catalog);
            return this;
        }

        public JoinPart<T> Subselect(string subselect)
        {
            structure.SetValue(Attr.Subselect, subselect);
            return this;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public JoinPart<T> Not
        {
            get
            {
                nextBool = !nextBool;
                return this;
            }
        }

        public void Table(string tableName)
        {
            structure.SetValue(Attr.Table, tableName);
        }
    }
}
