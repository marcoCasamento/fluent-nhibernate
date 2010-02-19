using FluentNHibernate.Mapping.Providers;
using FluentNHibernate.MappingModel;

namespace FluentNHibernate.Mapping
{
    public class StoredProcedurePart : IStoredProcedureMappingProvider
    {
        private readonly CheckTypeExpression<StoredProcedurePart> check;
        private readonly string _element;
        private readonly string _innerText;
        private readonly AttributeStore attributes = new AttributeStore();


        public StoredProcedurePart(string element, string innerText)
        {
            _element = element;
            _innerText = innerText;

            check = new CheckTypeExpression<StoredProcedurePart>(this, value => attributes.Set(Attr.Check, value));
        }


        public CheckTypeExpression<StoredProcedurePart> Check
        {
            get { return check; }
        }


        public StoredProcedureMapping GetStoredProcedureMapping()
        {
            var mapping = new StoredProcedureMapping(attributes.Clone());

            mapping.SPType = _element;
            mapping.Query = _innerText;

            if (!mapping.IsSpecified(Attr.Check))
                mapping.SetDefaultValue(Attr.Check, "rowcount");

            return mapping;
        }
    }
}
