using System;
using System.Collections.Generic;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.Collections
{
    public abstract class CollectionMappingBase : MappingBase, ICollectionMapping
    {
        private readonly MemberNameFormatter memberNameFormatter = new MemberNameFormatter();
        private readonly MemberTypeSelector memberTypeSelector = new MemberTypeSelector();
        private readonly IList<FilterMapping> filters = new List<FilterMapping>();
        public Type ContainingEntityType { get; set; }
        public Member Member { get; private set; }

        protected CollectionMappingBase(AttributeStore underlyingStore)
        {
            if (underlyingStore != null)
                ReplaceAttributes(underlyingStore);

            SetDefaultAttribute(Attr.Mutable, true);
        }

        public void SetTable(string table, SetMode mode)
        {
            if (mode == SetMode.Internal)
                SetDefaultAttribute(Attr.Table, table);
            else
                SetAttribute(Attr.Table, table);
        }

        public void SetMember(Member member)
        {
            Member = member;
            SetDefaultAttribute(Attr.Name, memberNameFormatter.Format(member));
            
            // Needs to be element.Type
            //SetDefaultAttribute(Attr.Type, memberTypeSelector.GetItemType(member));
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
            get { return (Type)GetAttribute(Attr.ChildType); }
            set { SetAttribute(Attr.ChildType, value); }
        }

        public ICollectionMapping OtherSide { get; set; }

        public KeyMapping Key
        {
            get { return (KeyMapping)GetAttribute(Attr.Key); }
            set { SetAttribute(Attr.Key, value); }
        }

        public ElementMapping Element
        {
            get { return (ElementMapping)GetAttribute(Attr.Element); }
            set { SetAttribute(Attr.Element, value); }
        }

        public CompositeElementMapping CompositeElement
        {
            get { return (CompositeElementMapping)GetAttribute(Attr.CompositeElement); }
            set { SetAttribute(Attr.CompositeElement, value); }
        }

        public CacheMapping Cache
        {
            get { return (CacheMapping)GetAttribute(Attr.Cache); }
            set { SetAttribute(Attr.Cache, value); }
        }

        public ICollectionRelationshipMapping Relationship
        {
            get { return (ICollectionRelationshipMapping)GetAttribute(Attr.Relationship); }
            set { SetAttribute(Attr.Relationship, value); }
        }

        public bool Generic
        {
            get { return (bool)GetAttribute(Attr.Generic); }
            set { SetAttribute(Attr.Generic, value); }
        }

        public bool Lazy
        {
            get { return (bool)GetAttribute(Attr.Lazy); }
            set { SetAttribute(Attr.Lazy, value); }
        }

        public bool Inverse
        {
            get { return (bool)GetAttribute(Attr.Inverse); }
            set { SetAttribute(Attr.Inverse, value); }
        }

        public string Name
        {
            get { return (string)GetAttribute(Attr.Name); }
            set { SetAttribute(Attr.Name, value); }
        }

        public string Access
        {
            get { return (string)GetAttribute(Attr.Access); }
            set { SetAttribute(Attr.Access, value); }
        }

        public string TableName
        {
            get { return (string)GetAttribute(Attr.Table); }
            set { SetAttribute(Attr.Table, value); }
        }

        public string Schema
        {
            get { return (string)GetAttribute(Attr.Schema); }
            set { SetAttribute(Attr.Schema, value); }
        }

        public string Fetch
        {
            get { return (string)GetAttribute(Attr.Fetch); }
            set { SetAttribute(Attr.Fetch, value); }
        }

        public string Cascade
        {
            get { return (string)GetAttribute(Attr.Cascade); }
            set { SetAttribute(Attr.Cascade, value); }
        }

        public string Where
        {
            get { return (string)GetAttribute(Attr.Where); }
            set { SetAttribute(Attr.Where, value); }
        }

        public bool Mutable
        {
            get { return (bool)GetAttribute(Attr.Mutable); }
            set { SetAttribute(Attr.Mutable, value); }
        }

        public string Subselect
        {
            get { return (string)GetAttribute(Attr.Subselect); }
            set { SetAttribute(Attr.Subselect, value); }
        }

    	public TypeReference Persister
        {
            get { return (TypeReference)GetAttribute(Attr.Persister); }
            set { SetAttribute(Attr.Persister, value); }
        }

        public int BatchSize
        {
            get { return (int)GetAttribute(Attr.BatchSize); }
            set { SetAttribute(Attr.BatchSize, value); }
        }

        public string Check
        {
            get { return (string)GetAttribute(Attr.Check); }
            set { SetAttribute(Attr.Check, value); }
        }

        public TypeReference CollectionType
        {
            get { return (TypeReference)GetAttribute(Attr.CollectionType); }
            set { SetAttribute(Attr.CollectionType, value); }
        }

        public string OptimisticLock
        {
            get { return (string)GetAttribute(Attr.OptimisticLock); }
            set { SetAttribute(Attr.OptimisticLock, value); }
        }

		public abstract string OrderBy { get; set; }

        public bool Equals(CollectionMappingBase other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) &&
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
                int result = base.GetHashCode();
                result = (result * 397) ^ (filters != null ? filters.GetHashCode() : 0);
                result = (result * 397) ^ (ContainingEntityType != null ? ContainingEntityType.GetHashCode() : 0);
                result = (result * 397) ^ (Member != null ? Member.GetHashCode() : 0);
                result = (result * 397) ^ (OtherSide != null ? OtherSide.GetHashCode() : 0);
                return result;
            }
        }
    }
}
