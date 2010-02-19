using System;
using System.Linq.Expressions;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.MappingModel;
using FluentNHibernate.Utils.Reflection;
using FluentNHibernate.Testing.Utils;
using NUnit.Framework;

namespace FluentNHibernate.Testing.ConventionsTests.Inspection
{
    [TestFixture, Category("Inspection DSL")]
    public class MetaValueInspectorMapsToMetaValueMapping
    {
        private MetaValueMapping mapping;
        private IMetaValueInspector inspector;

        [SetUp]
        public void CreateDsl()
        {
            mapping = new MetaValueMapping();
            inspector = new MetaValueInspector(mapping);
        }

        [Test]
        public void ClassMapped()
        {
            mapping.Class = new TypeReference(typeof(string));
            inspector.Class.ShouldEqual(new TypeReference(typeof(string)));
        }

        [Test]
        public void ClassIsSet()
        {
            mapping.Class = new TypeReference(typeof(string));
            inspector.IsSet(Attr.Class)
                .ShouldBeTrue();
        }

        [Test]
        public void ClassIsNotSet()
        {
            inspector.IsSet(Attr.Class)
                .ShouldBeFalse();
        }

        [Test]
        public void ValueMapped()
        {
            mapping.Value = "value";
            inspector.Value.ShouldEqual("value");
        }

        [Test]
        public void ValueIsSet()
        {
            mapping.Value = "value";
            inspector.IsSet(Attr.Value)
                .ShouldBeTrue();
        }

        [Test]
        public void ValueIsNotSet()
        {
            inspector.IsSet(Attr.Value)
                .ShouldBeFalse();
        }

        #region Helpers

        private Member Prop(Expression<Func<IMetaValueInspector, object>> propertyExpression)
        {
            return ReflectionHelper.GetMember(propertyExpression);
        }

        #endregion
    }
}