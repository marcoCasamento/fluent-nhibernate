using System;
using System.Linq.Expressions;
using FluentNHibernate.MappingModel.Identity;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.ClassBased
{
    public class ClassMapping : ClassMappingBase, IMapping, ITypeMapping
    {
        private readonly AttributeStore<ClassMapping> attributes;

        public ClassMapping(Type type)
            : this(new AttributeStore())
        {
            Type = type;
        }

        public ClassMapping(AttributeStore store)
        {
            attributes = new AttributeStore<ClassMapping>(store);
            attributes.SetDefault(x => x.Mutable, true);
        }

        public IIdentityMapping Id
        {
            get { return attributes.Get(x => x.Id); }
            set { attributes.Set(x => x.Id, value); }
        }

        public NaturalIdMapping NaturalId
        {
            get { return attributes.Get(x => x.NaturalId); }
            set { attributes.Set(x => x.NaturalId, value); }
        }

        public override string Name
        {
            get { return attributes.Get(x => x.Name); }
            set { attributes.Set(x => x.Name, value); }
        }

        public override Type Type
        {
            get { return attributes.Get(x => x.Type); }
            set { attributes.Set(x => x.Type, value); }
        }

        public CacheMapping Cache
        {
            get { return attributes.Get(x => x.Cache); }
            set { attributes.Set(x => x.Cache, value); }
        }

        public VersionMapping Version
        {
            get { return attributes.Get(x => x.Version); }
            set { attributes.Set(x => x.Version, value); }
        }

        public DiscriminatorMapping Discriminator
        {
            get { return attributes.Get(x => x.Discriminator); }
            set { attributes.Set(x => x.Discriminator, value); }
        }

        public TuplizerMapping Tuplizer
        {
            get { return attributes.Get(x => x.Tuplizer); }
            set { attributes.Set(x => x.Tuplizer, value); }
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessClass(this);            

            if (Id != null)
                visitor.Visit(Id);

            if (NaturalId != null)
                visitor.Visit(NaturalId);

            if (Discriminator != null)
                visitor.Visit(Discriminator);

            if (Cache != null)
                visitor.Visit(Cache);

            if (Version != null)
                visitor.Visit(Version);

            if (Tuplizer != null)
                visitor.Visit(Tuplizer);

            base.AcceptVisitor(visitor);
        }

        public override void MergeAttributes(AttributeStore store)
        {
            attributes.Merge(new AttributeStore<ClassMapping>(store));
        }

        public string TableName
        {
            get { return attributes.Get(x => x.TableName); }
            set { attributes.Set(x => x.TableName, value); }
        }

        public int BatchSize
        {
            get { return attributes.Get(x => x.BatchSize); }
            set { attributes.Set(x => x.BatchSize, value); }
        }

        public object DiscriminatorValue
        {
            get { return attributes.Get(x => x.DiscriminatorValue); }
            set { attributes.Set(x => x.DiscriminatorValue, value); }
        }

        public string Schema
        {
            get { return attributes.Get(x => x.Schema); }
            set { attributes.Set(x => x.Schema, value); }
        }

        public bool Lazy
        {
            get { return attributes.Get(x => x.Lazy); }
            set { attributes.Set(x => x.Lazy, value); }
        }

        public bool Mutable
        {
            get { return attributes.Get(x => x.Mutable); }
            set { attributes.Set(x => x.Mutable, value); }
        }

        public bool DynamicUpdate
        {
            get { return attributes.Get(x => x.DynamicUpdate); }
            set { attributes.Set(x => x.DynamicUpdate, value); }
        }

        public bool DynamicInsert
        {
            get { return attributes.Get(x => x.DynamicInsert); }
            set { attributes.Set(x => x.DynamicInsert, value); }
        }

        public string OptimisticLock
        {
            get { return attributes.Get(x => x.OptimisticLock); }
            set { attributes.Set(x => x.OptimisticLock, value); }
        }

        public string Polymorphism
        {
            get { return attributes.Get(x => x.Polymorphism); }
            set { attributes.Set(x => x.Polymorphism, value); }
        }

        public string Persister
        {
            get { return attributes.Get(x => x.Persister); }
            set { attributes.Set(x => x.Persister, value); }
        }

        public string Where
        {
            get { return attributes.Get(x => x.Where); }
            set { attributes.Set(x => x.Where, value); }
        }

        public string Check
        {
            get { return attributes.Get(x => x.Check); }
            set { attributes.Set(x => x.Check, value); }
        }

        public string Proxy
        {
            get { return attributes.Get(x => x.Proxy); }
            set { attributes.Set(x => x.Proxy, value); }
        }

        public bool SelectBeforeUpdate
        {
            get { return attributes.Get(x => x.SelectBeforeUpdate); }
            set { attributes.Set(x => x.SelectBeforeUpdate, value); }
        }

        public bool Abstract
        {
            get { return attributes.Get(x => x.Abstract); }
            set { attributes.Set(x => x.Abstract, value); }
        }

        public string Subselect
        {
            get { return attributes.Get(x => x.Subselect); }
            set { attributes.Set(x => x.Subselect, value); }
        }

        public string SchemaAction
        {
            get { return attributes.Get(x => x.SchemaAction); }
            set { attributes.Set(x => x.SchemaAction, value); }
        }

        public string EntityName
        {
            get { return attributes.Get(x => x.EntityName); }
            set { attributes.Set(x => x.EntityName, value); }
        }       

        public override bool IsSpecified(string property)
        {
            return attributes.IsSpecified(property);
        }

        public bool HasValue<TResult>(Expression<Func<ClassMapping, TResult>> property)
        {
            return attributes.HasValue(property);
        }

        public void SetDefaultValue<TResult>(Expression<Func<ClassMapping, TResult>> property, TResult value)
        {
            attributes.SetDefault(property, value);
        }

        public bool Equals(ClassMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) &&
                Equals(other.attributes, attributes);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(ClassMapping)) return false;
            return Equals((ClassMapping)obj);
        }

        public override int GetHashCode()
        {
            return (attributes != null ? attributes.GetHashCode() : 0);
        }

        public void AddChild(IMapping child)
        {
            if (child is CacheMapping)
                Cache = (CacheMapping)child;
        }
    }
}