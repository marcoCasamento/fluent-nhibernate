using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using FluentNHibernate.Mapping.Providers;
using FluentNHibernate.MappingModel;
using FluentNHibernate.Utils;

namespace FluentNHibernate.Mapping
{
    public class ManyToOnePart<TOther> : IManyToOneMappingProvider
    {
        private readonly AccessStrategyBuilder access;
        private readonly FetchBuilder fetch;
        private readonly NotFoundExpression<ManyToOnePart<TOther>> notFound;
        private readonly CascadeBuilder cascade;
        private readonly IList<string> columns = new List<string>();
        private bool nextBool = true;
        private readonly AttributeStore attributes = new AttributeStore();
        private readonly AttributeStore columnAttributes = new AttributeStore();
        private readonly Type entity;
        private readonly Member property;

        public ManyToOnePart(Type entity, Member property) 
        {
            this.entity = entity;
            this.property = property;
            access = new AccessStrategyBuilder(value => attributes.Set(Attr.Access, value));
            fetch = new FetchBuilder(value => attributes.Set(Attr.Fetch, value));
            cascade = new CascadeBuilder(value => attributes.Set(Attr.Cascade, value));
            notFound = new NotFoundExpression<ManyToOnePart<TOther>>(this, value => attributes.Set(Attr.NotFound, value));
        }

        ManyToOneMapping IManyToOneMappingProvider.GetManyToOneMapping()
        {
            var mapping = new ManyToOneMapping(attributes.Clone());

            mapping.ContainingEntityType = entity;
            mapping.SetMember(property);
            mapping.Class = typeof(TOther).ToReference();

            if (columns.Count == 0)
                mapping.AddDefaultColumn(CreateColumn(property.Name + "_id"));

            foreach (var column in columns)
            {
                var columnMapping = CreateColumn(column);

                mapping.AddColumn(columnMapping);
            }

            return mapping;
        }

        private ColumnMapping CreateColumn(string column)
        {
            return new ColumnMapping(columnAttributes.Clone()) { Name = column };
        }

        public FetchBuilder<ManyToOnePart<TOther>> Fetch
		{
			get { return new FetchBuilder<ManyToOnePart<TOther>>(this, fetch); }
		}

        public NotFoundExpression<ManyToOnePart<TOther>> NotFound
        {
            get { return notFound; }
        }

        public ManyToOnePart<TOther> Unique()
        {
            columnAttributes.Set(Attr.Unique, nextBool);
            nextBool = true;
            return this;
        }

        /// <summary>
        /// Specifies the name of a multi-column unique constraint.
        /// </summary>
        /// <param name="keyName">Name of constraint</param>
        public ManyToOnePart<TOther> UniqueKey(string keyName)
        {
            columnAttributes.Set(Attr.UniqueKey, keyName);
            return this;
        }

        public ManyToOnePart<TOther> Index(string indexName)
        {
            columnAttributes.Set(Attr.Index, indexName);
            return this;
        }

        public ManyToOnePart<TOther> Class<T>()
        {
	        return Class(typeof(T));
        }

        public ManyToOnePart<TOther> Class(Type type)
        {
            attributes.Set(Attr.Class, new TypeReference(type));
            return this;
        }

        public ManyToOnePart<TOther> ReadOnly()
        {
            attributes.Set(Attr.Insert, !nextBool);
            attributes.Set(Attr.Update, !nextBool);
            nextBool = true;
            return this;
        }

        public ManyToOnePart<TOther> LazyLoad()
        {
            attributes.Set(Attr.Lazy, nextBool);
            nextBool = true;
            return this;
        }
		
		public ManyToOnePart<TOther> ForeignKey()
		{
			return ForeignKey(string.Format("FK_{0}To{1}", property.DeclaringType.Name, property.Name));
		}
		
		public ManyToOnePart<TOther> ForeignKey(string foreignKeyName)
		{
		    attributes.Set(Attr.ForeignKey, foreignKeyName);
			return this;
		}

        public ManyToOnePart<TOther> Insert()
        {
            attributes.Set(Attr.Insert, nextBool);
            nextBool = true;
            return this;
        }

        public ManyToOnePart<TOther> Update()
        {
            attributes.Set(Attr.Update, nextBool);
            nextBool = true;
            return this;
        }

        public ManyToOnePart<TOther> Columns(params string[] columns)
        {
            foreach (var column in columns)
            {
                this.columns.Add(column);
            }

            return this;
        }

        public ManyToOnePart<TOther> Columns(params Expression<Func<TOther, object>>[] columns)
        {
            foreach (var expression in columns)
            {
                var member = expression.ToMember();

                Columns(member.Name);
            }

            return this;
        }

        public CascadeBuilder<ManyToOnePart<TOther>> Cascade
		{
			get { return new CascadeBuilder<ManyToOnePart<TOther>>(this, cascade); }
		}

        public ManyToOnePart<TOther> Column(string name)
        {
            columns.Clear();
            columns.Add(name);

            return this;
        }

        public ManyToOnePart<TOther> PropertyRef(Expression<Func<TOther, object>> expression)
        {
            var member = expression.ToMember();

            return PropertyRef(member.Name);
        }

        public ManyToOnePart<TOther> PropertyRef(string property)
        {
            attributes.Set(Attr.PropertyRef, property);
            return this;
        }

        public ManyToOnePart<TOther> Nullable()
        {
            columnAttributes.Set(Attr.NotNull, !nextBool);
            nextBool = true;
            return this;
        }

        public AccessStrategyBuilder<ManyToOnePart<TOther>> Access
        {
            get { return new AccessStrategyBuilder<ManyToOnePart<TOther>>(this, access); }
        }

        /// <summary>
        /// Inverts the next boolean
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public ManyToOnePart<TOther> Not
        {
            get
            {
                nextBool = !nextBool;
                return this;
            }
        }
    }
}
