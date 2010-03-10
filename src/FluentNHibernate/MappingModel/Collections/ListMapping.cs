﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.Collections
{
    public class ListMapping : CollectionMappingBase, IIndexedCollectionMapping
    {
        readonly Member member;
        private readonly ValueStore values;

        public ListMapping(Member member)
            : this(member, new ValueStore())
        {}

        ListMapping(Member member, ValueStore values)
            : base(member, values)
        {
            this.member = member;
            this.values = values;
        }

        public IIndexMapping Index { get; set; }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessList(this);

            if(Index != null)
                visitor.Visit(Index);

            base.AcceptVisitor(visitor);
        }

    	public override string OrderBy
    	{
			get { return null; }
			set { /* no-op */ }
    	}

        public new bool IsSpecified(string property)
        {
            return false;
        }

        public bool Equals(ListMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && Equals(other.values, values);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as ListMapping);
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
