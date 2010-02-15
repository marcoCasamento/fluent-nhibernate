using System;
using System.Linq;
using FluentNHibernate.Automapping.TestFixtures;
using FluentNHibernate.Conventions.Helpers.Builders;
using FluentNHibernate.Conventions.Instances;
using FluentNHibernate.Mapping;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.Testing.FluentInterfaceTests;
using FluentNHibernate.Testing.Utils;
using NUnit.Framework;

namespace FluentNHibernate.Testing.ConventionsTests.OverridingFluentInterface
{
    [TestFixture]
    public class ArrayConventionTests
    {
        private PersistenceModel model;
        private IMappingProvider mapping;
        private Type mappingType;

        [SetUp]
        public void CreatePersistenceModel()
        {
            model = new PersistenceModel();
        }

        [Test]
        public void AccessShouldntBeOverwritten()
        {
            Mapping(x => x.Access.Field());

            Convention(x => x.Access.Property());

            VerifyModel(x => x.Access.ShouldEqual("field"));
        }

        [Test]
        public void BatchSizeShouldntBeOverwritten()
        {
            //Mapping(x => x.BatchSize(10));

            //Convention(x => x.BatchSize(100));

            //VerifyModel(x => x.BatchSize.ShouldEqual(10));
            Assert.Fail();
        }

        [Test]
        public void CacheShouldntBeOverwritten()
        {
            //Mapping(x => x.Cache.CustomUsage("fish"));

            //Convention(x => x.Cache.ReadOnly());

            //VerifyModel(x => x.Cache.Usage.ShouldEqual("fish"));
            Assert.Fail();
        }

        [Test]
        public void CascadeShouldntBeOverwritten()
        {
            //Mapping(x => x.Cascade.All());

            //Convention(x => x.Cascade.None());

            //VerifyModel(x => x.Cascade.ShouldEqual("all"));
            Assert.Fail();
        }

        [Test]
        public void CheckShouldntBeOverwritten()
        {
            //Mapping(x => x.Check("constraint"));

            //Convention(x => x.Check("xxx"));

            //VerifyModel(x => x.Check.ShouldEqual("constraint"));
            Assert.Fail();
        }

        [Test]
        public void CollectionTypeShouldntBeOverwritten()
        {
            //Mapping(x => x.CollectionType<int>());

            //Convention(x => x.CollectionType<string>());

            //VerifyModel(x => x.CollectionType.GetUnderlyingSystemType().ShouldEqual(typeof(int)));
            Assert.Fail();
        }

        [Test]
        public void FetchShouldntBeOverwritten()
        {
            //Mapping(x => x.Fetch.Join());

            //Convention(x => x.Fetch.Select());

            //VerifyModel(x => x.Fetch.ShouldEqual("join"));
            Assert.Fail();
        }

        [Test]
        public void GenericShouldntBeOverwritten()
        {
            //Mapping(x => x.Generic());

            //Convention(x => x.Not.Generic());

            //VerifyModel(x => x.Generic.ShouldEqual(true));
            Assert.Fail();
        }

        [Test]
        public void InverseShouldntBeOverwritten()
        {
            //Mapping(x => x.Inverse());

            //Convention(x => x.Not.Inverse());

            //VerifyModel(x => x.Inverse.ShouldEqual(true));
            Assert.Fail();
        }

        [Test]
        public void LazyShouldntBeOverwritten()
        {
            //Mapping(x => x.LazyLoad());

            //Convention(x => x.Not.LazyLoad());

            //VerifyModel(x => x.Lazy.ShouldEqual(true));
            Assert.Fail();
        }

        [Test]
        public void MutableShouldntBeOverwritten()
        {
            //Mapping(x => x.ReadOnly());

            //Convention(x => x.Not.ReadOnly());

            //VerifyModel(x => x.Mutable.ShouldEqual(false));
            Assert.Fail();
        }

        [Test]
        public void OptimisticLockShouldntBeOverwritten()
        {
            //Mapping(x => x.OptimisticLock.All());

            //Convention(x => x.OptimisticLock.Dirty());

            //VerifyModel(x => x.OptimisticLock.ShouldEqual("all"));
            Assert.Fail();
        }

        [Test]
        public void PersisterShouldntBeOverwritten()
        {
            //Mapping(x => x.Persister<CustomPersister>());

            //Convention(x => x.Persister<SecondCustomPersister>());

            //VerifyModel(x => x.Persister.GetUnderlyingSystemType().ShouldEqual(typeof(CustomPersister)));
            Assert.Fail();
        }

        [Test]
        public void SchemaShouldntBeOverwritten()
        {
            //Mapping(x => x.Schema("dbo"));

            //Convention(x => x.Schema("xxx"));

            //VerifyModel(x => x.Schema.ShouldEqual("dbo"));
            Assert.Fail();
        }

        [Test]
        public void SubselectShouldntBeOverwritten()
        {
            //Mapping(x => x.Subselect("whee"));

            //Convention(x => x.Subselect("woo"));

            //VerifyModel(x => x.Subselect.ShouldEqual("whee"));
            Assert.Fail();
        }

        [Test]
        public void TableNameShouldntBeOverwritten()
        {
            //Mapping(x => x.Table("name"));

            //Convention(x => x.Table("xxx"));

            //VerifyModel(x => x.TableName.ShouldEqual("name"));
            Assert.Fail();
        }

        [Test]
        public void WhereShouldntBeOverwritten()
        {
            //Mapping(x => x.Where("x = 1"));

            //Convention(x => x.Where("y = 2"));

            //VerifyModel(x => x.Where.ShouldEqual("x = 1"));
            Assert.Fail();
        }

        #region Helpers

        private void Convention(Action<IArrayInstance> convention)
        {
            model.Conventions.Add(new ArrayConventionBuilder().Always(convention));
        }

        private void Mapping(Action<HasManyArrayBuilder<ExampleClass>> mappingDefinition)
        {
            var classMap = new ClassMap<ExampleParentClass>();
            classMap.Id(x => x.Id);
            var map = classMap.HasMany(x => x.ExampleArray);

            mappingDefinition(map);

            mapping = classMap;
            mappingType = typeof(ExampleParentClass);
        }

        private void VerifyModel(Action<ArrayMapping> modelVerification)
        {
            model.Add(mapping);

            var generatedModels = model.BuildMappings();
            var modelInstance = generatedModels
                .First(x => x.Classes.FirstOrDefault(c => c.Type == mappingType) != null)
                .Classes.First()
                .Collections.First();

            modelVerification((ArrayMapping)modelInstance);
        }

        #endregion

    }
}