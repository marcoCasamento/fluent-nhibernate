using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.Utils;

namespace FluentNHibernate.Automapping
{
    public class AutoEntityCollection : IAutoMapper
    {
        readonly AutoMappingExpressions expressions;
        readonly AutoKeyMapper keys;
        AutoCollectionCreator collections;

        public AutoEntityCollection(AutoMappingExpressions expressions)
        {
            this.expressions = expressions;
            keys = new AutoKeyMapper(expressions);
            collections = new AutoCollectionCreator();
        }

        public bool MapsProperty(Member property)
        {
            return property.CanWrite &&
                property.PropertyType.Namespace.In("System.Collections.Generic", "Iesi.Collections.Generic");
        }

        public void Map(ClassMappingBase classMap, Member property)
        {
            if (property.DeclaringType != classMap.Type)
                return;

            var mapping = collections.CreateCollectionMapping(property.PropertyType, property);

            mapping.ContainingEntityType = classMap.Type;
            mapping.Member = property;

            SetRelationship(property, classMap, mapping);
            keys.SetKey(property, classMap, mapping);

            classMap.AddCollection(mapping);  
        }

        private void SetRelationship(Member property, ClassMappingBase classMap, CollectionMapping mapping)
        {
            var relationship = new OneToManyMapping(property.PropertyType.GetGenericArguments()[0])
            {
                ContainingEntityType = classMap.Type
            };

            mapping.Relationship = relationship;
        }
    }
}