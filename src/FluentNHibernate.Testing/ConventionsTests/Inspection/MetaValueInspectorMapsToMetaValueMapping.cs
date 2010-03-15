using System;
using System.Linq.Expressions;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Structure;
using FluentNHibernate.Testing.Fixtures;
using FluentNHibernate.Utils.Reflection;
using NUnit.Framework;

namespace FluentNHibernate.Testing.ConventionsTests.Inspection
{
    [TestFixture, Category("Inspection DSL")]
    public class MetaValueInspectorMapsToMetaValueMapping
    {
        private IMappingStructure<MetaValueMapping> structure;
        private IMetaValueInspector inspector;

        [SetUp]
        public void CreateDsl()
        {
            structure = Structures.MetaValue(FakeMembers.Type);
            inspector = new MetaValueInspector(structure);
        }

        [Test]
        public void ClassMapped()
        {
            structure.SetValue(Attr.Class, new TypeReference(typeof(string)));
            inspector.Class.ShouldEqual(new TypeReference(typeof(string)));
        }

        [Test]
        public void ClassIsSet()
        {
            structure.SetValue(Attr.Class, new TypeReference(typeof(string)));
            inspector.IsSet(Prop(x => x.Class))
                .ShouldBeTrue();
        }

        [Test]
        public void ClassIsNotSet()
        {
            inspector.IsSet(Prop(x => x.Class))
                .ShouldBeFalse();
        }

        [Test]
        public void ValueMapped()
        {
            structure.SetValue(Attr.Value, "value");
            inspector.Value.ShouldEqual("value");
        }

        [Test]
        public void ValueIsSet()
        {
            structure.SetValue(Attr.Value, "value");
            inspector.IsSet(Prop(x => x.Value))
                .ShouldBeTrue();
        }

        [Test]
        public void ValueIsNotSet()
        {
            inspector.IsSet(Prop(x => x.Value))
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