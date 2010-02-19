using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FluentNHibernate.Mapping.Providers;
using FluentNHibernate.MappingModel;
using FluentNHibernate.Utils;

namespace FluentNHibernate.Mapping
{
    /// <summary>
    /// Represents the "Any" mapping in NHibernate. It is impossible to specify a foreign key constraint for this kind of association. For more information
    /// please reference chapter 5.2.4 in the NHibernate online documentation
    /// </summary>
    public class AnyPart<T> : IAnyMappingProvider
    {
        private readonly AttributeStore attributes = new AttributeStore();
        private readonly Type entity;
        private readonly Member property;
        private readonly AccessStrategyBuilder access;
        private readonly CascadeBuilder cascade;
        private readonly IList<string> typeColumns = new List<string>();
        private readonly IList<string> identifierColumns = new List<string>();
        private readonly IList<MetaValueMapping> metaValues = new List<MetaValueMapping>();
        private bool nextBool = true;

        public AnyPart(Type entity, Member property)
        {
            this.entity = entity;
            this.property = property;
            access = new AccessStrategyBuilder(value => attributes.Set(Attr.Access, value));
            cascade = new CascadeBuilder(value => attributes.Set(Attr.Cascade, value));
        }

        /// <summary>
        /// Defines how NHibernate will access the object for persisting/hydrating (Defaults to Property)
        /// </summary>
        public AccessStrategyBuilder<AnyPart<T>> Access
        {
            get { return new AccessStrategyBuilder<AnyPart<T>>(this, access); }
        }

        /// <summary>
        /// Cascade style (Defaults to none)
        /// </summary>
        public CascadeBuilder<AnyPart<T>> Cascade
        {
            get { return new CascadeBuilder<AnyPart<T>>(this, cascade); }
        }

        public AnyPart<T> IdentityType(Expression<Func<T, object>> expression)
        {
            return IdentityType(expression.ToMember().PropertyType);
        }

        public AnyPart<T> IdentityType<TIdentity>()
        {
            return IdentityType(typeof(TIdentity));
        }

        public AnyPart<T> IdentityType(Type type)
        {
            attributes.Set(Attr.IdType, type.AssemblyQualifiedName);
            return this;
        }

        public AnyPart<T> EntityTypeColumn(string columnName)
        {
            typeColumns.Add(columnName);
            return this;
        }

        public AnyPart<T> EntityIdentifierColumn(string columnName)
        {
            identifierColumns.Add(columnName);
            return this;
        }

        public AnyPart<T> AddMetaValue<TModel>(string valueMap)
        {
            metaValues.Add(new MetaValueMapping
            {
                Class = new TypeReference(typeof(TModel)),
                Value = valueMap,
                ContainingEntityType = entity
            });
            return this;
        }

        public AnyPart<T> Insert()
        {
            attributes.Set(Attr.Insert, nextBool);
            nextBool = true;
            return this;
        }

        public AnyPart<T> Update()
        {
            attributes.Set(Attr.Update, nextBool);
            nextBool = true;
            return this;
        }

        public AnyPart<T> ReadOnly()
        {
            attributes.Set(Attr.Insert, !nextBool);
            attributes.Set(Attr.Update, !nextBool);
            nextBool = true;
            return this;
        }

        public AnyPart<T> LazyLoad()
        {
            attributes.Set(Attr.Lazy, nextBool);
            nextBool = true;
            return this;
        }

        public AnyPart<T> OptimisticLock()
        {
            attributes.Set(Attr.OptimisticLock, nextBool);
            nextBool = true;
            return this;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public AnyPart<T> Not
        {
            get
            {
                nextBool = !nextBool;
                return this;
            }
        }

        AnyMapping IAnyMappingProvider.GetAnyMapping()
        {
            var mapping = new AnyMapping(attributes.Clone());

            if (typeColumns.Count() == 0)
                throw new InvalidOperationException("<any> mapping is not valid without specifying an Entity Type Column");
            if (identifierColumns.Count() == 0)
                throw new InvalidOperationException("<any> mapping is not valid without specifying an Entity Identifier Column");
            if (!mapping.IsSpecified(Attr.IdType))
                throw new InvalidOperationException("<any> mapping is not valid without specifying an IdType");

            mapping.ContainingEntityType = entity;

            if (!mapping.IsSpecified(Attr.Name))
                mapping.Name = property.Name;

            if (!mapping.IsSpecified(Attr.MetaType))
            {
                if (metaValues.Count() > 0)
                {
                    metaValues.Each(mapping.AddMetaValue);
                    mapping.MetaType = new TypeReference(typeof(string));
                }
                else
                    mapping.MetaType = new TypeReference(property.PropertyType);
            }

            foreach (var column in typeColumns)
                mapping.AddTypeColumn(new ColumnMapping { Name = column });

            foreach (var column in identifierColumns)
                mapping.AddIdentifierColumn(new ColumnMapping { Name = column });

            return mapping;
        }
    }
}
