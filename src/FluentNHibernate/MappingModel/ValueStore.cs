using System;
using System.Collections.Generic;

namespace FluentNHibernate.MappingModel
{
    public class ValueStore
    {
        readonly Dictionary<Attr, object> values = new Dictionary<Attr, object>();

        public void Set(Attr attr, object value)
        {
            values[attr] = value;
        }

        public string Get(Attr attr)
        {
            return Get<string>(attr);
        }

        public T Get<T>(Attr attr)
        {
            return (T)(values[attr] ?? default(T));
        }

        public void Merge(IEnumerable<KeyValuePair<Attr, object>> otherValues)
        {
            foreach (var pair in otherValues)
                values[pair.Key] = pair.Value;
        }

        public bool HasValue(Attr attr)
        {
            return values.ContainsKey(attr);
        }

        public ValueStore Clone()
        {
            return this; // TODO: Fix
        }
    }
}