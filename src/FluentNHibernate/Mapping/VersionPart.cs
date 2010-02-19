using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using FluentNHibernate.Mapping.Providers;
using FluentNHibernate.MappingModel;

namespace FluentNHibernate.Mapping
{
    public class VersionPart : IVersionMappingProvider
    {
        private readonly Type entity;
        private readonly Member property;
        private readonly AccessStrategyBuilder access;
        private readonly VersionGeneratedBuilder<IVersionMappingProvider> generated;
        private readonly AttributeStore attributes = new AttributeStore();
        private readonly AttributeStore columnAttributes = new AttributeStore();
        private readonly List<string> columns = new List<string>();
        private bool nextBool = true;

        public VersionPart(Type entity, Member property)
        {
            this.entity = entity;
            this.property = property;
            access = new AccessStrategyBuilder(value => attributes.Set(Attr.Access, value));
            generated = new VersionGeneratedBuilder<IVersionMappingProvider>(this, value => attributes.Set(Attr.Generated, value));
        }

        VersionMapping IVersionMappingProvider.GetVersionMapping()
        {
            var mapping = new VersionMapping(attributes.Clone());

            mapping.ContainingEntityType = entity;

            mapping.SetDefaultValue(Attr.Name, property.Name);
            mapping.SetDefaultValue(Attr.Name, property.PropertyType == typeof(DateTime) ? new TypeReference("timestamp") : new TypeReference(property.PropertyType));
            mapping.AddDefaultColumn(new ColumnMapping(columnAttributes.Clone()) { Name = property.Name });

            columns.ForEach(column => mapping.AddColumn(new ColumnMapping(columnAttributes.Clone()) { Name = column }));

            return mapping;
        }

        public VersionGeneratedBuilder<IVersionMappingProvider> Generated
        {
            get { return generated; }
        }

        public AccessStrategyBuilder<VersionPart> Access
        {
            get { return new AccessStrategyBuilder<VersionPart>(this, access); }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public VersionPart Not
        {
            get
            {
                nextBool = !nextBool;
                return this;
            }
        }

        public VersionPart Column(string name)
        {
            columns.Add(name);
            return this;
        }

        public VersionPart UnsavedValue(string value)
        {
            attributes.Set(Attr.UnsavedValue, value);
            return this;
        }

        public VersionPart Length(int length)
        {
            columnAttributes.Set(Attr.Length, length);
            return this;
        }

        public VersionPart Precision(int precision)
        {
            columnAttributes.Set(Attr.Precision, precision);
            return this;
        }

        public VersionPart Scale(int scale)
        {
            columnAttributes.Set(Attr.Scale, scale);
            return this;
        }

        public VersionPart Nullable()
        {
            columnAttributes.Set(Attr.NotNull, !nextBool);
            nextBool = true;
            return this;
        }

        public VersionPart Unique()
        {
            columnAttributes.Set(Attr.Unique, nextBool);
            nextBool = true;
            return this;
        }

        public VersionPart UniqueKey(string keyColumns)
        {
            columnAttributes.Set(Attr.UniqueKey, keyColumns);
            return this;
        }

        public VersionPart Index(string index)
        {
            columnAttributes.Set(Attr.Index, index);
            return this;
        }

        public VersionPart Check(string constraint)
        {
            columnAttributes.Set(Attr.Check, constraint);
            return this;
        }

        public VersionPart Default(object value)
        {
            columnAttributes.Set(Attr.Default, value.ToString());
            return this;
        }

        public VersionPart CustomType<T>()
        {
            attributes.Set(Attr.Type, new TypeReference(typeof(T)));
            return this;
        }

        public VersionPart CustomType(Type type)
        {
            attributes.Set(Attr.Type, new TypeReference(type));
            return this;
        }

        public VersionPart CustomType(string type)
        {
            attributes.Set(Attr.Type, new TypeReference(type));
            return this;
        }

        public VersionPart CustomSqlType(string sqlType)
        {
            columnAttributes.Set(Attr.SqlType, sqlType);
            return this;
        }
    }
}