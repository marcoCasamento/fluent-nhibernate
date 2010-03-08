using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using FluentNHibernate.Mapping.Providers;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.Utils;

namespace FluentNHibernate.Mapping
{
    public abstract class ClasslikeMapBase<T>
    {
        readonly IMappingStructure structure;

        protected ClasslikeMapBase(IMappingStructure structure)
        {
            this.structure = structure;
        }

        public PropertyPart Map(Expression<Func<T, object>> expression)
        {
            return Map(expression, null);
        }

        public PropertyPart Map(Expression<Func<T, object>> expression, string columnName)
        {
            return Map(expression.ToMember(), columnName);
        }

        protected virtual PropertyPart Map(Member property, string columnName)
        {
            var propertyStructure = new MemberStructure<PropertyMapping>(property);
            var propertyMap = new PropertyPart(propertyStructure);

            if (!string.IsNullOrEmpty(columnName))
                propertyMap.Column(columnName);

            structure.AddChild(propertyStructure);

            return propertyMap;
        }

        public ManyToOnePart<TOther> References<TOther>(Expression<Func<T, TOther>> expression)
        {
            return References(expression, null);
        }

        public ManyToOnePart<TOther> References<TOther>(Expression<Func<T, TOther>> expression, string columnName)
        {
            return References<TOther>(expression.ToMember(), columnName);
        }

        public ManyToOnePart<TOther> References<TOther>(Expression<Func<T, object>> expression)
        {
            return References<TOther>(expression, null);
        }

        public ManyToOnePart<TOther> References<TOther>(Expression<Func<T, object>> expression, string columnName)
        {
            return References<TOther>(expression.ToMember(), columnName);
        }

        protected virtual ManyToOnePart<TOther> References<TOther>(Member property, string columnName)
        {
            var referencesStructure = new MemberStructure<ManyToOneMapping>(property);
            var part = new ManyToOnePart<TOther>(referencesStructure);

            if (columnName != null)
                part.Column(columnName);

            structure.AddChild(referencesStructure);

            return part;
        }

        public AnyPart<TOther> ReferencesAny<TOther>(Expression<Func<T, TOther>> expression)
        {
            return ReferencesAny<TOther>(expression.ToMember());
        }

        protected virtual AnyPart<TOther> ReferencesAny<TOther>(Member property)
        {
            var anyStructure = new MemberStructure<AnyMapping>(property);
            var part = new AnyPart<TOther>(anyStructure);

            structure.AddChild(anyStructure);

            return part;
        }

        public OneToOnePart<TOther> HasOne<TOther>(Expression<Func<T, Object>> expression)
        {
            return HasOne<TOther>(expression.ToMember());
        }

        public OneToOnePart<TOther> HasOne<TOther>(Expression<Func<T, TOther>> expression)
        {
            return HasOne<TOther>(expression.ToMember());
        }

        protected virtual OneToOnePart<TOther> HasOne<TOther>(Member property)
        {
            var oneToOneStructure = new MemberStructure<OneToOneMapping>(property);
            var part = new OneToOnePart<TOther>(oneToOneStructure);

            structure.AddChild(oneToOneStructure);

            return part;
        }

        public DynamicComponentPart<IDictionary> DynamicComponent(Expression<Func<T, IDictionary>> expression, Action<DynamicComponentPart<IDictionary>> action)
        {
            return DynamicComponent(expression.ToMember(), action);
        }

        protected DynamicComponentPart<IDictionary> DynamicComponent(Member property, Action<DynamicComponentPart<IDictionary>> action)
        {
            var componentStructure = new TypeStructure<ComponentMapping>(typeof(IDictionary));
            var part = new DynamicComponentPart<IDictionary>(componentStructure);
            
            action(part);

            structure.AddChild(componentStructure);

            return part;
        }

        /// <summary>
        /// Creates a component reference. This is a place-holder for a component that is defined externally with a
        /// <see cref="ComponentMap{T}"/>; the mapping defined in said <see cref="ComponentMap{T}"/> will be merged
        /// with any options you specify from this call.
        /// </summary>
        /// <typeparam name="TComponent">Component type</typeparam>
        /// <param name="member">Property exposing the component</param>
        /// <returns>Component reference builder</returns>
        public ReferenceComponentPart<TComponent> Component<TComponent>(Expression<Func<T, TComponent>> member)
        {
            var componentStructure = new MemberStructure<ReferenceComponentMapping>(member.ToMember());
            var part = new ReferenceComponentPart<TComponent>(componentStructure);

            structure.AddChild(componentStructure);

            return part;
        }

        /// <summary>
        /// Maps a component
        /// </summary>
        /// <typeparam name="TComponent">Type of component</typeparam>
        /// <param name="expression">Component property</param>
        /// <param name="action">Component mapping</param>
        public ComponentPart<TComponent> Component<TComponent>(Expression<Func<T, TComponent>> expression, Action<ComponentPart<TComponent>> action)
        {
            return Component(expression.ToMember(), action);
        }

        /// <summary>
        /// Maps a component
        /// </summary>
        /// <typeparam name="TComponent">Type of component</typeparam>
        /// <param name="expression">Component property</param>
        /// <param name="action">Component mapping</param>
        public ComponentPart<TComponent> Component<TComponent>(Expression<Func<T, object>> expression, Action<ComponentPart<TComponent>> action)
        {
            return Component(expression.ToMember(), action);
        }
        
        protected virtual ComponentPart<TComponent> Component<TComponent>(Member property, Action<ComponentPart<TComponent>> action)
        {
            var componentStructure = new TypeStructure<ComponentMapping>(typeof(TComponent));
            var part = new ComponentPart<TComponent>(componentStructure);

            action(part);

            structure.AddChild(componentStructure);

            return part;
        }

