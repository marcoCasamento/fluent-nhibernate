using System;
using System.Diagnostics;
using FluentNHibernate.Mapping.Providers;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;

namespace FluentNHibernate.Mapping
{
    public class DiscriminatorPart : IDiscriminatorMappingProvider
    {
        private readonly string columnName;
        private readonly Type entity;
        private readonly Action<Type, ISubclassMappingProvider> setter;
        private readonly TypeReference discriminatorValueType;
        private readonly AttributeStore attributes = new AttributeStore();
        private readonly AttributeStore columnAttributes = new AttributeStore();
        private bool nextBool = true;

        public DiscriminatorPart(string columnName, Type entity, Action<Type, ISubclassMappingProvider> setter, TypeReference discriminatorValueType)
        {
            this.columnName = columnName;
            this.entity = entity;
            this.setter = setter;
            this.discriminatorValueType = discriminatorValueType;
        }

        DiscriminatorMapping IDiscriminatorMappingProvider.GetDiscriminatorMapping()
        {
            var mapping = new DiscriminatorMapping(attributes.Clone(), discriminatorValueType)
            {
                ContainingEntityType = entity,
            };

            mapping.AddColumn(new ColumnMapping(columnAttributes.Clone()) { Name = columnName });

            return mapping;
        }

        [Obsolete("Inline definitions of subclasses are depreciated. Please create a derived class from SubclassMap in the same way you do with ClassMap.")]
        public DiscriminatorPart SubClass<TSubClass>(object discriminatorValue, Action<SubClassPart<TSubClass>> action)
        {
            var subclass = new SubClassPart<TSubClass>(this, discriminatorValue);

            action(subclass);
            setter(typeof(TSubClass), subclass);

            return this;
        }

        [Obsolete("Inline definitions of subclasses are depreciated. Please create a derived class from SubclassMap in the same way you do with ClassMap.")]
        public DiscriminatorPart SubClass<TSubClass>(Action<SubClassPart<TSubClass>> action)
        {
            return SubClass(null, action);
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public DiscriminatorPart Not
        {
             get
             {
                 nextBool = !nextBool;
                 return this;
             }
        }

        /// <summary>
        /// Force NHibernate to always select using the discriminator value, even when selecting all subclasses. This
        /// can be useful when your table contains more discriminator values than you have classes (legacy).
        /// </summary>
        /// <remarks>Sets the "force" attribute.</remarks>
        public DiscriminatorPart AlwaysSelectWithValue()
        {
            attributes.Set(Attr.Force, nextBool);
            nextBool = true;
            return this;
        }

        /// <summary>
        /// Set this discriminator as read-only. Call this if your discriminator column is also part of a mapped composite identifier.
        /// </summary>
        /// <returns>Sets the "insert" attribute.</returns>
        public DiscriminatorPart ReadOnly()
        {
            attributes.Set(Attr.Insert, !nextBool);
            nextBool = true;
            return this;
        }

        /// <summary>
        /// An arbitrary SQL expression that is executed when a type has to be evaluated. Allows content-based discrimination.
        /// </summary>
        /// <param name="sql">SQL expression</param>
        public DiscriminatorPart Formula(string sql)
        {
            attributes.Set(Attr.Formula, sql);
            return this;
        }

        public DiscriminatorPart Precision(int precision)
        {
            columnAttributes.Set(Attr.Precision, precision);
            return this;
        }

        public DiscriminatorPart Length(int length)
        {
            columnAttributes.Set(Attr.Length, length);
            return this;
        }

        public DiscriminatorPart Scale(int scale)
        {
            columnAttributes.Set(Attr.Scale, scale);
            return this;
        }

        public DiscriminatorPart Nullable()
        {
            columnAttributes.Set(Attr.NotNull, !nextBool);
            nextBool = true;
            return this;
        }

        public DiscriminatorPart Unique()
        {
            columnAttributes.Set(Attr.Unique, nextBool);
            nextBool = true;
            return this;
        }

        public DiscriminatorPart UniqueKey(string keyColumns)
        {
            columnAttributes.Set(Attr.UniqueKey, keyColumns);
            return this;
        }

        public DiscriminatorPart Index(string index)
        {
            columnAttributes.Set(Attr.Index, index);
            return this;
        }

        public DiscriminatorPart Check(string constraint)
        {
            columnAttributes.Set(Attr.Check, constraint);
            return this;
        }

        public DiscriminatorPart Default(object value)
        {
            columnAttributes.Set(Attr.Default, value.ToString());
            return this;
        }

        public DiscriminatorPart CustomType<T>()
        {
            attributes.Set(Attr.Type, new TypeReference(typeof(T)));
            return this;
        }

        public DiscriminatorPart CustomType(Type type)
        {
            attributes.Set(Attr.Type, new TypeReference(type));
            return this;
        }

        public DiscriminatorPart CustomType(string type)
        {
            attributes.Set(Attr.Type, new TypeReference(type));
            return this;
        }
    }
}