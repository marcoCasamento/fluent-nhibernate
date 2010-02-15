using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
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
        private readonly AttributeStore<OneToOneMapping> attributes = new AttributeStore<OneToOneMapping>();
        private bool nextBool = true;

        public OneToOnePart(Type entity, Member property)
        {
            access = new AccessStrategyBuilder(value => attributes.Set(x => x.Access, value));
            fetch = new FetchBuilder(value => attributes.Set(x => x.Fetch, value));
            cascade = new CascadeBuilder(value => attributes.Set(x => x.Cascade, value));
            this.entity = entity;
            this.property = property;
        }

        OneToOneMapping IOneToOneMappingProvider.GetOneToOneMapping()
        {
            var mapping = new OneToOneMapping(attributes.CloneInner());

            mapping.ContainingEntityType = entity;

            if (!mapping.IsSpecified("Class"))
                mapping.SetDefaultValue(x => x.Class, new TypeReference(typeof(TOther)));

            if (!mapping.IsSpecified("Name"))
                mapping.SetDefaultValue(x => x.Name, property.Name);

            return mapping;
        }

        public OneToOnePart<TOther> Class<T>()
        {
            return Class(typeof(T));
        }

        public OneToOnePart<TOther> Class(Type type)
        {
            attributes.Set(x => x.Class, new TypeReference(type));
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
            attributes.Set(x => x.ForeignKey, foreignKeyName);
            return this;
        }

        public OneToOnePart<TOther> PropertyRef(Expression<Func<TOther, object>> expression)
        {
            var member = expression.ToMember();

            return PropertyRef(member.Name);
        }

        public OneToOnePart<TOther> PropertyRef(string propertyName)
        {
            attributes.Set(x => x.PropertyRef, propertyName);

            return this;
        }

        public OneToOnePart<TOther> Constrained()
        {
            attributes.Set(x => x.Constrained, nextBool);
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
            attributes.Set(x => x.Lazy, nextBool);
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
