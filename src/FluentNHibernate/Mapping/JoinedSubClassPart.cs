using System;
using System.Collections.Generic;
using System.Diagnostics;
using FluentNHibernate.Mapping.Providers;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;

namespace FluentNHibernate.Mapping
{
    public class JoinedSubClassPart<TSubclass> : ClasslikeMapBase<TSubclass>, ISubclassMappingProvider
    {
        private readonly ColumnMappingCollection<JoinedSubClassPart<TSubclass>> columns;
        private readonly List<ISubclassMapping> subclassMappings = new List<ISubclassMapping>();
        private readonly AttributeStore attributes;
        private bool nextBool = true;

        public JoinedSubClassPart(string keyColumn)
            : this(new AttributeStore())
        {
            columns.Add(keyColumn);
        }

        public JoinedSubClassPart(AttributeStore underlyingStore)
        {
            attributes = underlyingStore.Clone();
            columns = new ColumnMappingCollection<JoinedSubClassPart<TSubclass>>(this);
        }

        public virtual void JoinedSubClass<TNextSubclass>(string keyColumn, Action<JoinedSubClassPart<TNextSubclass>> action)
        {
            var subclass = new JoinedSubClassPart<TNextSubclass>(keyColumn);

            action(subclass);

            subclasses[typeof(TNextSubclass)] = subclass;

            subclassMappings.Add(((ISubclassMappingProvider)subclass).GetSubclassMapping());
        }

        public ColumnMappingCollection<JoinedSubClassPart<TSubclass>> KeyColumns
        {
            get { return columns; }
        }

        public JoinedSubClassPart<TSubclass> Table(string tableName)
        {
            attributes.Set(Attr.Table, tableName);
            return this;
        }

        public JoinedSubClassPart<TSubclass> Schema(string schema)
        {
            attributes.Set(Attr.Schema, schema);
            return this;
        }

        public JoinedSubClassPart<TSubclass> CheckConstraint(string constraintName)
        {
            attributes.Set(Attr.Check, constraintName);
            return this;
        }

        public JoinedSubClassPart<TSubclass> Proxy(Type type)
        {
            attributes.Set(Attr.Proxy, type.AssemblyQualifiedName);
            return this;
        }

        public JoinedSubClassPart<TSubclass> Proxy<T>()
        {
            return Proxy(typeof(T));
        }

        public JoinedSubClassPart<TSubclass> LazyLoad()
        {
            attributes.Set(Attr.Lazy, nextBool);
            nextBool = true;
            return this;
        }

        public JoinedSubClassPart<TSubclass> DynamicUpdate()
        {
            attributes.Set(Attr.DynamicUpdate, nextBool);
            nextBool = true;
            return this;
        }

        public JoinedSubClassPart<TSubclass> DynamicInsert()
        {
            attributes.Set(Attr.DynamicInsert, nextBool);
            nextBool = true;
            return this;
        }

        public JoinedSubClassPart<TSubclass> SelectBeforeUpdate()
        {
            attributes.Set(Attr.SelectBeforeUpdate, nextBool);
            nextBool = true;
            return this;
        }

        public JoinedSubClassPart<TSubclass> Abstract()
        {
            attributes.Set(Attr.Abstract, nextBool);
            nextBool = true;
            return this;
        }

        /// <summary>
        /// Specifies an entity-name.
        /// </summary>
        /// <remarks>See http://nhforge.org/blogs/nhibernate/archive/2008/10/21/entity-name-in-action-a-strongly-typed-entity.aspx</remarks>
        public JoinedSubClassPart<TSubclass> EntityName(string entityName)
        {
            attributes.Set(Attr.EntityName, entityName);
            return this;
        }

        /// <summary>
        /// Inverts the next boolean
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public JoinedSubClassPart<TSubclass> Not
        {
            get
            {
                nextBool = !nextBool;
                return this;
            }
        }

        ISubclassMapping ISubclassMappingProvider.GetSubclassMapping()
        {
            var mapping = new JoinedSubclassMapping(attributes.Clone());

            mapping.Key = new KeyMapping { ContainingEntityType = typeof(TSubclass) };
            mapping.Name = typeof(TSubclass).AssemblyQualifiedName;
            mapping.Type = typeof(TSubclass);

            foreach (var column in columns)
                mapping.Key.AddColumn(column);

            foreach (var property in properties)
                mapping.AddProperty(property.GetPropertyMapping());

            foreach (var component in components)
                mapping.AddComponent(component.GetComponentMapping());

            foreach (var oneToOne in oneToOnes)
                mapping.AddOneToOne(oneToOne.GetOneToOneMapping());

            foreach (var collection in collections)
                mapping.AddCollection(collection.GetCollectionMapping());

            foreach (var reference in references)
                mapping.AddReference(reference.GetManyToOneMapping());

            foreach (var any in anys)
                mapping.AddAny(any.GetAnyMapping());

            return mapping;
        }
    }
}