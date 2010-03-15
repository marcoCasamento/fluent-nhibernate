using System;
using System.Reflection;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Structure;

namespace FluentNHibernate.Conventions.Inspections
{
    public class DiscriminatorInspector : ColumnBasedInspector, IDiscriminatorInspector
    {
        private readonly InspectorMapper<IDiscriminatorInspector, DiscriminatorMapping> propertyMappings = new InspectorMapper<IDiscriminatorInspector, DiscriminatorMapping>();
        private readonly DiscriminatorMapping mapping;

        public DiscriminatorInspector(DiscriminatorMapping mapping)
            : base(mapping.Columns)
        {
            this.mapping = mapping;
            propertyMappings.Map(x => x.Nullable, "NotNull");
        }

        public bool Insert
        {
            get { return mapping.Insert; }
        }

        public bool Force
        {
            get { return mapping.Force; }
        }

        public string Formula
        {
            get { return mapping.Formula; }
        }

        public TypeReference Type
        {
            get { return mapping.Type; }
        }

        public Type EntityType
        {
            get { return mapping.ContainingEntityType; }
        }

        public string StringIdentifierForModel
        {
            get { return mapping.Type.Name; }
        }

        public IDefaultableEnumerable<IColumnInspector> Columns
        {
            get
            {
                var items = new DefaultableList<IColumnInspector>();

                foreach (var column in mapping.Columns.UserDefined)
                    items.Add(new ColumnInspector((IMappingStructure<ColumnMapping>)column));

                return items;
            }
        }

        public bool IsSet(Member property)
        {
            return mapping.IsSpecified(propertyMappings.Get(property));
        }
    }
}