using System;
using System.Xml;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.Utils;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.Output
{
    public class XmlCollectionWriter : NullMappingModelVisitor, IXmlWriter<CollectionMapping>
    {
        private readonly IXmlWriterServiceLocator serviceLocator;
        private XmlDocument document;

        public XmlCollectionWriter(IXmlWriterServiceLocator serviceLocator)
        {
            this.serviceLocator = serviceLocator;
        }

        public XmlDocument Write(CollectionMapping mappingModel)
        {
            document = null;
            mappingModel.AcceptVisitor(this);
            return document;
        }

        public override void ProcessCollection(CollectionMapping mapping)
        {
            var writer = serviceLocator.GetWriter<CollectionMapping>();
            document = writer.Write(mapping);
        }

        public override void Visit(FilterMapping filterMapping)
        {
            var writer = serviceLocator.GetWriter<FilterMapping>();
            var xml = writer.Write(filterMapping);
            document.ImportAndAppendChild(xml);
        }
    }
}