        /// <summary>
        /// CreateProperties a one-to-many relationship
        /// </summary>
        /// <typeparam name="TChild">Child object type</typeparam>
        /// <typeparam name="TReturn">Property return type</typeparam>
        /// <param name="expression">Expression to get property from</param>
        /// <returns>one-to-many part</returns>
        private OneToManyPart<TChild> MapHasMany<TChild, TReturn>(Expression<Func<T, TReturn>> expression)
        {
            return HasMany<TChild>(expression.ToMember());
        }

        protected virtual OneToManyPart<TChild> HasMany<TChild>(Member member)
        {
            var collectionStructure = new MemberStructure<ICollectionMapping>(member);
            var part = new OneToManyPart<TChild>(collectionStructure);

            structure.AddChild(collectionStructure);

            return part;
        }

        /// <summary>
        /// CreateProperties a one-to-many relationship
        /// </summary>
        /// <typeparam name="TChild">Child object type</typeparam>
        /// <param name="expression">Expression to get property from</param>
        /// <returns>one-to-many part</returns>
        public OneToManyPart<TChild> HasMany<TChild>(Expression<Func<T, IEnumerable<TChild>>> expression)
        {
            return MapHasMany<TChild, IEnumerable<TChild>>(expression);
        }

        /// <summary>
        /// CreateProperties a one-to-many relationship with a IDictionary
        /// </summary>
        /// <typeparam name="TKey">Dictionary key type</typeparam>
        /// <typeparam name="TChild">Child object type / Dictionary value type</typeparam>
        /// <param name="expression">Expression to get property from</param>
        /// <returns>one-to-many part</returns>
        public OneToManyPart<TChild> HasMany<TKey, TChild>(Expression<Func<T, IDictionary<TKey, TChild>>> expression)
        {
            return MapHasMany<TChild, IDictionary<TKey, TChild>>(expression);
        }

        /// <summary>
        /// CreateProperties a one-to-many relationship
        /// </summary>
        /// <typeparam name="TChild">Child object type</typeparam>
        /// <param name="expression">Expression to get property from</param>
        /// <returns>one-to-many part</returns>
        public OneToManyPart<TChild> HasMany<TChild>(Expression<Func<T, object>> expression)
        {
            return MapHasMany<TChild, object>(expression);
        }

        /// <summary>
        /// CreateProperties a many-to-many relationship
        /// </summary>
        /// <typeparam name="TChild">Child object type</typeparam>
        /// <typeparam name="TReturn">Property return type</typeparam>
        /// <param name="expression">Expression to get property from</param>
        /// <returns>many-to-many part</returns>
        private ManyToManyPart<TChild> MapHasManyToMany<TChild, TReturn>(Expression<Func<T, TReturn>> expression)
        {
            return HasManyToMany<TChild>(expression.ToMember());
        }

        protected virtual ManyToManyPart<TChild> HasManyToMany<TChild>(Member property)
        {
            var collectionStructure = new MemberStructure<ICollectionMapping>(property);
            var part = new ManyToManyPart<TChild>(collectionStructure);

            structure.AddChild(collectionStructure);

            return part;
        }

        /// <summary>
        /// CreateProperties a many-to-many relationship
        /// </summary>
        /// <typeparam name="TChild">Child object type</typeparam>
        /// <param name="expression">Expression to get property from</param>
        /// <returns>many-to-many part</returns>
        public ManyToManyPart<TChild> HasManyToMany<TChild>(Expression<Func<T, IEnumerable<TChild>>> expression)
        {
            return MapHasManyToMany<TChild, IEnumerable<TChild>>(expression);
        }

	/// <summary>
        /// CreateProperties a many-to-many relationship with a IDictionary
        /// </summary>
        /// <typeparam name="TKey">Dictionary key type</typeparam>
        /// <typeparam name="TChild">Child object type / Dictionary value type</typeparam>
        /// <param name="expression">Expression to get property from</param>
        /// <returns>one-to-many part</returns>
/*	public ManyToManyPart<TChild> HasManyToMany<TKey, TChild>(Expression<Func<T, IDictionary<TKey, TChild>>> expression)
	{
		return MapHasManyToMany<TChild, IDictionary<TKey, TChild>>(expression);
	}*/

        /// <summary>
        /// CreateProperties a many-to-many relationship
        /// </summary>
        /// <typeparam name="TChild">Child object type</typeparam>
        /// <param name="expression">Expression to get property from</param>
        /// <returns>many-to-many part</returns>
        public ManyToManyPart<TChild> HasManyToMany<TChild>(Expression<Func<T, object>> expression)
        {
            return MapHasManyToMany<TChild, object>(expression);
        }

        public StoredProcedurePart SqlInsert(string innerText)
        {
            return StoredProcedure("sql-insert", innerText);
        }

        public StoredProcedurePart SqlUpdate(string innerText)
        {
            return StoredProcedure("sql-update", innerText);
        }     

        public StoredProcedurePart SqlDelete(string innerText)
        {
            return StoredProcedure("sql-delete", innerText);
        }

        public StoredProcedurePart SqlDeleteAll(string innerText)
        {
            return StoredProcedure("sql-delete-all", innerText);
        }

        protected StoredProcedurePart StoredProcedure(string element, string innerText)
        {
            var part = new StoredProcedurePart(element, innerText);
            //storedProcedures.Add(part);
            return part;
        }

        public Type EntityType
        {
            get { return typeof(T); }
        }
    }
}
