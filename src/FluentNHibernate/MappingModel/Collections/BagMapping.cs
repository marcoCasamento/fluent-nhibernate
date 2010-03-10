using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using FluentNHibernate.Utils;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.Collections
{
    public class BagMapping : CollectionMappingBase
    {
        readonly Member member;
        readonly ValueStore values;

        public BagMapping(Member member)
            : this(member, new ValueStore())
        {}

        BagMapping(Member member, ValueStore values)
            : base(member, values)
        {
            this.values = values;
            this.member = member;
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessBag(this);
            base.AcceptVisitor(visitor);
        }

        public override string OrderBy
        {
            get { return values.Get(Attr.OrderBy); }
            set { values.Set(Attr.OrderBy, value); }
        }

        public new bool IsSpecified(string property)
        {
            return false;
        }

        public bool Equals(BagMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && Equals(other.values, values);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as BagMapping);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                {
                    return (base.GetHashCode() * 397) ^ (values != null ? values.GetHashCode() : 0);
                }
            }
        }

        public override void UpdateValues(IEnumerable<KeyValuePair<Attr, object>> otherValues)
        {
            values.Merge(otherValues);
        }
    }
}