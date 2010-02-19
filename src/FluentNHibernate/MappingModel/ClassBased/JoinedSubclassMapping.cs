using System;
using System.Linq.Expressions;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.ClassBased
{
    public class JoinedSubclassMapping : ClassMappingBase, ISubclassMapping
    {
        private AttributeStore attributes;

        public JoinedSubclassMapping() : this(new AttributeStore())
        {}

        public JoinedSubclassMapping(AttributeStore store)
        {
            attributes = store.Clone();
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessJoinedSubclass(this);
            
            if (Key != null)
                visitor.Visit(Key);

            base.AcceptVisitor(visitor);
        }

        public KeyMapping Key
        {
            get { return attributes.Get<KeyMapping>(Attr.Key); }
            set { attributes.Set(Attr.Key, value); }
        }

        public string TableName
        {
            get { return attributes.Get(Attr.Table); }
            set { attributes.Set(Attr.Table, value); }
        }

        public string Schema
        {
            get { return attributes.Get(Attr.Schema); }
            set { attributes.Set(Attr.Schema, value); }
        }

        public string Extends
        {
            get { return attributes.Get(Attr.Extends); }
            set { attributes.Set(Attr.Extends, value); }
        }

        public string Check
        {
            get { return attributes.Get(Attr.Check); }
            set { attributes.Set(Attr.Check, value); }
        }

        public string Proxy
        {
            get { return attributes.Get(Attr.Proxy); }
            set { attributes.Set(Attr.Proxy, value); }
        }

        public bool Lazy
        {
            get { return attributes.Get<bool>(Attr.Lazy); }
            set { attributes.Set(Attr.Lazy, value); }
        }

        public bool DynamicUpdate
        {
            get { return attributes.Get<bool>(Attr.DynamicUpdate); }
            set { attributes.Set(Attr.DynamicUpdate, value); }
        }

        public bool DynamicInsert
        {
            get { return attributes.Get<bool>(Attr.DynamicInsert); }
            set { attributes.Set(Attr.DynamicInsert, value); }
        }

        public bool SelectBeforeUpdate
        {
            get { return attributes.Get<bool>(Attr.SelectBeforeUpdate); }
            set { attributes.Set(Attr.SelectBeforeUpdate, value); }
        }

        public bool Abstract
        {
            get { return attributes.Get<bool>(Attr.Abstract); }
            set { attributes.Set(Attr.Abstract, value); }
        }

        public string Subselect
        {
            get { return attributes.Get(Attr.Subselect); }
            set { attributes.Set(Attr.Subselect, value); }
        }

        public TypeReference Persister
        {
            get { return attributes.Get<TypeReference>(Attr.Persister); }
            set { attributes.Set(Attr.Persister, value); }
        }

        public int BatchSize
        {
            get { return attributes.Get<int>(Attr.BatchSize); }
            set { attributes.Set(Attr.BatchSize, value); }
        }

        public string EntityName
        {
            get { return attributes.Get(Attr.EntityName); }
            set { attributes.Set(Attr.EntityName, value); }
<<<<<<< Updated upstream
        }

        public override bool IsSpecified(Attr property)
        {
            return attributes.HasUserValue(property);
        }

        public bool HasValue(Attr property)
        {
            return attributes.HasAnyValue(property);
        }

        public void SetDefaultValue<TResult>(Attr property, TResult value)
        {
            attributes.SetDefault(property, value);
=======
>>>>>>> Stashed changes
        }

        public void OverrideAttributes(AttributeStore store)
        {
            attributes = store.Clone();
        }

        public bool Equals(JoinedSubclassMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && Equals(other.attributes, attributes);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as JoinedSubclassMapping);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                {
                    return (base.GetHashCode() * 397) ^ (attributes != null ? attributes.GetHashCode() : 0);
                }
            }
        }
    }
}
