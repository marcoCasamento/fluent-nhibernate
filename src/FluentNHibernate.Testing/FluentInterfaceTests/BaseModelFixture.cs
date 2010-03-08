using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using FluentNHibernate.Automapping.TestFixtures;
using FluentNHibernate.Mapping;
using FluentNHibernate.Mapping.Providers;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.MappingModel.Identity;
using FluentNHibernate.Testing.DomainModel;
using FluentNHibernate.Testing.DomainModel.Mapping;
using FluentNHibernate.Utils;
using FluentNHibernate.Utils.Reflection;

namespace FluentNHibernate.Testing.FluentInterfaceTests
{
    public abstract class BaseModelFixture
    {
        protected static ModelTester<ClassMap<T>, ClassMapping> ClassMap<T>()
        {
            return new ModelTester<ClassMap<T>, ClassMapping>(
                () => new ClassMap<T>(),
                x => (ClassMapping)((IMappingProvider)x).GetUserDefinedMappings().Structure);
        }

        protected static ModelTester<DiscriminatorPart, DiscriminatorMapping> DiscriminatorMap<T>()
        {
            var structure = new BucketStructure<DiscriminatorMapping>();
            return new ModelTester<DiscriminatorPart, DiscriminatorMapping>(
                () => new DiscriminatorPart(structure),
                structure.CreateMappingNode);
        }

        protected static ModelTester<SubClassPart<T>, SubclassMapping> Subclass<T>()
        {
            var structure = new TypeStructure<SubclassMapping>(typeof(T));
            return new ModelTester<SubClassPart<T>, SubclassMapping>(
                () => new SubClassPart<T>(null, structure),
                structure.CreateMappingNode);
        }

        protected static ModelTester<SubclassMap<T>, SubclassMapping> SubclassMapForSubclass<T>()
        {
            return new ModelTester<SubclassMap<T>, SubclassMapping>(() => new SubclassMap<T>(), x =>
            {
                var userMappings = ((IIndeterminateSubclassMappingProvider)x).GetUserDefinedMappings();
                var mapping = (SubclassMapping)userMappings.Structure;
                mapping.SubclassType = SubclassType.Subclass;
                return mapping;
            });
        }

        protected static ModelTester<JoinedSubClassPart<T>, SubclassMapping> JoinedSubclass<T>()
        {
            var structure = new TypeStructure<SubclassMapping>(typeof(T));
            return new ModelTester<JoinedSubClassPart<T>, SubclassMapping>(
                () => new JoinedSubClassPart<T>(structure),
                structure.CreateMappingNode);
        }

        protected static ModelTester<SubclassMap<T>, SubclassMapping> SubclassMapForJoinedSubclass<T>()
        {
            return new ModelTester<SubclassMap<T>, SubclassMapping>(() => new SubclassMap<T>(), x =>
            {
                var userMappings = ((IIndeterminateSubclassMappingProvider)x).GetUserDefinedMappings();
                var mapping = (SubclassMapping)userMappings.Structure;
                mapping.SubclassType = SubclassType.JoinedSubclass;
                return mapping;
            });
        }

        protected static ModelTester<ComponentPart<T>, ComponentMapping> Component<T>()
        {
            var structure = new TypeStructure<ComponentMapping>(typeof(T));
            return new ModelTester<ComponentPart<T>, ComponentMapping>(
                () => new ComponentPart<T>(structure),
                structure.CreateMappingNode);
        }

        protected static ModelTester<DynamicComponentPart<T>, ComponentMapping> DynamicComponent<T>()
        {
            var structure = new TypeStructure<ComponentMapping>(typeof(T));
            return new ModelTester<DynamicComponentPart<T>, ComponentMapping>(
                () => new DynamicComponentPart<T>(structure),
                structure.CreateMappingNode);
        }

        protected static ModelTester<VersionPart, VersionMapping> Version()
        {
            var structure = new MemberStructure<VersionMapping>(ReflectionHelper.GetMember<VersionTarget>(x => x.VersionNumber));
            return new ModelTester<VersionPart, VersionMapping>(
                () => new VersionPart(structure),
                structure.CreateMappingNode);
        }

        protected static ModelTester<CachePart, CacheMapping> Cache()
        {
            var structure = new BucketStructure<CacheMapping>();
            return new ModelTester<CachePart, CacheMapping>(
                () => new CachePart(structure),
                structure.CreateMappingNode);
        }

        protected static ModelTester<IdentityPart<int>, IdMapping> Id()
        {
            var structure = new MemberStructure<IdMapping>(ReflectionHelper.GetMember<IdentityTarget>(x => x.IntId));
            return new ModelTester<IdentityPart<int>, IdMapping>(
                () => new IdentityPart<int>(structure),
                structure.CreateMappingNode);
        }

        protected static ModelTester<CompositeIdentityPart<T>, CompositeIdMapping> CompositeId<T>()
        {
            var structure = new BucketStructure<CompositeIdMapping>();
            return new ModelTester<CompositeIdentityPart<T>, CompositeIdMapping>(
                () => new CompositeIdentityPart<T>(structure),
                structure.CreateMappingNode);
        }

        protected static ModelTester<OneToOnePart<PropertyReferenceTarget>, OneToOneMapping> OneToOne()
        {
            var structure = new MemberStructure<OneToOneMapping>(ReflectionHelper.GetMember<PropertyTarget>(x => x.Reference));
            return new ModelTester<OneToOnePart<PropertyReferenceTarget>, OneToOneMapping>(
                () => new OneToOnePart<PropertyReferenceTarget>(structure),
                structure.CreateMappingNode);
        }

