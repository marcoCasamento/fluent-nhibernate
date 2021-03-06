using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FluentNHibernate.Automapping.TestFixtures;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.MappingModel.Identity;
using FluentNHibernate.Testing.DomainModel;
using FluentNHibernate.Utils.Reflection;
using NUnit.Framework;

namespace FluentNHibernate.Testing.ConventionsTests.Inspection
{
    [TestFixture, Category("Inspection DSL")]
    public class OneToOneInspectorMapsToOneToOneMapping
    {
        private OneToOneMapping mapping;
        private IOneToOneInspector inspector;

        [SetUp]
        public void CreateDsl()
        {
            mapping = new OneToOneMapping();
            inspector = new OneToOneInspector(mapping);
        }

        [Test]
        public void AccessMapped()
        {
            mapping.Access = "field";
            inspector.Access.ShouldEqual(Access.Field);
        }

        [Test]
        public void AccessIsSet()
        {
            mapping.Access = "field";
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
            mapping.Cascade = "all";
            inspector.Cascade.ShouldEqual(Cascade.All);
        }

        [Test]
        public void CascadeIsSet()
        {
            mapping.Cascade = "all";
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
        public void ConstrainedMapped()
        {
            mapping.Constrained = true;
            inspector.Constrained.ShouldBeTrue();
        }

        [Test]
        public void ConstrainedIsSet()
        {
            mapping.Constrained = true;
            inspector.IsSet(Prop(x => x.Constrained))
                .ShouldBeTrue();
        }

        [Test]
        public void ConstrainedIsNotSet()
        {
            inspector.IsSet(Prop(x => x.Constrained))
                .ShouldBeFalse();
        }

        [Test]
        public void FetchMapped()
        {
            mapping.Fetch = "join";
            inspector.Fetch.ShouldEqual(Fetch.Join);
        }

        [Test]
        public void FetchIsSet()
        {
            mapping.Fetch = "join";
            inspector.IsSet(Prop(x => x.Fetch))
                .ShouldBeTrue();
        }

        [Test]
        public void FetchIsNotSet()
        {
            inspector.IsSet(Prop(x => x.Fetch))
                .ShouldBeFalse();
        }

        [Test]
        public void ForeignKeyMapped()
        {
            mapping.ForeignKey = "key";
            inspector.ForeignKey.ShouldEqual("key");
        }

        [Test]
        public void ForeignKeyIsSet()
        {
            mapping.ForeignKey = "key";
            inspector.IsSet(Prop(x => x.ForeignKey))
                .ShouldBeTrue();
        }

        [Test]
        public void ForeignKeyIsNotSet()
        {
            inspector.IsSet(Prop(x => x.ForeignKey))
                .ShouldBeFalse();
        }

        [Test]
        public void LazyMapped()
        {
            mapping.Lazy = true;
            inspector.LazyLoad.ShouldEqual(true);
        }

        [Test]
        public void LazyIsSet()
        {
            mapping.Lazy = true;
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
        public void NameMapped()
        {
            mapping.Name = "name";
            inspector.Name.ShouldEqual("name");
        }

        [Test]
        public void NameIsSet()
        {
            mapping.Name = "name";
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
        public void PropertyRefMapped()
        {
            mapping.PropertyRef = "ref";
            inspector.PropertyRef.ShouldEqual("ref");
        }

        [Test]
        public void PropertyRefIsSet()
        {
            mapping.PropertyRef = "ref";
            inspector.IsSet(Prop(x => x.PropertyRef))
                .ShouldBeTrue();
        }

        [Test]
        public void PropertyRefIsNotSet()
        {
            inspector.IsSet(Prop(x => x.PropertyRef))
                .ShouldBeFalse();
        }

        #region Helpers

        private Member Prop(Expression<Func<IOneToOneInspector, object>> propertyExpression)
        {
            return ReflectionHelper.GetMember(propertyExpression);
        }

        #endregion
    }
}