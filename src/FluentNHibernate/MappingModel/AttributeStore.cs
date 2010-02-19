using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FluentNHibernate.MappingModel
{
    public enum Attr
    {
        Name,
        Lazy,
        Access,
        Cascade,
        IdType,
        Insert,
        Update,
        OptimisticLock,
        Usage,
        Region,
        Include,
        Polymorphism,
        SchemaAction,
        DiscriminatorValue,
        Schema,
        Table,
        Mutable,
        DynamicUpdate,
        DynamicInsert,
        BatchSize,
        Check,
        Persister,
        Proxy,
        SelectBeforeUpdate,
        Where,
        Subselect,
        EntityName,
        Parent,
        Unique,
        Mapped,
        UnsavedValue,
        Force,
        Formula,
        Precision,
        Length,
        Scale,
        NotNull,
        UniqueKey,
        Index,
        Default,
        Type,
        DefaultCascade,
        DefaultAccess,
        AutoImport,
        DefaultLazy,
        Catalog,
        Namespace,
        Assembly,
        Generator,
        SqlType,
        Rename,
        Class,
        Abstract,
        Optional,
        Inverse,
        Key,
        Fetch,
        ForeignKey,
        NotFound,
        OrderBy,
        PropertyRef,
        MetaType,
        Version,
        EntityType,
        Id,
        Discriminator,
        Cache,
        ChildType,
        CollectionType,
        CompositeElement,
        Element,
        Generic,
        Relationship,
        Nullable,
        Extends,
        OnDelete,
        ParentType,
        Sort,
        Value,
        Constrained,
        Generated,
        Tuplizer,
        Condition,
        SPType,
        Query,
        Mode
    }
    public class AttributeStore : IDefaultableEnumerable<KeyValuePair<Attr, object>>
    {
        private readonly IDictionary<Attr, object> userDefined;
        private readonly IDictionary<Attr, object> defaults;

        public AttributeStore()
        {
            userDefined = new Dictionary<Attr, object>();
            defaults = new Dictionary<Attr, object>();
        }

        public object this[Attr key]
        {
            get
            {
                if (userDefined.ContainsKey(key))
                    return userDefined[key];
                
                if (defaults.ContainsKey(key))
                    return defaults[key];

                return null;
            }
            set { userDefined[key] = value; }
        }

        public string Get(Attr property)
        {
            return Get<string>(property);
        }

        public TResult Get<TResult>(Attr property)
        {
            return (TResult)(this[property] ?? default(TResult));
        }

        public void Set<TResult>(Attr property, TResult value)
        {
            this[property] = value;
        }

        /// <summary>
        /// Gets whether an attribute has a value specified by the user.
        /// </summary>
        /// <param name="key">Attribute</param>
        public bool HasUserValue(Attr key)
        {
            return userDefined.ContainsKey(key);
        }

        /// <summary>
        /// Gets whether an attribute has any value at all, user specified or default.
        /// </summary>
        /// <param name="key">Attribute</param>
        public bool HasAnyValue(Attr key)
        {
            return userDefined.ContainsKey(key) || defaults.ContainsKey(key);
        }

        public void CopyTo(AttributeStore store)
        {
            foreach (var pair in userDefined)
                store.userDefined[pair.Key] = pair.Value;

            foreach (var pair in defaults)
                store.defaults[pair.Key] = pair.Value;
        }

        public void SetDefault(Attr key, object value)
        {
            defaults[key] = value;
        }

        public void Merge(AttributeStore otherStore)
        {
            foreach (var key in otherStore.defaults.Keys)
                defaults[key] = otherStore.defaults[key];

            foreach (var key in otherStore.userDefined.Keys)
                userDefined[key] = otherStore.userDefined[key];
        }

        public AttributeStore Clone()
        {
            var clonedStore = new AttributeStore();

            CopyTo(clonedStore);

            return clonedStore;
        }

        public bool Equals(AttributeStore other)
        {
            return other.userDefined.ContentEquals(userDefined) && other.defaults.ContentEquals(defaults);
        }

        public IEnumerator<KeyValuePair<Attr, object>> GetEnumerator()
        {
            return Defaults.Concat(UserDefined).ToList().GetEnumerator();
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(AttributeStore)) return false;
            return Equals((AttributeStore)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((userDefined != null ? userDefined.GetHashCode() : 0) * 397) ^
                    (defaults != null ? defaults.GetHashCode() : 0);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerable<KeyValuePair<Attr, object>> Defaults
        {
            get { return defaults.ToArray(); }
        }

        public IEnumerable<KeyValuePair<Attr, object>> UserDefined
        {
            get { return userDefined.ToArray(); }
        }
    }

    //public class AttributeStore<T>
    //{
    //    private readonly AttributeStore store;

    //    public AttributeStore()
    //        : this(new AttributeStore())
    //    {

    //    }

    //    public AttributeStore(AttributeStore store)
    //    {
    //        this.store = store;
    //    }

    //    public TResult Get<TResult>(Expression<Func<T, TResult>> exp)
    //    {
    //        return (TResult)(store[GetKey(exp)] ?? default(TResult));
    //    }

    //    public void Set<TResult>(Expression<Func<T, TResult>> exp, TResult value)
    //    {
    //        store[GetKey(exp)] = value;
    //    }

    //    public void SetDefault<TResult>(Expression<Func<T, TResult>> exp, TResult value)
    //    {
    //        store.SetDefault(GetKey(exp), value);
    //    }

    //    /// <summary>
    //    /// Returns whether the user has set a value for a property.
    //    /// </summary>
    //    public bool HasUserValue<TResult>(Expression<Func<T, TResult>> exp)
    //    {
    //        return store.HasUserValue(GetKey(exp));
    //    }

    //    /// <summary>
    //    /// Returns whether the user has set a value for a property.
    //    /// </summary>
    //    public bool HasUserValue(string property)
    //    {
    //        return store.HasUserValue(property);
    //    }

    //    /// <summary>
    //    /// Returns whether a property has any value, default or user specified.
    //    /// </summary>
    //    /// <typeparam name="TResult"></typeparam>
    //    /// <param name="exp"></param>
    //    /// <returns></returns>
    //    public bool HasAnyValue<TResult>(Expression<Func<T, TResult>> exp)
    //    {
    //        return store.HasAnyValue(GetKey(exp));
    //    }

    //    public bool HasAnyValue(string property)
    //    {
    //        return store.HasAnyValue(property);
    //    }

    //    public void CopyTo(AttributeStore<T> target)
    //    {
    //        store.CopyTo(target.store);
    //    }

    //    private static string GetKey<TResult>(Expression<Func<T, TResult>> expression)
    //    {
    //        var member = expression.ToMember();
    //        return member.Name.ToLower();
    //    }

    //    public AttributeStore<T> Clone()
    //    {
    //        var clonedStore = new AttributeStore<T>();

    //        store.CopyTo(clonedStore.store);

    //        return clonedStore;
    //    }

    //    public AttributeStore CloneInner()
    //    {
    //        var clonedStore = new AttributeStore();

    //        store.CopyTo(clonedStore);

    //        return clonedStore;
    //    }

    //    public void Merge(AttributeStore<T> otherStore)
    //    {
    //        store.Merge(otherStore.store);
    //    }

    //    public bool Equals(AttributeStore<T> other)
    //    {
    //        return Equals(other.store, store);
    //    }

    //    public override bool Equals(object obj)
    //    {
    //        if (obj.GetType() != typeof(AttributeStore<T>)) return false;
    //        return Equals((AttributeStore<T>)obj);
    //    }

    //    public override int GetHashCode()
    //    {
    //        return (store != null ? store.GetHashCode() : 0);
    //    }
    //}
}