        protected static ModelTester<PropertyPart, PropertyMapping> Property()
        {
            var structure = new MemberStructure<PropertyMapping>(ReflectionHelper.GetMember<PropertyTarget>(x => x.Name));
            return new ModelTester<PropertyPart, PropertyMapping>(
                () => new PropertyPart(structure),
                structure.CreateMappingNode);
        }

        protected static ModelTester<PropertyPart, PropertyMapping> Property<T>(Expression<Func<T, object>> property)
        {
            var structure = new MemberStructure<PropertyMapping>(property.ToMember());
            return new ModelTester<PropertyPart, PropertyMapping>(
                () => new PropertyPart(structure),
                structure.CreateMappingNode);
        }

        protected static ModelTester<OneToManyPart<T>, ICollectionMapping> OneToMany<T>(Expression<Func<OneToManyTarget, IEnumerable<T>>> property)
        {
            var structure = new MemberStructure<ICollectionMapping>(property.ToMember());
            return new ModelTester<OneToManyPart<T>, ICollectionMapping>(
                () => new OneToManyPart<T>(structure),
                structure.CreateMappingNode);
        }

        protected static ModelTester<ManyToManyPart<T>, ICollectionMapping> ManyToMany<T>(Expression<Func<ManyToManyTarget, IList<T>>> property)
        {
            var structure = new MemberStructure<ICollectionMapping>(ReflectionHelper.GetMember(property));
            return new ModelTester<ManyToManyPart<T>, ICollectionMapping>(
                () => new ManyToManyPart<T>(structure),
                structure.CreateMappingNode);
        }

        protected static ModelTester<ManyToManyPart<IDictionary>, ICollectionMapping> ManyToMany(Expression<Func<ManyToManyTarget, IDictionary>> property)
        {
            var structure = new MemberStructure<ICollectionMapping>(ReflectionHelper.GetMember(property));
            return new ModelTester<ManyToManyPart<IDictionary>, ICollectionMapping>(
                () => new ManyToManyPart<IDictionary>(structure),
                structure.CreateMappingNode);
        }

        protected static ModelTester<ManyToManyPart<IDictionary<TIndex, TValue>>, ICollectionMapping> ManyToMany<TIndex, TValue>(Expression<Func<ManyToManyTarget, IDictionary<TIndex, TValue>>> property)
        {
            var structure = new MemberStructure<ICollectionMapping>(ReflectionHelper.GetMember(property));
            return new ModelTester<ManyToManyPart<IDictionary<TIndex, TValue>>, ICollectionMapping>(
               () => new ManyToManyPart<IDictionary<TIndex, TValue>>(structure),
               structure.CreateMappingNode);
        }

        protected static ModelTester<ManyToOnePart<PropertyReferenceTarget>, ManyToOneMapping> ManyToOne()
        {
            var structure = new MemberStructure<ManyToOneMapping>(ReflectionHelper.GetMember<PropertyTarget>(x => x.Reference));
            return new ModelTester<ManyToOnePart<PropertyReferenceTarget>, ManyToOneMapping>(
                () => new ManyToOnePart<PropertyReferenceTarget>(structure),
                structure.CreateMappingNode);
        }

        protected static ModelTester<AnyPart<T>, AnyMapping> Any<T>()
        {
            var structure = new MemberStructure<AnyMapping>(ReflectionHelper.GetMember<MappedObject>(x => x.Parent));
            return new ModelTester<AnyPart<T>, AnyMapping>(
                () => new AnyPart<T>(structure),
                structure.CreateMappingNode);
        }

        protected static ModelTester<JoinPart<T>, JoinMapping> Join<T>(string table)
        {
            var structure = new BucketStructure<JoinMapping>();
            return new ModelTester<JoinPart<T>, JoinMapping>(
                () => new JoinPart<T>(structure),
                structure.CreateMappingNode);
        }

        protected static ModelTester<HibernateMappingPart, HibernateMapping> HibernateMapping()
        {
            return new ModelTester<HibernateMappingPart, HibernateMapping>(() => new HibernateMappingPart(), x => ((IHibernateMappingProvider)x).GetHibernateMapping());
        }

        protected static ModelTester<CompositeElementPart<T>, CompositeElementMapping> CompositeElement<T>()
        {
            var structure = new BucketStructure<CompositeElementMapping>();
            return new ModelTester<CompositeElementPart<T>, CompositeElementMapping>(
                () => new CompositeElementPart<T>(structure),
                structure.CreateMappingNode);
        }

        protected static ModelTester<StoredProcedurePart, StoredProcedureMapping> StoredProcedure()
        {
            return null;
            //return new ModelTester<StoredProcedurePart, StoredProcedureMapping>(
            //    () => new StoredProcedurePart(null, null),
            //    x => x.GetStoredProcedureMapping());
        }

        protected static ModelTester<NaturalIdPart<T>, NaturalIdMapping> NaturalId<T>()
        {
            var structure = new BucketStructure<NaturalIdMapping>();
            return new ModelTester<NaturalIdPart<T>, NaturalIdMapping>(
                () => new NaturalIdPart<T>(structure), 
                structure.CreateMappingNode);
        }
    }
}