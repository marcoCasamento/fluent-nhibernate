using System;
using NHibernate.Properties;

namespace FluentNHibernate.Mapping
{
    /// <summary>
    /// Access strategy mapping builder.
    /// </summary>
    /// <typeparam name="T">Mapping part to be applied to</typeparam>
    public class AccessStrategyBuilder<T>
    {
        private readonly T parent;
        readonly AccessStrategyBuilder innerBuilder;

        /// <summary>
        /// Access strategy mapping builder.
        /// </summary>
        /// <param name="parent">Instance of the parent mapping part.</param>
        /// <param name="innerBuilder">Inner, non-fluent, access builder</param>
        public AccessStrategyBuilder(T parent, AccessStrategyBuilder innerBuilder)
        {
            this.parent = parent;
            this.innerBuilder = innerBuilder;
        }

        /// <summary>
        /// Sets the access-strategy to property.
        /// </summary>
        public T Property()
        {
            innerBuilder.Property();

            return parent;
        }

        /// <summary>
        /// Sets the access-strategy to field.
        /// </summary>
        public T Field()
        {
            innerBuilder.Field();

            return parent;
        }

        /// <summary>
        /// Sets the access-strategy to use the backing-field of an auto-property.
        /// </summary>
        public T BackingField()
        {
            innerBuilder.BackingField();

            return parent;
        }

        /// <summary>
        /// Sets the access-strategy to field and the naming-strategy to camelcase (field.camelcase).
        /// </summary>
        public T CamelCaseField()
        {
            innerBuilder.CamelCaseField();

            return parent;
        }

        /// <summary>
        /// Sets the access-strategy to field and the naming-strategy to camelcase, with the specified prefix.
        /// </summary>
        /// <param name="prefix">Naming-strategy prefix</param>
        public T CamelCaseField(Prefix prefix)
        {
            innerBuilder.CamelCaseField(prefix);

            return parent;
        }

        /// <summary>
        /// Sets the access-strategy to field and the naming-strategy to lowercase.
        /// </summary>
        public T LowerCaseField()
        {
            innerBuilder.LowerCaseField();

            return parent;
        }

        /// <summary>
        /// Sets the access-strategy to field and the naming-strategy to lowercase, with the specified prefix.
        /// </summary>
        /// <param name="prefix">Naming-strategy prefix</param>
        public T LowerCaseField(Prefix prefix)
        {
            innerBuilder.LowerCaseField(prefix);

            return parent;
        }

        /// <summary>
        /// Sets the access-strategy to field and the naming-strategy to pascalcase, with the specified prefix.
        /// </summary>
        /// <param name="prefix">Naming-strategy prefix</param>
        public T PascalCaseField(Prefix prefix)
        {
            innerBuilder.PascalCaseField(prefix);
            
            return parent;
        }

        /// <summary>
        /// Sets the access-strategy to read-only property (nosetter) and the naming-strategy to camelcase.
        /// </summary>
        public T ReadOnlyPropertyThroughCamelCaseField()
        {
            innerBuilder.ReadOnlyPropertyThroughCamelCaseField();

            return parent;
        }

        /// <summary>
        /// Sets the access-strategy to read-only property (nosetter) and the naming-strategy to camelcase, with the specified prefix.
        /// </summary>
        /// <param name="prefix">Naming-strategy prefix</param>
        public T ReadOnlyPropertyThroughCamelCaseField(Prefix prefix)
        {
            innerBuilder.ReadOnlyPropertyThroughCamelCaseField(prefix);

            return parent;
        }

        /// <summary>
        /// Sets the access-strategy to read-only property (nosetter) and the naming-strategy to lowercase.
        /// </summary>
        public T ReadOnlyPropertyThroughLowerCaseField()
        {
            innerBuilder.ReadOnlyPropertyThroughLowerCaseField();

            return parent;
        }

        /// <summary>
        /// Sets the access-strategy to read-only property (nosetter) and the naming-strategy to lowercase.
        /// </summary>
        /// <param name="prefix">Naming-strategy prefix</param>
        public T ReadOnlyPropertyThroughLowerCaseField(Prefix prefix)
        {
            innerBuilder.ReadOnlyPropertyThroughLowerCaseField(prefix);

            return parent;
        }

        /// <summary>
        /// Sets the access-strategy to read-only property (nosetter) and the naming-strategy to pascalcase, with the specified prefix.
        /// </summary>
        /// <param name="prefix">Naming-strategy prefix</param>
        public T ReadOnlyPropertyThroughPascalCaseField(Prefix prefix)
        {
            innerBuilder.ReadOnlyPropertyThroughPascalCaseField(prefix);

            return parent;
        }

        /// <summary>
        /// Sets the access-strategy to use the type referenced.
        /// </summary>
        /// <param name="propertyAccessorAssemblyQualifiedClassName">Assembly qualified name of the type to use as the access-strategy</param>
        public T Using(string propertyAccessorAssemblyQualifiedClassName)
        {
            innerBuilder.Using(propertyAccessorAssemblyQualifiedClassName);

            return parent;
        }

        /// <summary>
        /// Sets the access-strategy to use the type referenced.
        /// </summary>
        /// <param name="propertyAccessorClassType">Type to use as the access-strategy</param>
        public T Using(Type propertyAccessorClassType)
        {
            innerBuilder.Using(propertyAccessorClassType);

            return parent;
        }

        /// <summary>
        /// Sets the access-strategy to use the type referenced.
        /// </summary>
        /// <typeparam name="TPropertyAccessorClass">Type to use as the access-strategy</typeparam>
        public T Using<TPropertyAccessorClass>() where TPropertyAccessorClass : IPropertyAccessor
        {
            innerBuilder.Using<TPropertyAccessorClass>();

            return parent;
        }

        public T NoOp()
        {
            innerBuilder.NoOp();

            return parent;
        }

        public T None()
        {
            innerBuilder.None();

            return parent;
        }
    }
}