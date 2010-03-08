using FluentNHibernate.Automapping.TestFixtures;
using FluentNHibernate.MappingModel.ClassBased;
using NUnit.Framework;

namespace FluentNHibernate.Testing.MappingModel.Defaults
{
    [TestFixture]
    public class ComponentMappingDefaultsTester
    {
        [Test]
        public void UniqueShouldDefaultToFalse()
        {
            var mapping = new ComponentMapping(typeof(ExampleClass));
            mapping.Unique.ShouldBeFalse();
        }

        [Test]
        public void InsertShouldDefaultToTrue()
        {
            var mapping = new ComponentMapping(typeof(ExampleClass));
            mapping.Insert.ShouldBeTrue();
        }

        [Test]
        public void UpdateShouldDefaultToTrue()
        {
            var mapping = new ComponentMapping(typeof(ExampleClass));
            mapping.Update.ShouldBeTrue();
        }

        [Test]
        public void OptimisticLockShouldDefaultToTrue()
        {
            var mapping = new ComponentMapping(typeof(ExampleClass));
            mapping.OptimisticLock.ShouldBeTrue();
        }

        [Test]
        public void LazyShouldDefaultToFalse()
        {
            var mapping = new ComponentMapping(typeof(ExampleClass));
            mapping.Lazy.ShouldBeFalse();
        }
    }
}
