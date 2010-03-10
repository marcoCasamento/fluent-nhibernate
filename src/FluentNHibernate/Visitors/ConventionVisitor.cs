﻿using System;
using System.Collections;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Instances;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.MappingModel.Identity;

namespace FluentNHibernate.Visitors
{
    public class ConventionVisitor : DefaultMappingModelVisitor
    {
        private readonly IConventionFinder finder;

        private Type currentType;

        public ConventionVisitor(IConventionFinder finder)
        {
            this.finder = finder;
        }

        public override void ProcessHibernateMapping(HibernateMapping hibernateMapping)
        {
            var conventions = finder.Find<IHibernateMappingConvention>();

            Apply<IHibernateMappingInspector, IHibernateMappingInstance>(conventions,
                new HibernateMappingInstance(hibernateMapping));
        }

        public override void ProcessId(IdMapping idMapping)
        {
            var conventions = finder.Find<IIdConvention>();

            Apply<IIdentityInspector, IIdentityInstance>(conventions,
                new IdentityInstance(idMapping));
        }

        public override void ProcessCompositeId(CompositeIdMapping idMapping)
        {
            var conventions = finder.Find<ICompositeIdentityConvention>();

            Apply<ICompositeIdentityInspector, ICompositeIdentityInstance>(conventions,
                new CompositeIdentityInstance(idMapping));
        }

        public override void ProcessClass(ClassMapping classMapping)
        {
            var conventions = finder.Find<IClassConvention>();

            currentType = classMapping.Type;

            Apply<IClassInspector, IClassInstance>(conventions,
                new ClassInstance(classMapping));
        }

        public override void ProcessProperty(PropertyMapping propertyMapping)
        {
            var conventions = finder.Find<IPropertyConvention>();

            Apply<IPropertyInspector, IPropertyInstance>(conventions,
                new PropertyInstance(propertyMapping));
        }

        public override void ProcessColumn(ColumnMapping columnMapping)
        {
            var conventions = finder.Find<IColumnConvention>();

            Apply<IColumnInspector, IColumnInstance>(conventions,
                new ColumnInstance(currentType, columnMapping));
        }

        public override void ProcessManyToOne(ManyToOneMapping mapping)
        {
            var conventions = finder.Find<IReferenceConvention>();

            Apply<IManyToOneInspector, IManyToOneInstance>(conventions,
                new ManyToOneInstance(mapping));
        }

        public override void ProcessVersion(VersionMapping mapping)
        {
            var conventions = finder.Find<IVersionConvention>();

            Apply<IVersionInspector, IVersionInstance>(conventions,
                new VersionInstance(mapping));
        }

        public override void ProcessOneToOne(OneToOneMapping mapping)
        {
            var conventions = finder.Find<IHasOneConvention>();

            Apply<IOneToOneInspector, IOneToOneInstance>(conventions,
                new OneToOneInstance(mapping));
        }

        public override void ProcessSubclass(SubclassMapping subclassMapping)
        {
            if (subclassMapping.SubclassType == SubclassType.Subclass)
            {
                var conventions = finder.Find<ISubclassConvention>();

                Apply<ISubclassInspector, ISubclassInstance>(conventions,
                    new SubclassInstance(subclassMapping));
            }
            else
            {
                var conventions = finder.Find<IJoinedSubclassConvention>();

                Apply<IJoinedSubclassInspector, IJoinedSubclassInstance>(conventions,
                    new JoinedSubclassInstance(subclassMapping));
            }
        }

        public override void ProcessComponent(ComponentMapping mapping)
        {
            if (mapping.ComponentType == ComponentType.Component)
            {
                var conventions = finder.Find<IComponentConvention>();

                Apply<IComponentInspector, IComponentInstance>(conventions,
                    new ComponentInstance((ComponentMapping)mapping));
            }
            else
            {
                var conventions = finder.Find<IDynamicComponentConvention>();

                Apply<IDynamicComponentInspector, IDynamicComponentInstance>(conventions,
                    new DynamicComponentInstance((ComponentMapping)mapping));
            }
        }

        public override void ProcessIndex(IndexMapping indexMapping)
        {
            var conventions = finder.Find<IIndexConvention>();

            Apply<IIndexInspector, IIndexInstance>(conventions,
                new IndexInstance(indexMapping));
        }

        public override void ProcessIndex(IndexManyToManyMapping indexMapping)
        {
            var conventions = finder.Find<IIndexManyToManyConvention>();

            Apply<IIndexManyToManyInspector, IIndexManyToManyInstance>(conventions,
                new IndexManyToManyInstance(indexMapping));
        }

