using System;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Structure;

namespace FluentNHibernate.Conventions.Inspections
{
    public class MetaValueInspector : IMetaValueInspector
    {
        private readonly InspectorMapper<IMetaValueInspector> mappings = new InspectorMapper<IMetaValueInspector>();
        private readonly IMappingStructure<MetaValueMapping> structure;

        public MetaValueInspector(IMappingStructure<MetaValueMapping> structure)
        {
            this.structure = structure;
        }

        public Type EntityType
        {
            get { return structure.ContainingEntityType(); }
        }

        public string StringIdentifierForModel
        {
            get { return Class.Name; }
        }

        public bool IsSet(Member property)
        {
            return structure.HasValue(property, mappings);
        }

        public TypeReference Class
        {
            get { return structure.GetValue<TypeReference>(Attr.Class); }
        }

        public string Value
        {
            get { return structure.GetValue(Attr.Value); }
        }
    }
}