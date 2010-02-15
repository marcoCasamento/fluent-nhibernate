using System;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Identity;
namespace FluentNHibernate.Mapping
{
    public class KeyPropertyPart
    {
        private readonly KeyPropertyMapping mapping;
        private readonly AccessStrategyBuilder access;

        public KeyPropertyPart(KeyPropertyMapping mapping)
        {
            this.mapping = mapping;
            access = new AccessStrategyBuilder(value => mapping.Access = value);
        }

        public KeyPropertyPart ColumnName(string columnName)
        {
            mapping.AddColumn(new ColumnMapping { Name = columnName });
            return this;
        }

        public KeyPropertyPart Type(Type type)
        {
            mapping.Type = new TypeReference(type);
            return this;
        }

        public AccessStrategyBuilder<KeyPropertyPart> Access
        {
            get { return new AccessStrategyBuilder<KeyPropertyPart>(this, access); }
        }
    }
}