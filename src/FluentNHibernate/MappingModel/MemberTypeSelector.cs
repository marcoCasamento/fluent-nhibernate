using System;
using System.Collections.Generic;
using FluentNHibernate.Mapping;
using FluentNHibernate.Utils;

namespace FluentNHibernate.MappingModel
{
    public class MemberTypeSelector
    {
        public TypeReference GetItemType(Member member)
        {
            if (member.PropertyType.ClosesInterface(typeof(IEnumerable<>)))
                return member.PropertyType.GetGenericArguments()[0].ToReference();

            throw new InvalidOperationException("Cannot get item type of a non-collection member.");
        }

        public TypeReference GetType(Member member)
        {
            var type = new TypeReference(member.PropertyType);

            if (member.PropertyType.IsEnum())
                type = new TypeReference(typeof(GenericEnumMapper<>).MakeGenericType(member.PropertyType));

            if (member.PropertyType.IsNullable() && member.PropertyType.IsEnum())
                type = new TypeReference(typeof(GenericEnumMapper<>).MakeGenericType(member.PropertyType.GetGenericArguments()[0]));

            return type;
        }
    }
}