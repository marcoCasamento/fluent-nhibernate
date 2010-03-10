using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FluentNHibernate.Utils;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.Collections
{
    public class MapMapping : CollectionMappingBase, IIndexedCollectionMapping
    {
        readonly Member member;
        readonly ValueStore values;

        public IIndexMapping Index { get; set; }

        public MapMapping(Member member)
            : this(member, new ValueStore())
        {
            this.member = member;
        }

        MapMapping(Member member, ValueStore values)
            : base(member, values)
        {
            this.member = member;
            this.values = values;
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessMap(this);

            if (Index != null)
                visitor.Visit(Index);

            base.AcceptVisitor(visitor);
        }

        public override string OrderBy
        {
            get { return values.Get(Attr.OrderBy); }
            set { values.Set(Attr.OrderBy, value); }
        }

        public string Sort
        {
            get { return values.Get(Attr.Sort); }
            set { values.Set(Attr.Sort, value); }
        }

        public new bool IsSpecified(string property)
        {
            return false;
        }

        public bool Equals(MapMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && Equals(other.values, values);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as MapMapping);
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

        public override void AddChild(IMapping child)
        {
        }

        public override void UpdateValues(IEnumerable<KeyValuePair<Attr, object>> otherValues)
        {
            values.Merge(otherValues);
        }
    }
}