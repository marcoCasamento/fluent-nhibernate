using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.Collections
{
    public abstract class CollectionMappingBase : MappingBase, ICollectionMapping
    {
        protected AttributeStore attributes;
        private readonly IList<FilterMapping> filters = new List<FilterMapping>();
        public Type ContainingEntityType { get; set; }
        public Member Member { get; set; }

        protected CollectionMappingBase(AttributeStore underlyingStore)
        {
            attributes = underlyingStore.Clone();
            attributes.SetDefault(Attr.Mutable, true);
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
            get { return attributes.Get<Type>(Attr.ChildType); }
            set { attributes.Set(Attr.ChildType, value); }
        }

        public ICollectionMapping OtherSide { get; set; }

        public KeyMapping Key
        {
            get { return attributes.Get<KeyMapping>(Attr.Key); }
            set { attributes.Set(Attr.Key, value); }
        }

        public ElementMapping Element
        {
            get { return attributes.Get<ElementMapping>(Attr.Element); }
            set { attributes.Set(Attr.Element, value); }
        }

        public CompositeElementMapping CompositeElement
        {
            get { return attributes.Get<CompositeElementMapping>(Attr.CompositeElement); }
            set { attributes.Set(Attr.CompositeElement, value); }
        }

        public CacheMapping Cache
        {
            get { return attributes.Get<CacheMapping>(Attr.Cache); }
            set { attributes.Set(Attr.Cache, value); }
        }

        public ICollectionRelationshipMapping Relationship
        {
            get { return attributes.Get<ICollectionRelationshipMapping>(Attr.Relationship); }
            set { attributes.Set(Attr.Relationship, value); }
        }

        public bool Generic
        {
            get { return attributes.Get<bool>(Attr.Generic); }
            set { attributes.Set(Attr.Generic, value); }
        }

        public bool Lazy
        {
            get { return attributes.Get<bool>(Attr.Lazy); }
            set { attributes.Set(Attr.Lazy, value); }
        }

        public bool Inverse
        {
            get { return attributes.Get<bool>(Attr.Inverse); }
            set { attributes.Set(Attr.Inverse, value); }
        }

        public string Name
        {
            get { return attributes.Get(Attr.Name); }
            set { attributes.Set(Attr.Name, value); }
        }

        public string Access
        {
            get { return attributes.Get(Attr.Access); }
            set { attributes.Set(Attr.Access, value); }
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

        public string Fetch
        {
            get { return attributes.Get(Attr.Fetch); }
            set { attributes.Set(Attr.Fetch, value); }
        }

        public string Cascade
        {
            get { return attributes.Get(Attr.Cascade); }
            set { attributes.Set(Attr.Cascade, value); }
        }

        public string Where
        {
            get { return attributes.Get(Attr.Where); }
            set { attributes.Set(Attr.Where, value); }
        }

        public bool Mutable
        {
            get { return attributes.Get<bool>(Attr.Mutable); }
            set { attributes.Set(Attr.Mutable, value); }
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

        public string Check
        {
            get { return attributes.Get(Attr.Check); }
            set { attributes.Set(Attr.Check, value); }
        }

        public TypeReference CollectionType
        {
            get { return attributes.Get<TypeReference>(Attr.CollectionType); }
            set { attributes.Set(Attr.CollectionType, value); }
        }

        public string OptimisticLock
        {
            get { return attributes.Get(Attr.OptimisticLock); }
            set { attributes.Set(Attr.OptimisticLock, value); }
        }

		public abstract string OrderBy { get; set; }

        public bool Equals(CollectionMappingBase other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.attributes, attributes) &&
                other.filters.ContentEquals(filters) &&
                Equals(other.ContainingEntityType, ContainingEntityType)
                && Equals(other.Member, Member) &&
                Equals(other.OtherSide, OtherSide);
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
                int result = (attributes != null ? attributes.GetHashCode() : 0);
                result = (result * 397) ^ (filters != null ? filters.GetHashCode() : 0);
                result = (result * 397) ^ (ContainingEntityType != null ? ContainingEntityType.GetHashCode() : 0);
                result = (result * 397) ^ (Member != null ? Member.GetHashCode() : 0);
                result = (result * 397) ^ (OtherSide != null ? OtherSide.GetHashCode() : 0);
                return result;
            }
        }
    }
}
