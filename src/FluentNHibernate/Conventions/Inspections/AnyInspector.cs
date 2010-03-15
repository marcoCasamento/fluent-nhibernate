using System;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Structure;

namespace FluentNHibernate.Conventions.Inspections
{
    public class AnyInspector : IAnyInspector
    {
        readonly InspectorMapper<IAnyInspector> mappings = new InspectorMapper<IAnyInspector>();
        readonly IMappingStructure<AnyMapping> structure;

        public AnyInspector(IMappingStructure<AnyMapping> structure)
        {
            this.structure = structure;
            mappings.Map(x => x.LazyLoad, Attr.Lazy);
        }

        public Type EntityType
        {
            get { return structure.ContainingEntityType(); }
        }

        public string StringIdentifierForModel
        {
            get { return Name; }
        }

        public bool IsSet(Member property)
        {
            return structure.HasValue(property, mappings);
        }

        public Access Access
        {
            get { return Access.FromString(structure.GetValue(Attr.Access)); }
        }

        public Cascade Cascade
        {
            get { return Cascade.FromString(structure.GetValue(Attr.Cascade)); }
        }

        public IEnumerable<IColumnInspector> Columns
        {
            get
            {
                return structure.ChildrenOf<ColumnMapping>()
                    .Select(x => new ColumnInspector(x))
                    .Cast<IColumnInspector>()
                    .ToList();
            }
        }

        public string IdType
        {
            get { return structure.GetValue(Attr.IdType); }
        }

        public bool Insert
        {
            get { return structure.GetValue<bool>(Attr.Insert); }
        }

        public TypeReference MetaType
        {
            get { return structure.GetValue<TypeReference>(Attr.MetaType); }
        }

        public IEnumerable<IMetaValueInspector> MetaValues
        {
            get
            {
                return structure.ChildrenOf<MetaValueMapping>()
                    .Select(x => new MetaValueInspector(x))
                    .Cast<IMetaValueInspector>();
            }
        }

        public string Name
        {
            get { return structure.GetValue(Attr.Name); }
        }

        public bool Update
        {
            get { return structure.GetValue<bool>(Attr.Update); }
        }

        public bool LazyLoad
        {
            get { return structure.GetValue<bool>(Attr.Lazy); }
        }

        public bool OptimisticLock
        {
            get { return structure.GetValue<bool>(Attr.OptimisticLock); }
        }
    }
}