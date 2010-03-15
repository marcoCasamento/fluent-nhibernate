using System;
using System.Linq.Expressions;
using System.Reflection;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Structure;
using FluentNHibernate.Testing.DomainModel;
using FluentNHibernate.Utils.Reflection;
using NUnit.Framework;

namespace FluentNHibernate.Testing.ConventionsTests.Inspection
{
    [TestFixture, Category("Inspection DSL")]
    public class ColumnInspectorMapsToColumnMapping
    {
        private IMappingStructure<ColumnMapping> structure;
        private IColumnInspector inspector;

        [SetUp]
        public void CreateDsl()
        {
            structure = Structures.Column(Structures.Element());
            inspector = new ColumnInspector(structure);
        }

        [Test]
        public void CheckMapped()
        {
            structure.SetValue(Attr.Check, "chk");
            inspector.Check.ShouldEqual("chk");
        }

        [Test]
        public void CheckIsSet()
        {
            structure.SetValue(Attr.Check, "chk");
            inspector.IsSet(Prop(x => x.Check))
                .ShouldBeTrue();
        }

        [Test]
        public void CheckIsNotSet()
        {
            inspector.IsSet(Prop(x => x.Check))
                .ShouldBeFalse();
        }

        [Test]
        public void DefaultMapped()
        {
            structure.SetValue(Attr.Default, "value");
            inspector.Default.ShouldEqual("value");
        }

        [Test]
        public void DefaultIsSet()
        {
            structure.SetValue(Attr.Default, "value");
            inspector.IsSet(Prop(x => x.Default))
                .ShouldBeTrue();
        }

        [Test]
        public void DefaultIsNotSet()
        {
            inspector.IsSet(Prop(x => x.Default))
                .ShouldBeFalse();
        }

        [Test]
        public void IndexMapped()
        {
            structure.SetValue(Attr.Index, "ix");
            inspector.Index.ShouldEqual("ix");
        }

        [Test]
        public void IndexIsSet()
        {
            structure.SetValue(Attr.Index, "ix");
            inspector.IsSet(Prop(x => x.Index))
                .ShouldBeTrue();
        }

        [Test]
        public void IndexIsNotSet()
        {
            inspector.IsSet(Prop(x => x.Index))
                .ShouldBeFalse();
        }

        [Test]
        public void LengthMapped()
        {
            structure.SetValue(Attr.Length, 100);
            inspector.Length.ShouldEqual(100);
        }

        [Test]
        public void LengthIsSet()
        {
            structure.SetValue(Attr.Length, 100);
            inspector.IsSet(Prop(x => x.Length))
                .ShouldBeTrue();
        }

        [Test]
        public void LengthIsNotSet()
        {
            inspector.IsSet(Prop(x => x.Length))
                .ShouldBeFalse();
        }

        [Test]
        public void NameMapped()
        {
            structure.SetValue(Attr.Name, "name");
            inspector.Name.ShouldEqual("name");
        }

        [Test]
        public void NameIsSet()
        {
            structure.SetValue(Attr.Name, "name");
            inspector.IsSet(Prop(x => x.Name))
                .ShouldBeTrue();
        }

        [Test]
        public void NameIsNotSet()
        {
            inspector.IsSet(Prop(x => x.Name))
                .ShouldBeFalse();
        }

        [Test]
        public void NotNullMapped()
        {
            structure.SetValue(Attr.NotNull, true);
            inspector.NotNull.ShouldBeTrue();
        }

        [Test]
        public void NotNullIsSet()
        {
            structure.SetValue(Attr.NotNull, true);
            inspector.IsSet(Prop(x => x.NotNull))
                .ShouldBeTrue();
        }

        [Test]
        public void NotNullIsNotSet()
        {
            inspector.IsSet(Prop(x => x.NotNull))
                .ShouldBeFalse();
        }

        [Test]
        public void PrecisionMapped()
        {
            structure.SetValue(Attr.Precision, 10);
            inspector.Precision.ShouldEqual(10);
        }

        [Test]
        public void PrecisionIsSet()
        {
            structure.SetValue(Attr.Precision, 10);
            inspector.IsSet(Prop(x => x.Precision))
                .ShouldBeTrue();
        }

        [Test]
        public void PrecisionIsNotSet()
        {
            inspector.IsSet(Prop(x => x.Precision))
                .ShouldBeFalse();
        }

        [Test]
        public void ScaleMapped()
        {
            structure.SetValue(Attr.Scale, 10);
            inspector.Scale.ShouldEqual(10);
        }

        [Test]
        public void ScaleIsSet()
        {
            structure.SetValue(Attr.Scale, 10);
            inspector.IsSet(Prop(x => x.Scale))
                .ShouldBeTrue();
        }

        [Test]
        public void ScaleIsNotSet()
        {
            inspector.IsSet(Prop(x => x.Scale))
                .ShouldBeFalse();
        }

        [Test]
        public void SqlTypeMapped()
        {
            structure.SetValue(Attr.SqlType, "type");
            inspector.SqlType.ShouldEqual("type");
        }

        [Test]
        public void SqlTypeIsSet()
        {
            structure.SetValue(Attr.SqlType, "type");
            inspector.IsSet(Prop(x => x.SqlType))
                .ShouldBeTrue();
        }

        [Test]
        public void SqlTypeIsNotSet()
        {
            inspector.IsSet(Prop(x => x.SqlType))
                .ShouldBeFalse();
        }

        [Test]
        public void UniqueMapped()
        {
            structure.SetValue(Attr.Unique, true);
            inspector.Unique.ShouldBeTrue();
        }

        [Test]
        public void UniqueIsSet()
        {
            structure.SetValue(Attr.Unique, true);
            inspector.IsSet(Prop(x => x.Unique))
                .ShouldBeTrue();
        }

        [Test]
        public void UniqueIsNotSet()
        {
            inspector.IsSet(Prop(x => x.Unique))
                .ShouldBeFalse();
        }

        [Test]
        public void UniqueKeyMapped()
        {
            structure.SetValue(Attr.UniqueKey, "key");
            inspector.UniqueKey.ShouldEqual("key");
        }

        [Test]
        public void UniqueKeyIsSet()
        {
            structure.SetValue(Attr.UniqueKey, "key");
            inspector.IsSet(Prop(x => x.UniqueKey))
                .ShouldBeTrue();
        }

        [Test]
        public void UniqueKeyIsNotSet()
        {
            inspector.IsSet(Prop(x => x.UniqueKey))
                .ShouldBeFalse();
        }

        private Member Prop(Expression<Func<IColumnInspector, object>> propertyExpression)
        {
            return ReflectionHelper.GetMember(propertyExpression);
        }
    }
}