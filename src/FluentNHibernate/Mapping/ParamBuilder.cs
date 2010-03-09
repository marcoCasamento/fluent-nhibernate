using System;
using System.Collections.Generic;
using FluentNHibernate.MappingModel;

namespace FluentNHibernate.Mapping
{
    public class ParamBuilder
    {
        readonly IMappingStructure structure;

        public ParamBuilder(IMappingStructure structure)
        {
            this.structure = structure;
        }

        public ParamBuilder AddParam(string name, string value)
        {
            var paramStructure = new BucketStructure<ParamMapping>();

            paramStructure.SetValue(Attr.Name, name);
            paramStructure.SetValue(Attr.Value, value);

            structure.AddChild(paramStructure);

            return this;
        }
    }
}