using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentNHibernate.Conventions;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.Utils;

namespace FluentNHibernate.Automapping
{
    public class AutoMapper
    {
        private readonly List<IAutoMapper> mappingRules;
        private List<AutoMapType> mappingTypes;
        private readonly AutoMappingExpressions expressions;
        private readonly IEnumerable<InlineOverride> inlineOverrides;

        public AutoMapper(AutoMappingExpressions expressions, IConventionFinder conventionFinder, IEnumerable<InlineOverride> inlineOverrides)
        {
            this.expressions = expressions;
            this.inlineOverrides = inlineOverrides;

            mappingRules = new List<IAutoMapper>
            {
                new AutoMapIdentity(expressions), 
                new AutoMapVersion(), 
                new AutoMapComponent(expressions, this),
                new AutoMapProperty(conventionFinder, expressions),
                new AutoMapManyToMany(expressions),
                new AutoMapManyToOne(),
                new AutoMapOneToMany(expressions),
            };
        }

        private void ApplyOverrides(Type classType, IList<string> mappedProperties, ClassMappingBase mapping)
        {
            var autoMapType = typeof(AutoMapping<>).MakeGenericType(classType);
            var autoMap = Activator.CreateInstance(autoMapType, mappedProperties);

            inlineOverrides
                .Where(x => x.CanOverride(classType))
                .Each(x => x.Apply(autoMap));

            ((IAutoClasslike)autoMap).AlterModel(mapping);
        }

        public ClassMappingBase MergeMap(Type classType, ClassMappingBase mapping, IList<string> mappedProperties)
        {
            // map class first, then subclasses - this way subclasses can inspect the class model
            // to see which properties have already been mapped
            ApplyOverrides(classType, mappedProperties, mapping);

            MapEverythingInClass(mapping, classType, mappedProperties);

            if (mappingTypes != null)
                MapInheritanceTree(classType, mapping, mappedProperties);

            return mapping;
        }

        private void MapInheritanceTree(Type classType, ClassMappingBase mapping, IList<string> mappedProperties)
        {
            var discriminatorSet = false;
            var isDiscriminated = expressions.IsDiscriminated(classType);

            foreach (var inheritedClass in mappingTypes.Where(q =>
                q.Type.BaseType == classType &&
                    !expressions.IsConcreteBaseType(q.Type.BaseType)))
            {
                if (isDiscriminated && !discriminatorSet && mapping is ClassMapping)
                {
                    var discriminatorColumn = expressions.DiscriminatorColumn(classType);
                    var discriminator = new DiscriminatorMapping
                    {
                        ContainingEntityType = classType,
                        Type = new TypeReference(typeof(string))
                    };
                    discriminator.AddDefaultColumn(new ColumnMapping() { Name = discriminatorColumn });

                    ((ClassMapping)mapping).Discriminator = discriminator;
                    discriminatorSet = true;
                }

                SubclassMapping subclassMapping;
                var subclassStrategy = expressions.SubclassStrategy(classType);

                if (subclassStrategy == SubclassStrategy.JoinedSubclass)
                {
                    subclassMapping = new SubclassMapping(classType);
                    subclassMapping.Key = new KeyMapping(classType);
                    subclassMapping.Key.AddDefaultColumn(new ColumnMapping() { Name = mapping.Type.Name + "_id" });
                }
                else
                    subclassMapping = new SubclassMapping(classType);

				// track separate set of properties for each sub-tree within inheritance hierarchy
            	var subClassProperties = new List<string>(mappedProperties);
				MapSubclass(subClassProperties, subclassMapping, inheritedClass);

                mapping.AddSubclass(subclassMapping);

				MergeMap(inheritedClass.Type, (ClassMappingBase)subclassMapping, subClassProperties);
            }
        }

        private void MapSubclass(IList<string> mappedProperties, SubclassMapping subclass, AutoMapType inheritedClass)
        {
            subclass.Name = inheritedClass.Type.AssemblyQualifiedName;
            subclass.Type = inheritedClass.Type;
            ApplyOverrides(inheritedClass.Type, mappedProperties, subclass);
            MapEverythingInClass(subclass, inheritedClass.Type, mappedProperties);
            inheritedClass.IsMapped = true;
        }

        public virtual void MapEverythingInClass(ClassMappingBase mapping, Type entityType, IList<string> mappedProperties)
        {
            foreach (var property in entityType.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                TryToMapProperty(mapping, property.ToMember(), mappedProperties);
            }
        }

        protected void TryToMapProperty(ClassMappingBase mapping, Member property, IList<string> mappedProperties)
        {
            if (!property.HasIndexParameters)
            {
                foreach (var rule in mappingRules)
                {
                    if (rule.MapsProperty(property))
                    {
                        if (!mappedProperties.Any(name => name == property.Name))
                        {
                            rule.Map(mapping, property);
                            mappedProperties.Add(property.Name);

                            break;
                        }
                    }
                }
            }
        }

        public ClassMapping Map(Type classType, List<AutoMapType> types)
        {
            var classMap = new ClassMapping(classType);

            mappingTypes = types;
            return (ClassMapping)MergeMap(classType, classMap, new List<string>());
        }

        /// <summary>
        /// Flags a type as already mapped, stop it from being auto-mapped.
        /// </summary>
        public void FlagAsMapped(Type type)
        {
            mappingTypes
                .Where(x => x.Type == type)
                .Each(x => x.IsMapped = true);
        }
    }
}
