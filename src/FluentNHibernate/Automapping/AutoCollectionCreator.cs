using System;
using System.Collections.Generic;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.Utils;

namespace FluentNHibernate.Automapping
{
    public class AutoCollectionCreator
    {
        public ICollectionMapping CreateCollectionMapping(Type type, Member member)
        {
            if (type.Namespace.StartsWith("Iesi.Collections") || type.Closes(typeof(HashSet<>)))
                return new SetMapping(member);

            return new BagMapping(member);
        }
    }
}