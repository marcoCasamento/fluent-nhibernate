using FluentNHibernate.Automapping.TestFixtures;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.MappingModel.Output;
using FluentNHibernate.Testing.DomainModel;
using FluentNHibernate.Testing.Testing;
using NUnit.Framework;

namespace FluentNHibernate.Testing.MappingModel.Output
{
    [TestFixture]
    public class XmlComponentWriterTester
    {
        private IXmlWriter<IComponentMapping> writer;

        private XmlWriterTestHelper<IComponentMapping> create_helper()
        {
            var helper = new XmlWriterTestHelper<IComponentMapping>();
            helper.CreateInstance(() => new ComponentMapping(typeof(ExampleClass)));
            return helper;
        }

        [SetUp]
        public void GetWriterFromContainer()
        {
            var container = new XmlWriterContainer();
            writer = container.Resolve<IXmlWriter<IComponentMapping>>();
        }

        [Test]
        public void ShouldWriteNameAttribute()
        {
            var testHelper = create_helper();
            
            testHelper.Check(x => x.Name, "name").MapsToAttribute("name");
            testHelper.VerifyAll(writer);
        }

        [Test]
        public void ShouldWriteAccessAttribute()
        {
            var testHelper = create_helper();

            testHelper.Check(x => x.Access, "acc").MapsToAttribute("access");
            testHelper.VerifyAll(writer);
        }

        [Test]
        public void ShouldWriteClassAttribute()
        {
            var testHelper = create_helper();

            testHelper.Check(x => x.Class, new TypeReference("class")).MapsToAttribute("class");
            testHelper.VerifyAll(writer);
        }

        [Test]
        public void ShouldWriteUpdateAttribute()
        {
            var testHelper = create_helper();

            testHelper.Check(x => x.Update, true).MapsToAttribute("update");
            testHelper.VerifyAll(writer);
        }

        [Test]
        public void ShouldWriteInsertAttribute()
        {
            var testHelper = create_helper();

            testHelper.Check(x => x.Insert, true).MapsToAttribute("insert");
            testHelper.VerifyAll(writer);
        }

        [Test]
        public void ShouldWriteLazyAttribute()
        {
            var testHelper = create_helper();

            testHelper.Check(x => x.Lazy, true).MapsToAttribute("lazy");
            testHelper.VerifyAll(writer);
        }

        [Test]
        public void ShouldWriteOptimisticLockAttribute()
        {
            var testHelper = create_helper();

            testHelper.Check(x => x.OptimisticLock, true).MapsToAttribute("optimistic-lock");
            testHelper.VerifyAll(writer);
        }

        [Test]
        public void ShouldWriteComponents()
        {
            var mapping = new ComponentMapping(typeof(ExampleClass));

            mapping.AddComponent(new ComponentMapping(typeof(ExampleClass)));

            writer.VerifyXml(mapping)
                .Element("component").Exists();
        }

        [Test]
        public void ShouldWriteDynamicComponents()
        {
            var mapping = new ComponentMapping(typeof(ExampleClass));

            mapping.AddComponent(new ComponentMapping(typeof(ExampleClass)));

            writer.VerifyXml(mapping)
                .Element("dynamic-component").Exists();
        }

        [Test]
        public void ShouldWriteProperties()
        {
            var mapping = new ComponentMapping(typeof(ExampleClass));

            mapping.AddProperty(new PropertyMapping(null));

            writer.VerifyXml(mapping)
                .Element("property").Exists();
        }

        [Test]
        public void ShouldWriteManyToOnes()
        {
            var mapping = new ComponentMapping(typeof(ExampleClass));

            mapping.AddReference(new ManyToOneMapping(null));

            writer.VerifyXml(mapping)
                .Element("many-to-one").Exists();
        }

        [Test]
        public void ShouldWriteOneToOnes()
        {
            var mapping = new ComponentMapping(typeof(ExampleClass));

            mapping.AddOneToOne(new OneToOneMapping(null));

            writer.VerifyXml(mapping)
                .Element("one-to-one").Exists();
        }

        [Test]
        public void ShouldWriteAnys()
        {
            var mapping = new ComponentMapping(typeof(ExampleClass));

            mapping.AddAny(new AnyMapping(null));

            writer.VerifyXml(mapping)
                .Element("any").Exists();
        }

        [Test]
        public void ShouldWriteMaps()
        {
            var mapping = new ComponentMapping(typeof(ExampleClass));

            mapping.AddCollection(new MapMapping(null));

            writer.VerifyXml(mapping)
                .Element("map").Exists();
        }

        [Test]
        public void ShouldWriteSets()
        {
            var mapping = new ComponentMapping(typeof(ExampleClass));

            mapping.AddCollection(new SetMapping(null));

            writer.VerifyXml(mapping)
                .Element("set").Exists();
        }

        [Test]
        public void ShouldWriteBags()
        {
            var mapping = new ComponentMapping(typeof(ExampleClass));

            mapping.AddCollection(new BagMapping(null));

            writer.VerifyXml(mapping)
                .Element("bag").Exists();
        }

        [Test]
        public void ShouldWriteLists()
        {
            var mapping = new ComponentMapping(typeof(ExampleClass));

            mapping.AddCollection(new ListMapping(null));

            writer.VerifyXml(mapping)
                .Element("list").Exists();
        }

        [Test, Ignore]
        public void ShouldWriteArrays()
        {
            Assert.Fail();
        }

        [Test, Ignore]
        public void ShouldWritePrimitiveArrays()
        {
            Assert.Fail();
        }
    }
}