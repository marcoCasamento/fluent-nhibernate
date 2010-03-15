using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.MappingModel.Structure;

namespace FluentNHibernate.Conventions.Inspections
{
    public class IndexManyToManyInspector : IIndexManyToManyInspector
    {
        private readonly InspectorMapper<IIndexManyToManyInspector, IndexManyToManyMapping> mappedProperties = new InspectorMapper<IIndexManyToManyInspector, IndexManyToManyMapping>();
        private readonly IndexManyToManyMapping mapping;

        public IndexManyToManyInspector(IndexManyToManyMapping mapping)
        {
            this.mapping = mapping;
        }

        public Type EntityType
        {
            get { return mapping.ContainingEntityType; }
        }

        public string StringIdentifierForModel
        {
            get { return mapping.Class.Name; }
        }

        public bool IsSet(Member property)
        {
            return mapping.IsSpecified(mappedProperties.Get(property));
        }
        
        public TypeReference Class
        {
            get { return mapping.Class; }
        }
        
        public string ForeignKey
        {
            get { return mapping.ForeignKey; }
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
