using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.MappingModel;

namespace FluentNHibernate.Testing
{
    public static class ModelTestExtensions
    {
        public static bool IsSpecified<T>(this T model, Attr property)
            where T : IMappingBase
        {
            return model.IsSpecified(property);
        }
    }
}