using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.Collections
{
    public abstract class CollectionMappingBase : MappingBase, ICollectionMapping
    {
        readonly Member member;
        readonly ValueStore values;
        readonly IList<FilterMapping> filters = new List<FilterMapping>();
        public Type ContainingEntityType { get; set; }
        public Member Member { get; set; }

        protected CollectionMappingBase(Member member, ValueStore values)
        {
            this.member = member;
            this.values = values;
        }

        public IList<FilterMapping> Filters
        {
            get { return filters; }
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            if (Key != null)
                visitor.Visit(Key);

            if (Element != null)
                visitor.Visit(Element);

            if (CompositeElement != null)
                visitor.Visit(CompositeElement);

            if (Relationship != null)
                visitor.Visit(Relationship);

            foreach (var filter in Filters)
                visitor.Visit(filter);

            if (Cache != null)
                visitor.Visit(Cache);
        }

        public Type ChildType
        {
            get { return values.Get<Type>(Attr.ChildType); }
            set { values.Set(Attr.ChildType, value); }
        }

        public ICollectionMapping OtherSide { get; set; }
        public KeyMapping Key { get; set; }
        public ElementMapping Element { get; set; }
        public CompositeElementMapping CompositeElement { get; set; }
        public CacheMapping Cache { get; set; }
        public ICollectionRelationshipMapping Relationship { get; set; }

        public bool Generic
        {
            get { return values.Get<bool>(Attr.Generic); }
            set { values.Set(Attr.Generic, value); }
        }

        public bool Lazy
        {
            get { return values.Get<bool>(Attr.Lazy); }
            set { values.Set(Attr.Lazy, value); }
        }

        public bool Inverse
        {
            get { return values.Get<bool>(Attr.Inverse); }
            set { values.Set(Attr.Inverse, value); }
        }

        public string Name
        {
            get { return values.Get(Attr.Name); }
            set { values.Set(Attr.Name, value); }
        }

        public string Access
        {
            get { return values.Get(Attr.Access); }
            set { values.Set(Attr.Access, value); }
        }

        public string TableName
        {
            get { return values.Get(Attr.Table); }
            set { values.Set(Attr.Table, value); }
        }

        public string Schema
        {
            get { return values.Get(Attr.Schema); }
            set { values.Set(Attr.Schema, value); }
        }

        public string Fetch
        {
            get { return values.Get(Attr.Fetch); }
            set { values.Set(Attr.Fetch, value); }
        }

        public string Cascade
        {
            get { return values.Get(Attr.Cascade); }
            set { values.Set(Attr.Cascade, value); }
        }

        public string Where
        {
            get { return values.Get(Attr.Where); }
            set { values.Set(Attr.Where, value); }
        }

        public bool Mutable
        {
            get { return values.Get<bool>(Attr.Mutable); }
            set { values.Set(Attr.Mutable, value); }
        }

        public string Subselect
        {
            get { return values.Get(Attr.Subselect); }
            set { values.Set(Attr.Subselect, value); }
        }

    	public TypeReference Persister
        {
            get { return values.Get<TypeReference>(Attr.Persister); }
            set { values.Set(Attr.Persister, value); }
        }

        public int BatchSize
        {
            get { return values.Get<int>(Attr.BatchSize); }
            set { values.Set(Attr.BatchSize, value); }
        }

        public string Check
        {
            get { return values.Get(Attr.Check); }
            set { values.Set(Attr.Check, value); }
        }

        public TypeReference CollectionType
        {
            get { return values.Get<TypeReference>(Attr.CollectionType); }
            set { values.Set(Attr.CollectionType, value); }
        }

        public string OptimisticLock
        {
            get { return values.Get(Attr.OptimisticLock); }
            set { values.Set(Attr.OptimisticLock, value); }
        }

        public override bool IsSpecified(string property)
        {
            return false;
        }

        public bool HasValue(Attr attr)
        {
            return values.HasValue(attr);
        }

		public abstract string OrderBy { get; set; }

        public bool Equals(CollectionMappingBase other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.values, values) &&
                other.filters.ContentEquals(filters) &&
                Equals(other.ContainingEntityType, ContainingEntityType)
                && Equals(other.Member, Member);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(CollectionMappingBase)) return false;
            return Equals((CollectionMappingBase)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = (values != null ? values.GetHashCode() : 0);
                result = (result * 397) ^ (filters != null ? filters.GetHashCode() : 0);
                result = (result * 397) ^ (ContainingEntityType != null ? ContainingEntityType.GetHashCode() : 0);
                result = (result * 397) ^ (Member != null ? Member.GetHashCode() : 0);
                return result;
            }
        }

        public virtual void AddChild(IMapping child)
        {
            if (child is KeyMapping)
                Key = (KeyMapping)child;
            if (child is ElementMapping)
                Element = (ElementMapping)child;
            if (child is CompositeElementMapping)
                CompositeElement = (CompositeElementMapping)child;
            if (child is CacheMapping)
                Cache = (CacheMapping)child;
            if (child is ICollectionRelationshipMapping)
                Relationship = (ICollectionRelationshipMapping)child;
        }
        
        public virtual void UpdateValues(IEnumerable<KeyValuePair<Attr, object>> otherValues)
        {
            values.Merge(otherValues);
        }
    }
}