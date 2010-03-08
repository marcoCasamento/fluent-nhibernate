using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FluentNHibernate.Mapping;
using FluentNHibernate.Utils;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.Collections
{
    public class SetMapping : CollectionMappingBase
    {
        readonly Member member;
        private readonly AttributeStore<SetMapping> attributes;

        public SetMapping(Member member)
            : this(new AttributeStore())
        {
            this.member = member;
        }

        SetMapping(AttributeStore underlyingStore)
            : base(underlyingStore)
        {
            attributes = new AttributeStore<SetMapping>(underlyingStore);
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessSet(this);
            base.AcceptVisitor(visitor);
        }

        public override string OrderBy
        {
            get { return attributes.Get(x => x.OrderBy); }
            set { attributes.Set(x => x.OrderBy, value); }
        }

        public string Sort
        {
            get { return attributes.Get(x => x.Sort); }
            set { attributes.Set(x => x.Sort, value); }
        }

        public new bool IsSpecified(string property)
        {
            return attributes.IsSpecified(property);
        }

        public bool HasValue<TResult>(Expression<Func<SetMapping, TResult>> property)
        {
            return attributes.HasValue(property);
        }

        public void SetDefaultValue<TResult>(Expression<Func<SetMapping, TResult>> property, TResult value)
        {
            attributes.SetDefault(property, value);
        }

        public bool Equals(SetMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && Equals(other.attributes, attributes);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as SetMapping);
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

        public override void AddChild(IMapping child)
        {
            throw new NotImplementedException();
        }

        public override void UpdateValues(IEnumerable<KeyValuePair<Attr, object>> values)
        {
            throw new NotImplementedException();
        }
    }
}