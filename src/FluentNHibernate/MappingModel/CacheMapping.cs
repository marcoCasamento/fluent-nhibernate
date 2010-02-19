using System;
using System.Linq.Expressions;
using System.Reflection;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel
{
    public class CacheMapping : MappingBase
    {
        public CacheMapping()
            : this(null)
        {}

        public CacheMapping(AttributeStore underlyingStore)
        {
            if (underlyingStore != null)
                ReplaceAttributes(underlyingStore);
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessCache(this);
        }

        public string Region
        {
            get { return (string)GetAttribute(Attr.Region); }
            set { SetAttribute(Attr.Region, value); }
        }

        public string Usage
        {
            get { return (string)GetAttribute(Attr.Usage); }
            set { SetAttribute(Attr.Usage, value); }
        }

        public string Include
        {
            get { return (string)GetAttribute(Attr.Include); }
            set { SetAttribute(Attr.Include, value); }
        }

        public Type ContainedEntityType { get; set; }

        public bool Equals(CacheMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && Equals(other.ContainedEntityType, ContainedEntityType);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(CacheMapping)) return false;
            return Equals((CacheMapping)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return base.GetHashCode() ^ (ContainedEntityType != null ? ContainedEntityType.GetHashCode() : 0);
            }
        }
    }
}