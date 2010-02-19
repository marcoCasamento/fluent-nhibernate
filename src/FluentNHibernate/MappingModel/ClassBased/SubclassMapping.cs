using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FluentNHibernate.Mapping;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.ClassBased
{
    public class SubclassMapping : ClassMappingBase, ISubclassMapping
    {
        private AttributeStore attributes;

        public SubclassMapping()
            : this(new AttributeStore())
        {}

        public SubclassMapping(AttributeStore underlyingStore)
        {
            attributes = underlyingStore.Clone();
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessSubclass(this);

            base.AcceptVisitor(visitor);
        }

<<<<<<< Updated upstream
        public override void MergeAttributes(AttributeStore store)
        {
            attributes.Merge(store.Clone());
        }

        public override string Name
        {
            get { return attributes.Get(Attr.Name); }
            set { attributes.Set(Attr.Name, value); }
        }

        public override Type Type
        {
            get { return attributes.Get<Type>(Attr.Type); }
            set { attributes.Set(Attr.Type, value); }
        }

=======
>>>>>>> Stashed changes
        public object DiscriminatorValue
        {
            get { return attributes.Get(Attr.DiscriminatorValue); }
            set { attributes.Set(Attr.DiscriminatorValue, value); }
        }

        public string Extends
        {
            get { return attributes.Get(Attr.Extends); }
            set { attributes.Set(Attr.Extends, value); }
        }

        public bool Lazy
        {
            get { return attributes.Get<bool>(Attr.Lazy); }
            set { attributes.Set(Attr.Lazy, value); }
        }

        public string Proxy
        {
            get { return attributes.Get(Attr.Proxy); }
            set { attributes.Set(Attr.Proxy, value); }
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
=======
>>>>>>> Stashed changes
        }

        public void SetDefaultValue<TResult>(Attr property, TResult value)
        {
            attributes.SetDefault(property, value);
        }

        public void OverrideAttributes(AttributeStore store)
        {
            attributes = store.Clone();
        }

        public bool Equals(SubclassMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && Equals(other.attributes, attributes);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as SubclassMapping);
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