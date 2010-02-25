using System;
using System.Linq.Expressions;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel
{
    public class ImportMapping : MappingBase
    {
        public ImportMapping()
            : this(null)
        {}

        public ImportMapping(AttributeStore underlyingStore)
        {
            if (underlyingStore != null)
                ReplaceAttributes(underlyingStore);
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessImport(this);
        }

        public string Rename
        {
            get { return (string)GetAttribute(Attr.Rename); }
            set { SetAttribute(Attr.Rename, value); }
        }

        public TypeReference Class
        {
            get { return (TypeReference)GetAttribute(Attr.Class); }
            set { SetAttribute(Attr.Class, value); }
        }
    }
}