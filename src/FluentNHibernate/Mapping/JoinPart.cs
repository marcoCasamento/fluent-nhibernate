using System;
using System.Collections.Generic;
using System.Diagnostics;
using FluentNHibernate.Mapping.Providers;
using FluentNHibernate.MappingModel;

namespace FluentNHibernate.Mapping
{
    /// <summary>
    /// Maps to the Join element in NH 2.0
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JoinPart<T> : ClasslikeMapBase<T>, IJoinMappingProvider
    {
        private readonly IList<string> columns = new List<string>();
        private readonly FetchBuilder fetch;
        private readonly AttributeStore attributes = new AttributeStore();
        private bool nextBool = true;

        public JoinPart(string tableName)
        {
            fetch = new FetchBuilder(value => attributes.Set(Attr.Fetch, value));

            attributes.SetDefault(Attr.Table, tableName);
            attributes.Set(Attr.Key, new KeyMapping { ContainingEntityType = typeof(T) });
        }

        public JoinPart<T> KeyColumn(string column)
        {
            columns.Clear(); // only one supported currently
            columns.Add(column);
            return this;
        }

        public JoinPart<T> Schema(string schema)
        {
            attributes.Set(Attr.Schema, schema);
            return this;
        }

        public FetchBuilder<JoinPart<T>> Fetch
        {
            get { return new FetchBuilder<JoinPart<T>>(this, fetch); }
        }

        public JoinPart<T> Inverse()
        {
            attributes.Set(Attr.Inverse, nextBool);
            nextBool = true;
            return this;
        }

        public JoinPart<T> Optional()
        {
            attributes.Set(Attr.Optional, nextBool);
            nextBool = true;
            return this;
        }

        public JoinPart<T> Catalog(string catalog)
        {
            attributes.Set(Attr.Catalog, catalog);
            return this;
        }

        public JoinPart<T> Subselect(string subselect)
        {
            attributes.Set(Attr.Subselect, subselect);
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

        JoinMapping IJoinMappingProvider.GetJoinMapping()
        {
            var mapping = new JoinMapping(attributes.Clone());

            mapping.ContainingEntityType = typeof(T);

            if (columns.Count == 0)
                mapping.Key.AddDefaultColumn(new ColumnMapping { Name = typeof(T).Name + "_id" });
            else
                foreach (var column in columns)
                    mapping.Key.AddColumn(new ColumnMapping { Name = column });

            foreach (var property in properties)
                mapping.AddProperty(property.GetPropertyMapping());

            foreach (var component in components)
                mapping.AddComponent(component.GetComponentMapping());

            foreach (var reference in references)
                mapping.AddReference(reference.GetManyToOneMapping());

            foreach (var any in anys)
                mapping.AddAny(any.GetAnyMapping());

            return mapping;
        }

        public void Table(string tableName)
        {
            attributes.Set(Attr.Table, tableName);
        }
    }
}
