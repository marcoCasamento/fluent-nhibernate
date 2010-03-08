using System;
using System.Collections.Generic;
using FluentNHibernate.Mapping;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel
{
    public class ParamMapping : MappingBase, IMapping
    {
        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override bool IsSpecified(string property)
        {
            throw new NotImplementedException();
        }

        public void AddChild(IMapping child)
        {
            throw new NotImplementedException();
        }

        public void UpdateValues(IEnumerable<KeyValuePair<Attr, object>> values)
        {
            throw new NotImplementedException();
        }
    }
}