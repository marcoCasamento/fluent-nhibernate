using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FluentNHibernate.Utils
{
    public static class CollectionExtensions
    {
        [DebuggerStepThrough]
        public static void Each<T>(this IEnumerable<T> enumerable, Action<T> each)
        {
            foreach (var item in enumerable)
                each(item);
        }

        [DebuggerStepThrough]
        public static void Each<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> enumerable, Action<TKey, TValue> each)
        {
            foreach (var item in enumerable)
                each(item.Key, item.Value);
        }
    }
}