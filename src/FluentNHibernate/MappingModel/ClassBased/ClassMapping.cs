using System;
using FluentNHibernate.MappingModel.Identity;
using FluentNHibernate.Utils;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.ClassBased
{
    public class ClassMapping : ClassMappingBase
    {
        public ClassMapping()
            : this(new AttributeStore())
        {}

        public ClassMapping(AttributeStore store)
        {
            if (store != null)
                ReplaceAttributes(store);
            
            SetDefaultAttribute(Attr.Mutable, true);
            SetDefaultAttribute(Attr.Name, Type.AssemblyQualifiedName);
            SetDefaultAttribute(Attr.Table, GetDefaultTableName(Type));
        }

        public IIdentityMapping Id
        {
            get { return (IIdentityMapping)GetAttribute(Attr.Id); }
            set { SetAttribute(Attr.Id, value); }
        }

        public CacheMapping Cache
        {
            get { return (CacheMapping)GetAttribute(Attr.Cache); }
            set { SetAttribute(Attr.Cache, value); }
        }

        public VersionMapping Version
        {
            get { return (VersionMapping)GetAttribute(Attr.Version); }
            set { SetAttribute(Attr.Version, value); }
        }

        public DiscriminatorMapping Discriminator
        {
            get { return (DiscriminatorMapping)GetAttribute(Attr.Discriminator); }
            set { SetAttribute(Attr.Discriminator, value); }
        }

        public TuplizerMapping Tuplizer
        {
            get { return (TuplizerMapping)GetAttribute(Attr.Tuplizer); }
            set { SetAttribute(Attr.Tuplizer, value); }
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessClass(this);            

            if (Id != null)
                visitor.Visit(Id);

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

        public string TableName
        {
            get { return (string)GetAttribute(Attr.Table); }
            set { SetAttribute(Attr.Table, value); }
        }

        public int BatchSize
        {
            get { return (int)GetAttribute(Attr.BatchSize); }
            set { SetAttribute(Attr.BatchSize, value); }
        }

        public object DiscriminatorValue
        {
            get { return GetAttribute(Attr.DiscriminatorValue); }
            set { SetAttribute(Attr.DiscriminatorValue, value); }
        }

        public string Schema
        {
            get { return (string)GetAttribute(Attr.Schema); }
            set { SetAttribute(Attr.Schema, value); }
        }

        public bool Lazy
        {
            get { return (bool)GetAttribute(Attr.Lazy); }
            set { SetAttribute(Attr.Lazy, value); }
        }

        public bool Mutable
        {
            get { return (bool)GetAttribute(Attr.Mutable); }
            set { SetAttribute(Attr.Mutable, value); }
        }

        public bool DynamicUpdate
        {
            get { return (bool)GetAttribute(Attr.DynamicUpdate); }
            set { SetAttribute(Attr.DynamicUpdate, value); }
        }

        public bool DynamicInsert
        {
            get { return (bool)GetAttribute(Attr.DynamicInsert); }
            set { SetAttribute(Attr.DynamicInsert, value); }
        }

        public string OptimisticLock
        {
            get { return (string)GetAttribute(Attr.OptimisticLock); }
            set { SetAttribute(Attr.OptimisticLock, value); }
        }

        public string Polymorphism
        {
            get { return (string)GetAttribute(Attr.Polymorphism); }
            set { SetAttribute(Attr.Polymorphism, value); }
        }

        public string Persister
        {
            get { return (string)GetAttribute(Attr.Persister); }
            set { SetAttribute(Attr.Persister, value); }
        }

        public string Where
        {
            get { return (string)GetAttribute(Attr.Where); }
            set { SetAttribute(Attr.Where, value); }
        }

        public string Check
        {
            get { return (string)GetAttribute(Attr.Check); }
            set { SetAttribute(Attr.Check, value); }
        }

        public string Proxy
        {
            get { return (string)GetAttribute(Attr.Proxy); }
            set { SetAttribute(Attr.Proxy, value); }
        }

        public bool SelectBeforeUpdate
        {
            get { return (bool)GetAttribute(Attr.SelectBeforeUpdate); }
            set { SetAttribute(Attr.SelectBeforeUpdate, value); }
        }

        public bool Abstract
        {
            get { return (bool)GetAttribute(Attr.Abstract); }
            set { SetAttribute(Attr.Abstract, value); }
        }

        public string Subselect
        {
            get { return (string)GetAttribute(Attr.Subselect); }
            set { SetAttribute(Attr.Subselect, value); }
        }

        public string SchemaAction
        {
            get { return (string)GetAttribute(Attr.SchemaAction); }
            set { SetAttribute(Attr.SchemaAction, value); }
        }

        public string EntityName
        {
            get { return (string)GetAttribute(Attr.EntityName); }
            set { SetAttribute(Attr.EntityName, value); }
        }

        private string GetDefaultTableName(Type type)
        {
            var tableName = type.Name;

            if (type.IsGenericType)
            {
                // special case for generics: GenericType_GenericParameterType
                tableName = type.Name.Substring(0, type.Name.IndexOf('`'));

                foreach (var argument in type.GetGenericArguments())
                {
                    tableName += "_";
                    tableName += argument.Name;
                }
            }

            return "`" + tableName + "`";
        }
    }
}