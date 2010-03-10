using System;
using System.Collections.Generic;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.Utils;

namespace FluentNHibernate.Automapping
{
    public class AutoCollectionCreator
    {
        public CollectionMapping CreateCollectionMapping(Type type, Member member)
        {
            if (type.Namespace.StartsWith("Iesi.Collections") || type.Closes(typeof(HashSet<>)))
                return new CollectionMapping(member);

            return new CollectionMapping(member);
        }
    }
}