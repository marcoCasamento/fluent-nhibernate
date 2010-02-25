using System;
using System.Linq.Expressions;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel
{
    public class TuplizerMapping : MappingBase
    {
        public TuplizerMapping()
            : this(null)
        {}

        public TuplizerMapping(AttributeStore underlyingStore)
        {
            if (underlyingStore != null)
                ReplaceAttributes(underlyingStore);
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessTuplizer(this);
        }

        public TuplizerMode Mode
        {
            get { return (TuplizerMode)GetAttribute(Attr.Mode); }
            set { SetAttribute(Attr.Mode, value); }
        }

        public TypeReference Type
        {
            get { return (TypeReference)GetAttribute(Attr.Type); }
            set { SetAttribute(Attr.Type, value); }
        }
    }

    public enum TuplizerMode
    {
        Poco,
        Xml,
        DynamicMap
    }
}