using System;
using System.Linq;
using System.Linq.Expressions;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Structure;
using FluentNHibernate.Testing.Fixtures;
using FluentNHibernate.Utils.Reflection;
using NUnit.Framework;

namespace FluentNHibernate.Testing.ConventionsTests.Inspection
{
    [TestFixture, Category("Inspection DSL")]
    public class AnyInspectorMapsToAnyMapping
    {
        private IMappingStructure<AnyMapping> structure;
        private IAnyInspector inspector;

        [SetUp]
        public void CreateDsl()
        {
            structure = Structures.Any(FakeMembers.Entity);
            inspector = new AnyInspector(structure);
        }

        [Test]
        public void AccessMapped()
        {
            structure.SetValue(Attr.Access, "field");
            inspector.Access.ShouldEqual(Access.Field);
        }

        [Test]
        public void AccessIsSet()
        {
            structure.SetValue(Attr.Access, "field");
            inspector.IsSet(Prop(x => x.Access))
                .ShouldBeTrue();
        }

        [Test]
        public void AccessIsNotSet()
        {
            inspector.IsSet(Prop(x => x.Access))
                .ShouldBeFalse();
        }

        [Test]
        public void CascadeMapped()
        {
            structure.SetValue(Attr.Cascade, "all");
            inspector.Cascade.ShouldEqual(Cascade.All);
        }

        [Test]
        public void CascadeIsSet()
        {
            structure.SetValue(Attr.Cascade, "all");
            inspector.IsSet(Prop(x => x.Cascade))
                .ShouldBeTrue();
        }

        [Test]
        public void CascadeIsNotSet()
        {
            inspector.IsSet(Prop(x => x.Cascade))
                .ShouldBeFalse();
        }

        [Test]
        public void IdentifierColumnsCollectionHasSameCountAsMapping()
        {
            structure.AddChild(Structures.Column(structure));
            inspector.Columns.Count().ShouldEqual(1);
        }

        [Test]
        public void IdentifierColumnsCollectionOfInspectors()
        {
            structure.AddChild(Structures.Column(structure));
            inspector.Columns.First().ShouldBeOfType<IColumnInspector>();
        }

        [Test]
        public void IdentifierColumnsCollectionIsEmpty()
        {
            inspector.Columns.IsEmpty().ShouldBeTrue();
        }

        [Test]
        public void IdTypeMapped()
        {
            structure.SetValue(Attr.IdType, "type");
            inspector.IdType.ShouldEqual("type");
        }

        [Test]
        public void IdTypeIsSet()
        {
            structure.SetValue(Attr.IdType, "type");
            inspector.IsSet(Prop(x => x.IdType))
                .ShouldBeTrue();
        }

        [Test]
        public void IdTypeIsNotSet()
        {
            inspector.IsSet(Prop(x => x.IdType))
                .ShouldBeFalse();
        }

        [Test]
        public void InsertMapped()
        {
            structure.SetValue(Attr.Insert, true);
            inspector.Insert.ShouldEqual(true);
        }

        [Test]
        public void InsertIsSet()
        {
            structure.SetValue(Attr.Insert, true);
            inspector.IsSet(Prop(x => x.Insert))
                .ShouldBeTrue();
        }

        [Test]
        public void InsertIsNotSet()
        {
            inspector.IsSet(Prop(x => x.Insert))
                .ShouldBeFalse();
        }

        [Test]
        public void LazyMapped()
        {
            structure.SetValue(Attr.Lazy, true);
            inspector.LazyLoad.ShouldEqual(true);
        }

        [Test]
        public void LazyIsSet()
        {
            structure.SetValue(Attr.Lazy, true);
            inspector.IsSet(Prop(x => x.LazyLoad))
                .ShouldBeTrue();
        }

        [Test]
        public void LazyIsNotSet()
        {
            inspector.IsSet(Prop(x => x.LazyLoad))
                .ShouldBeFalse();
        }

        [Test]
        public void MetaTypeMapped()
        {
            structure.SetValue(Attr.MetaType, new TypeReference(typeof(string)));
            inspector.MetaType.ShouldEqual(new TypeReference(typeof(string)));
        }

        [Test]
        public void MetaTypeIsSet()
        {
            structure.SetValue(Attr.MetaType, new TypeReference(typeof(string)));
            inspector.IsSet(Prop(x => x.MetaType))
                .ShouldBeTrue();
        }

        [Test]
        public void MetaTypeIsNotSet()
        {
            inspector.IsSet(Prop(x => x.MetaType))
                .ShouldBeFalse();
        }

        [Test]
        public void MetaValuesCollectionHasSameCountAsMapping()
        {
            structure.AddChild(Structures.MetaValue(FakeMembers.Type));
            inspector.MetaValues.Count().ShouldEqual(1);
        }

        [Test]
        public void MetaValuesCollectionOfInspectors()
        {
            structure.AddChild(Structures.MetaValue(FakeMembers.Type));
            inspector.MetaValues.First().ShouldBeOfType<IMetaValueInspector>();
        }

        [Test]
        public void MetaValuesCollectionIsEmpty()
        {
            inspector.MetaValues.IsEmpty().ShouldBeTrue();
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
        public void OptimisticLockMapped()
        {
            structure.SetValue(Attr.OptimisticLock, true);
            inspector.OptimisticLock.ShouldEqual(true);
        }

        [Test]
        public void OptimisticLockIsSet()
        {
            structure.SetValue(Attr.OptimisticLock, true);
            inspector.IsSet(Prop(x => x.OptimisticLock))
                .ShouldBeTrue();
        }

        [Test]
        public void OptimisticLockIsNotSet()
        {
            inspector.IsSet(Prop(x => x.OptimisticLock))
                .ShouldBeFalse();
        }

        [Test]
        public void TypeColumnsCollectionHasSameCountAsMapping()
        {
            structure.AddChild(Structures.Column(structure));
            inspector.Columns.Count().ShouldEqual(1);
        }

        [Test]
        public void TypeColumnsCollectionOfInspectors()
        {
            structure.AddChild(Structures.Column(structure));
            inspector.Columns.First().ShouldBeOfType<IColumnInspector>();
        }

        [Test]
        public void TypeColumnsCollectionIsEmpty()
        {
            inspector.Columns.IsEmpty().ShouldBeTrue();
        }

        [Test]
        public void UpdateMapped()
        {
            structure.SetValue(Attr.Update, true);
            inspector.Update.ShouldEqual(true);
        }

        [Test]
        public void UpdateIsSet()
        {
            structure.SetValue(Attr.Update, true);
            inspector.IsSet(Prop(x => x.Update))
                .ShouldBeTrue();
        }

        [Test]
        public void UpdateIsNotSet()
        {
            inspector.IsSet(Prop(x => x.Update))
                .ShouldBeFalse();
        }

        #region Helpers

        private Member Prop(Expression<Func<IAnyInspector, object>> propertyExpression)
        {
            return ReflectionHelper.GetMember(propertyExpression);
        }

        #endregion
    }
}