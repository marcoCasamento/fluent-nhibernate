using System;
using FluentNHibernate.Mapping.Providers;
using FluentNHibernate.MappingModel;

namespace FluentNHibernate.Mapping
{
    public class CachePart : ICacheMappingProvider
    {
        private readonly Type entityType;
        private readonly AttributeStore attributes = new AttributeStore();

        public CachePart(Type entityType)
        {
            this.entityType = entityType;
        }

        CacheMapping ICacheMappingProvider.GetCacheMapping()
        {
            var mapping = new CacheMapping(attributes.Clone());
            mapping.ContainedEntityType = entityType;

            return mapping;
        }

        public CachePart ReadWrite()
        {
            attributes.Set(Attr.Usage, "read-write");
            return this;
        }

        public CachePart NonStrictReadWrite()
        {
            attributes.Set(Attr.Usage, "nonstrict-read-write");
            return this;
        }

        public CachePart ReadOnly()
        {
            attributes.Set(Attr.Usage, "read-only");
            return this;
        }

        public CachePart Transactional()
        {
            attributes.Set(Attr.Usage, "transactional");
            return this;
        }

        public CachePart CustomUsage(string custom)
        {
            attributes.Set(Attr.Usage, custom);
            return this;
        }

        public CachePart Region(string name)
        {
            attributes.Set(Attr.Region, name);
            return this;
        }

        public CachePart IncludeAll()
        {
            attributes.Set(Attr.Include, "all");
            return this;
        }

        public CachePart IncludeNonLazy()
        {
            attributes.Set(Attr.Include, "non-lazy");
            return this;
        }

        public CachePart CustomInclude(string custom)
        {
            attributes.Set(Attr.Include, custom);
            return this;
        }

        public bool IsDirty
        {
            get { return attributes.HasUserValue(Attr.Region) || attributes.HasUserValue(Attr.Usage) || attributes.HasUserValue(Attr.Include); }
        }
    }
}