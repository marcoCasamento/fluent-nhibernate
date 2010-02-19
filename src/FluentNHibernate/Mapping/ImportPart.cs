using System;
using System.Xml;
using FluentNHibernate.MappingModel;

namespace FluentNHibernate.Mapping
{
    public class ImportPart
    {
        private readonly AttributeStore attributes = new AttributeStore();

        public ImportPart(Type importType)
        {
            attributes.SetDefault(Attr.Class, new TypeReference(importType));
        }

        public void As(string alternativeName)
        {
            attributes.Set(Attr.Rename, alternativeName);
        }

        public ImportMapping GetImportMapping()
        {
            return new ImportMapping(attributes.Clone());
        }
    }
}