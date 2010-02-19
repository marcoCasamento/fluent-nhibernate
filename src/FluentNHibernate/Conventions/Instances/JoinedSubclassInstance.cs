using System;
using System.Diagnostics;
using System.Reflection;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;
using NHibernate.Persister.Entity;

namespace FluentNHibernate.Conventions.Instances
{
    public class JoinedSubclassInstance : JoinedSubclassInspector, IJoinedSubclassInstance
    {
        private readonly JoinedSubclassMapping mapping;
        private bool nextBool = true;

        public JoinedSubclassInstance(JoinedSubclassMapping mapping)
            : base(mapping)
        {
            this.mapping = mapping;
        }

        public new IKeyInstance Key
        {
            get
            {
                if (mapping.Key == null)
                    mapping.Key = new KeyMapping();

                return new KeyInstance(mapping.Key);
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public IJoinedSubclassInstance Not
        {
            get
            {
                nextBool = !nextBool;
                return this;
            }
        }

        public new void Abstract()
        {
            if (!mapping.IsSpecified(Attr.Abstract))
                mapping.Abstract = nextBool;
            nextBool = true;
        }

        public new void Check(string constraint)
        {
            if (!mapping.IsSpecified(Attr.Abstract))
                mapping.Check = constraint;
        }

        public new void DynamicInsert()
        {
            if (!mapping.IsSpecified(Attr.DynamicInsert))
                mapping.DynamicInsert = nextBool;
            nextBool = true;
        }

        public new void DynamicUpdate()
        {
            if (!mapping.IsSpecified(Attr.DynamicUpdate))
                mapping.DynamicUpdate = nextBool;
            nextBool = true;
        }

        public new void LazyLoad()
        {
            if (!mapping.IsSpecified(Attr.Lazy))
                mapping.Lazy = nextBool;
            nextBool = true;
        }

        public new void Proxy(Type type)
        {
            if (!mapping.IsSpecified(Attr.Proxy))
                mapping.Proxy = type.AssemblyQualifiedName;
        }

        public new void Proxy<T>()
        {
            if (!mapping.IsSpecified(Attr.Proxy))
                mapping.Proxy = typeof(T).AssemblyQualifiedName;
        }

        public void Schema(string schema)
        {
            if (!mapping.IsSpecified(Attr.Schema))
                mapping.Schema = schema;
        }

        public new void SelectBeforeUpdate()
        {
            if (!mapping.IsSpecified(Attr.SelectBeforeUpdate))
                mapping.SelectBeforeUpdate = nextBool;
            nextBool = true;
        }

        public void Table(string tableName)
        {
            if (!mapping.IsSpecified(Attr.Table))
                mapping.TableName = tableName;
        }

        public void Subselect(string subselect)
        {
            if (!mapping.IsSpecified(Attr.Subselect))
                mapping.Subselect = subselect;
        }

        public void Persister<T>() where T : IEntityPersister
        {
            if (!mapping.IsSpecified(Attr.Persister))
                mapping.Persister = new TypeReference(typeof(T));
        }

        public void Persister(Type type)
        {
            if (!mapping.IsSpecified(Attr.Persister))
                mapping.Persister = new TypeReference(type);
        }

        public void Persister(string type)
        {
            if (!mapping.IsSpecified(Attr.Persister))
                mapping.Persister = new TypeReference(type);
        }

        public void BatchSize(int batchSize)
        {
            if (!mapping.IsSpecified(Attr.BatchSize))
                mapping.BatchSize = batchSize;
        }
    }
}