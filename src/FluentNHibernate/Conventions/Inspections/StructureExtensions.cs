using System;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Structure;

namespace FluentNHibernate.Conventions.Inspections
{
    internal static class StructureExtensions
    {
        public static IEnumerable<IMappingStructure<T>> ChildrenOf<T>(this IMappingStructure structure)
            where T : IMapping
        {
            return structure.Children
                .Where(x => x is IMappingStructure<T>)
                .Cast<IMappingStructure<T>>();
        }

        public static string GetValue(this IMappingStructure structure, Attr attr)
        {
            return structure.GetValue<string>(attr);
        }

        public static Type ContainingEntityType(this IMappingStructure structure)
        {
            var parent = structure.Parent;

            while (parent != null)
            {
                if (parent is ITypeMappingStructure)
                    return ((ITypeMappingStructure)parent).Type;
            }

            return null;
        }

        public static bool HasValue(this IMappingStructure structure, Member member, IInspectorMapper mapper)
        {
            return structure.Values.Any(x => x.Key == mapper.Get(member));
        }
    }
}