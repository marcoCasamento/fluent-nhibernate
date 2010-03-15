using System;
using System.Reflection;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Structure;

namespace FluentNHibernate.Conventions.Inspections
{
    public class ColumnInspector : IColumnInspector
    {
        private readonly IMappingStructure<ColumnMapping> structure;
        private readonly InspectorMapper<IColumnInspector> mappings = new InspectorMapper<IColumnInspector>();

        public ColumnInspector(IMappingStructure<ColumnMapping> structure)
        {
            //EntityType = containingEntityType;
            this.structure = structure;
        }

        public ColumnInspector(Type containingEntityType, ColumnMapping columnMapping)
        {
            throw new NotImplementedException();
        }

        public Type EntityType
        {
            get { return structure.ContainingEntityType(); }
        }

        public string Name
        {
            get { return structure.GetValue(Attr.Name); }
        }

        public string Check
        {
            get { return structure.GetValue(Attr.Check); }
        }

        public string Index
        {
            get { return structure.GetValue(Attr.Index); }
        }

        public int Length
        {
            get { return structure.GetValue<int>(Attr.Length); }
        }

        public bool NotNull
        {
            get { return structure.GetValue<bool>(Attr.NotNull); }
        }

        public string SqlType
        {
            get { return structure.GetValue(Attr.SqlType); }
        }

        public bool Unique
        {
            get { return structure.GetValue<bool>(Attr.Unique); }
        }

        public string UniqueKey
        {
            get { return structure.GetValue(Attr.UniqueKey); }
        }

        public int Precision
        {
            get { return structure.GetValue<int>(Attr.Precision); }
        }

        public int Scale
        {
            get { return structure.GetValue<int>(Attr.Scale); }
        }

        public string Default
        {
            get { return structure.GetValue(Attr.Default); }
        }

        public string StringIdentifierForModel
        {
            get { return Name; }
        }

        public bool IsSet(Member property)
        {
            return structure.HasValue(property, mappings);
        }
    }
}