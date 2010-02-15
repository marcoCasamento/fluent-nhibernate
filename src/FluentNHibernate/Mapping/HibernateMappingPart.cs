using System.Diagnostics;
using FluentNHibernate.Mapping.Providers;
using FluentNHibernate.MappingModel;

namespace FluentNHibernate.Mapping
{
    public class HibernateMappingPart : IHibernateMappingProvider
    {
        private readonly CascadeBuilder defaultCascade;
        private readonly AccessStrategyBuilder defaultAccess;
        private readonly AttributeStore<HibernateMapping> attributes = new AttributeStore<HibernateMapping>();
        private bool nextBool = true;

        public HibernateMappingPart()
        {
            defaultCascade = new CascadeBuilder(value => attributes.Set(x => x.DefaultCascade, value));
            defaultAccess = new AccessStrategyBuilder(value => attributes.Set(x => x.DefaultAccess, value));
        }

        public HibernateMappingPart Schema(string schema)
        {
            attributes.Set(x => x.Schema, schema);
            return this;
        }

        public CascadeBuilder<HibernateMappingPart> DefaultCascade
        {
            get { return new CascadeBuilder<HibernateMappingPart>(this, defaultCascade); }
        }

        public AccessStrategyBuilder<HibernateMappingPart> DefaultAccess
        {
            get { return new AccessStrategyBuilder<HibernateMappingPart>(this, defaultAccess); }
        }

        public HibernateMappingPart AutoImport()
        {
            attributes.Set(x => x.AutoImport, nextBool);
            nextBool = true;
            return this;
        }

        public HibernateMappingPart DefaultLazy()
        {
            attributes.Set(x => x.DefaultLazy, nextBool);
            nextBool = true;
            return this;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public HibernateMappingPart Not
        {
            get
            {
                nextBool = !nextBool;
                return this;
            }
        }

        public HibernateMappingPart Catalog(string catalog)
        {
            attributes.Set(x => x.Catalog, catalog);
            return this;
        }

        public HibernateMappingPart Namespace(string ns)
        {
            attributes.Set(x => x.Namespace, ns);
            return this;
        }

        public HibernateMappingPart Assembly(string assembly)
        {
            attributes.Set(x => x.Assembly, assembly);
            return this;
        }

        HibernateMapping IHibernateMappingProvider.GetHibernateMapping()
        {
            return new HibernateMapping(attributes.CloneInner());
        }
    }
}