using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.MappingModel.Structure;

namespace FluentNHibernate.Conventions.Inspections
{
    public class IndexInspector : IIndexInspector
    {
        private readonly InspectorMapper<IIndexInspector, IndexMapping> mappedProperties = new InspectorMapper<IIndexInspector, IndexMapping>();
        private readonly IndexMapping mapping;

        public IndexInspector(IndexMapping mapping)
        {
            this.mapping = mapping;
        }

        public Type EntityType
        {
            get { return mapping.ContainingEntityType; }
        }

        public string StringIdentifierForModel
        {
            get { return mapping.Type.Name; }
        }

        public bool IsSet(Member property)
        {
            return mapping.IsSpecified(mappedProperties.Get(property));
        }

        public TypeReference Type
        {
            get { return mapping.Type; }
        }

        public IEnumerable<IColumnInspector> Columns
        {
            get
            {
                return mapping.Columns
                    .Select(x => new ColumnInspector((IMappingStructure<ColumnMapping>)x))
                    .Cast<IColumnInspector>();
            }
        }
    }
}