        public override void ProcessCollection(CollectionMapping mapping)
        {
            var generalConventions = finder.Find<ICollectionConvention>();

            Apply<ICollectionInspector, ICollectionInstance>(generalConventions,
                new CollectionInstance(mapping));

            if (mapping.Relationship is ManyToManyMapping)
            {
                var conventions = finder.Find<IHasManyToManyConvention>();

                Apply<IManyToManyCollectionInspector, IManyToManyCollectionInstance>(conventions,
                    new ManyToManyCollectionInstance(mapping));
            }
            else
            {
                var conventions = finder.Find<IHasManyConvention>();

                Apply<IOneToManyCollectionInspector, IOneToManyCollectionInstance>(conventions,
                    new OneToManyCollectionInstance(mapping));
            }
        }

        //public override void ProcessArray(CollectionMapping mapping)
        //{
        //    var conventions = finder.Find<IArrayConvention>();

        //    Apply<IArrayInspector, IArrayInstance>(conventions,
        //        new ArrayInstance(mapping));

        //    ApplyCollectionConventions(mapping);
        //}

        //public override void ProcessBag(CollectionMapping collectionMapping)
        //{
        //    var conventions = finder.Find<IBagConvention>();

        //    Apply<IBagInspector, IBagInstance>(conventions,
        //        new BagInstance(collectionMapping));

        //    ApplyCollectionConventions(collectionMapping);
        //}

        //public override void ProcessList(CollectionMapping collectionMapping)
        //{
        //    var conventions = finder.Find<IListConvention>();

        //    Apply<IListInspector, IListInstance>(conventions,
        //        new ListInstance(collectionMapping));

        //    ApplyCollectionConventions(collectionMapping);
        //}

        //public override void ProcessMap(CollectionMapping mapping)
        //{
        //    var conventions = finder.Find<IMapConvention>();

        //    Apply<IMapInspector, IMapInstance>(conventions,
        //        new MapInstance(mapping));

        //    ApplyCollectionConventions(mapping);
        //}

        //public override void ProcessSet(CollectionMapping collectionMapping)
        //{
        //    var conventions = finder.Find<ISetConvention>();

        //    Apply<ISetInspector, ISetInstance>(conventions,
        //        new SetInstance(collectionMapping));

        //    ApplyCollectionConventions(collectionMapping);
        //}

        public override void ProcessJoin(JoinMapping joinMapping)
        {
            var conventions = finder.Find<IJoinConvention>();

            Apply<IJoinInspector, IJoinInstance>(conventions,
                new JoinInstance(joinMapping));
        }

        public override void ProcessKeyProperty(KeyPropertyMapping mapping)
        {
            var conventions = finder.Find<IKeyPropertyConvention>();

            Apply<IKeyPropertyInspector, IKeyPropertyInstance>(conventions, 
                new KeyPropertyInstance(mapping));
        }

        public override void ProcessKeyManyToOne(KeyManyToOneMapping mapping)
        {
            var conventions = finder.Find<IKeyManyToOneConvention>();

            Apply<IKeyManyToOneInspector, IKeyManyToOneInstance>(conventions, 
                new KeyManyToOneInstance(mapping));
        }

        public override void ProcessAny(AnyMapping mapping)
        {
            var conventions = finder.Find<IAnyConvention>();

            Apply<IAnyInspector, IAnyInstance>(conventions,
                new AnyInstance(mapping));
        }

        private void Apply<TInspector, TInstance>(IEnumerable conventions, TInstance instance)
            where TInspector : IInspector
            where TInstance : TInspector
        {
            foreach (IConvention<TInspector, TInstance> convention in conventions)
            {
                var criteria = new ConcreteAcceptanceCriteria<TInspector>();
                var acceptance = convention as IConventionAcceptance<TInspector>;

                if (acceptance != null)
                    acceptance.Accept(criteria);

                if (criteria.Matches(instance))
                    convention.Apply(instance);
            }
        }

        private void ApplyCollectionConventions(CollectionMapping mapping)
        {
            if (mapping.Relationship is ManyToManyMapping)
                Apply<IManyToManyCollectionInspector, IManyToManyCollectionInstance>(finder.Find<IHasManyToManyConvention>(),
                    new ManyToManyCollectionInstance(mapping));

            if (mapping.Relationship is OneToManyMapping)
                Apply<IOneToManyCollectionInspector, IOneToManyCollectionInstance>(finder.Find<IHasManyConvention>(),
                    new OneToManyCollectionInstance(mapping));

            Apply<ICollectionInspector, ICollectionInstance>(finder.Find<ICollectionConvention>(),
                new CollectionInstance(mapping));
        }
    }
}