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
using FluentNHibernate.MappingModel.Structure;
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
            var map = new ClassMap<T>();
            return new ModelTester<ClassMap<T>, ClassMapping>(
                () => map,
                () => (ClassMapping)((IMappingProvider)map).GetUserDefinedMappings().Structure.CreateMappingNode());
        }

        protected static ModelTester<DiscriminatorPart, DiscriminatorMapping> DiscriminatorMap<T>()
        {
            var structure = Structures.Discriminator(typeof(T));
            var parentStructure = Structures.Class(typeof(T));
            return new ModelTester<DiscriminatorPart, DiscriminatorMapping>(
                () => new DiscriminatorPart(structure, parentStructure),
                structure.CreateMappingNode);
        }

        protected static ModelTester<SubClassPart<T>, SubclassMapping> Subclass<T>()
        {
            var structure = Structures.Subclass(SubclassType.JoinedSubclass, typeof(T));
            return new ModelTester<SubClassPart<T>, SubclassMapping>(
                () => new SubClassPart<T>(null, structure),
                structure.CreateMappingNode);
        }

        protected static ModelTester<SubclassMap<T>, SubclassMapping> SubclassMapForSubclass<T>()
        {
            var map = new SubclassMap<T>();
            return new ModelTester<SubclassMap<T>, SubclassMapping>(() => map, () =>
            {
                var userMappings = ((IIndeterminateSubclassMappingProvider)map).GetUserDefinedMappings();
                var mapping = (SubclassMapping)userMappings.Structure.CreateMappingNode();
                mapping.SubclassType = SubclassType.Subclass;
                return mapping;
            });
        }

        protected static ModelTester<JoinedSubClassPart<T>, SubclassMapping> JoinedSubclass<T>()
        {
            var structure = Structures.Subclass(SubclassType.JoinedSubclass, typeof(T));
            return new ModelTester<JoinedSubClassPart<T>, SubclassMapping>(
                () => new JoinedSubClassPart<T>(structure),
                structure.CreateMappingNode);
        }

        protected static ModelTester<SubclassMap<T>, SubclassMapping> SubclassMapForJoinedSubclass<T>()
        {
            var map = new SubclassMap<T>();
            return new ModelTester<SubclassMap<T>, SubclassMapping>(() => map, () =>
            {
                var userMappings = ((IIndeterminateSubclassMappingProvider)map).GetUserDefinedMappings();
                var mapping = (SubclassMapping)userMappings.Structure.CreateMappingNode();
                mapping.SubclassType = SubclassType.JoinedSubclass;
                return mapping;
            });
        }

        protected static ModelTester<ComponentPart<T>, ComponentMapping> Component<T>()
        {
            var structure = Structures.Component(ComponentType.Component, ReflectionHelper.GetMember<ExampleClass>(x => x.Parent), typeof(ExampleClass));
            return new ModelTester<ComponentPart<T>, ComponentMapping>(
                () => new ComponentPart<T>(structure),
                structure.CreateMappingNode);
        }

        protected static ModelTester<DynamicComponentPart<T>, ComponentMapping> DynamicComponent<T>()
        {
            var structure = Structures.Component(ComponentType.DynamicComponent, ReflectionHelper.GetMember<ExampleClass>(x => x.Dictionary), typeof(ExampleClass));
            return new ModelTester<DynamicComponentPart<T>, ComponentMapping>(
                () => new DynamicComponentPart<T>(structure),
                structure.CreateMappingNode);
        }

        protected static ModelTester<VersionPart, VersionMapping> Version()
        {
            var structure = Structures.Version(ReflectionHelper.GetMember<VersionTarget>(x => x.VersionNumber));
            return new ModelTester<VersionPart, VersionMapping>(
                () => new VersionPart(structure),
                structure.CreateMappingNode);
        }

        protected static ModelTester<CachePart, CacheMapping> Cache()
        {
            var structure = Structures.Cache();
            return new ModelTester<CachePart, CacheMapping>(
                () => new CachePart(structure),
                structure.CreateMappingNode);
        }

        protected static ModelTester<IdentityPart<int>, IdMapping> Id()
        {
            var structure = Structures.Id(ReflectionHelper.GetMember<IdentityTarget>(x => x.IntId));
            return new ModelTester<IdentityPart<int>, IdMapping>(
                () => new IdentityPart<int>(structure),
                structure.CreateMappingNode);
        }

        protected static ModelTester<CompositeIdentityPart<T>, CompositeIdMapping> CompositeId<T>()
        {
            var structure = Structures.CompositeId(ReflectionHelper.GetMember<IdentityTarget>(x => x.IntId));
            return new ModelTester<CompositeIdentityPart<T>, CompositeIdMapping>(
                () => new CompositeIdentityPart<T>(structure),
                structure.CreateMappingNode);
        }

        protected static ModelTester<OneToOnePart<PropertyReferenceTarget>, OneToOneMapping> OneToOne()
        {
            var structure = Structures.OneToOne(ReflectionHelper.GetMember<PropertyTarget>(x => x.Reference));
            return new ModelTester<OneToOnePart<PropertyReferenceTarget>, OneToOneMapping>(
                () => new OneToOnePart<PropertyReferenceTarget>(structure),
                structure.CreateMappingNode);
        }

        protected static ModelTester<PropertyPart, PropertyMapping> Property()
        {
            var structure = Structures.Property(ReflectionHelper.GetMember<PropertyTarget>(x => x.Name));
            return new ModelTester<PropertyPart, PropertyMapping>(
                () => new PropertyPart(structure),
                structure.CreateMappingNode);
        }

        protected static ModelTester<PropertyPart, PropertyMapping> Property<T>(Expression<Func<T, object>> property)
        {
            var structure = Structures.Property(ReflectionHelper.GetMember<PropertyTarget>(x => x.Name));
            return new ModelTester<PropertyPart, PropertyMapping>(
                () => new PropertyPart(structure),
                structure.CreateMappingNode);
        }

        protected static ModelTester<OneToManyPart<T>, CollectionMapping> OneToMany<T>(Expression<Func<OneToManyTarget, IEnumerable<T>>> property)
        {
            var structure = Structures.Collection(typeof(OneToManyTarget), property.ToMember());
            var key = Structures.Key(typeof(OneToManyTarget));
            var relationship = Structures.OneToMany(typeof(T));
            return new ModelTester<OneToManyPart<T>, CollectionMapping>(
                () => new OneToManyPart<T>(typeof(T), structure, key, relationship),
                structure.CreateMappingNode);
        }

        protected static ModelTester<ManyToManyPart<T>, CollectionMapping> ManyToMany<T>(Expression<Func<ManyToManyTarget, IList<T>>> property)
        {
            var structure = Structures.Collection(typeof(ManyToManyTarget), property.ToMember());
            var key = Structures.Key(typeof(OneToManyTarget));
            var relationship = Structures.ManyToMany(typeof(T));
            return new ModelTester<ManyToManyPart<T>, CollectionMapping>(
                () => new ManyToManyPart<T>(typeof(T), structure, key, relationship),
                structure.CreateMappingNode);
        }

        protected static ModelTester<ManyToManyPart<IDictionary>, CollectionMapping> ManyToMany(Expression<Func<ManyToManyTarget, IDictionary>> property)
        {
            var structure = Structures.Collection(typeof(ManyToManyTarget), property.ToMember());
            var key = Structures.Key(typeof(OneToManyTarget));
            var relationship = Structures.ManyToMany(typeof(ManyToManyTarget));
            return new ModelTester<ManyToManyPart<IDictionary>, CollectionMapping>(
                () => new ManyToManyPart<IDictionary>(typeof(ManyToManyTarget), structure, key, relationship),
                structure.CreateMappingNode);
        }

        protected static ModelTester<ManyToManyPart<IDictionary<TIndex, TValue>>, CollectionMapping> ManyToMany<TIndex, TValue>(Expression<Func<ManyToManyTarget, IDictionary<TIndex, TValue>>> property)
        {
            var structure = Structures.Collection(typeof(ManyToManyTarget), property.ToMember());
            var key = Structures.Key(typeof(OneToManyTarget));
            var relationship = Structures.ManyToMany(typeof(ManyToManyTarget));
            return new ModelTester<ManyToManyPart<IDictionary<TIndex, TValue>>, CollectionMapping>(
               () => new ManyToManyPart<IDictionary<TIndex, TValue>>(typeof(ManyToManyTarget), structure, key, relationship),
               structure.CreateMappingNode);
        }

        protected static ModelTester<ManyToOnePart<PropertyReferenceTarget>, ManyToOneMapping> ManyToOne()
        {
            var structure = Structures.ManyToOne(ReflectionHelper.GetMember<PropertyTarget>(x => x.Reference));
            return new ModelTester<ManyToOnePart<PropertyReferenceTarget>, ManyToOneMapping>(
                () => new ManyToOnePart<PropertyReferenceTarget>(structure),
                structure.CreateMappingNode);
        }

        protected static ModelTester<AnyPart<T>, AnyMapping> Any<T>()
        {
            var structure = Structures.Any(ReflectionHelper.GetMember<PropertyTarget>(x => x.Reference));
            return new ModelTester<AnyPart<T>, AnyMapping>(
                () => new AnyPart<T>(structure),
                structure.CreateMappingNode);
        }

        protected static ModelTester<JoinPart<T>, JoinMapping> Join<T>()
        {
            var structure = Structures.Join();
            return new ModelTester<JoinPart<T>, JoinMapping>(
                () => new JoinPart<T>(structure),
                structure.CreateMappingNode);
        }

        protected static ModelTester<HibernateMappingPart, HibernateMapping> HibernateMapping()
        {
            var structure = Structures.Container();
            var map = new HibernateMappingPart(structure);
            return new ModelTester<HibernateMappingPart, HibernateMapping>(
                () => map,
                () => ((IHibernateMappingProvider)map).GetHibernateMapping());
        }

        protected static ModelTester<CompositeElementPart<T>, CompositeElementMapping> CompositeElement<T>()
        {
            var structure = Structures.CompositeElement(typeof(T));
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
            var structure = Structures.NaturalId();
            return new ModelTester<NaturalIdPart<T>, NaturalIdMapping>(
                () => new NaturalIdPart<T>(structure), 
                structure.CreateMappingNode);
        }
    }
}