using System;
using System.Diagnostics;
using System.Linq.Expressions;
using FluentNHibernate.Mapping.Providers;
using FluentNHibernate.MappingModel;
using FluentNHibernate.Utils;

namespace FluentNHibernate.Mapping
{
    public class OneToOnePart<TOther> : IOneToOneMappingProvider
    {
        private readonly Type entity;
        private readonly Member property;
        private readonly AccessStrategyBuilder access;
        private readonly FetchBuilder fetch;
        private readonly CascadeBuilder cascade;
        private readonly AttributeStore attributes = new AttributeStore();
        private bool nextBool = true;

        public OneToOnePart(Type entity, Member property)
        {
            access = new AccessStrategyBuilder(value => attributes.Set(Attr.Access, value));
            fetch = new FetchBuilder(value => attributes.Set(Attr.Fetch, value));
            cascade = new CascadeBuilder(value => attributes.Set(Attr.Cascade, value));
            this.entity = entity;
            this.property = property;
        }

        OneToOneMapping IOneToOneMappingProvider.GetOneToOneMapping()
        {
            var mapping = new OneToOneMapping(attributes.Clone());

            mapping.ContainingEntityType = entity;

            if (!mapping.IsSpecified(Attr.Class))
                mapping.SetDefaultValue(Attr.Class, new TypeReference(typeof(TOther)));

            if (!mapping.IsSpecified(Attr.Name))
                mapping.SetDefaultValue(Attr.Name, property.Name);

            return mapping;
        }

        public OneToOnePart<TOther> Class<T>()
        {
            return Class(typeof(T));
        }

        public OneToOnePart<TOther> Class(Type type)
        {
            attributes.Set(Attr.Class, new TypeReference(type));
            return this;
        }

        public FetchBuilder<OneToOnePart<TOther>> Fetch
        {
            get { return new FetchBuilder<OneToOnePart<TOther>>(this, fetch); }
        }

        public OneToOnePart<TOther> ForeignKey()
        {
            return ForeignKey(string.Format("FK_{0}To{1}", property.DeclaringType.Name, property.Name));
        }

        public OneToOnePart<TOther> ForeignKey(string foreignKeyName)
        {
            attributes.Set(Attr.ForeignKey, foreignKeyName);
            return this;
        }

        public OneToOnePart<TOther> PropertyRef(Expression<Func<TOther, object>> expression)
        {
            var member = expression.ToMember();

            return PropertyRef(member.Name);
        }

        public OneToOnePart<TOther> PropertyRef(string propertyName)
        {
            attributes.Set(Attr.PropertyRef, propertyName);

            return this;
        }

        public OneToOnePart<TOther> Constrained()
        {
            attributes.Set(Attr.Constrained, nextBool);
            nextBool = true;

            return this;
        }

        public CascadeBuilder<OneToOnePart<TOther>> Cascade
        {
            get { return new CascadeBuilder<OneToOnePart<TOther>>(this, cascade); }
        }

        public AccessStrategyBuilder<OneToOnePart<TOther>> Access
        {
            get { return new AccessStrategyBuilder<OneToOnePart<TOther>>(this, access); }
        }

        public OneToOnePart<TOther> LazyLoad()
        {
            attributes.Set(Attr.Lazy, nextBool);
            nextBool = true;
            return this;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public OneToOnePart<TOther> Not
        {
            get
            {
                nextBool = !nextBool;
                return this;
            }
        }
    }
}
