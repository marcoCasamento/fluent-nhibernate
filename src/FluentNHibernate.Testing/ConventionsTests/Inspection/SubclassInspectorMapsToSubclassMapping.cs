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
using FluentNHibernate.Utils.Reflection;
using NUnit.Framework;

namespace FluentNHibernate.Testing.ConventionsTests.Inspection
{
    [TestFixture, Category("Inspection DSL")]
    public class SubclassInspectorMapsToSubclassMapping
    {
        private SubclassMapping mapping;
        private ISubclassInspector inspector;

        [SetUp]
        public void CreateDsl()
        {
            mapping = new SubclassMapping(typeof(ExampleClass));
            inspector = new SubclassInspector(mapping);
        }

        [Test]
        public void AbstractMapped()
        {
            mapping.Abstract = true;
            inspector.Abstract.ShouldEqual(true);
        }

        [Test]
        public void AbstractIsSet()
        {
            mapping.Abstract = true;
            inspector.IsSet(Prop(x => x.Abstract))
                .ShouldBeTrue();
        }

        [Test]
        public void AbstractIsNotSet()
        {
            inspector.IsSet(Prop(x => x.Abstract))
                .ShouldBeFalse();
        }

        [Test]
        public void AnysCollectionHasSameCountAsMapping()
        {
            mapping.AddAny(new AnyMapping(null));
            inspector.Anys.Count().ShouldEqual(1);
        }

        [Test]
        public void AnysCollectionOfInspectors()
        {
            mapping.AddAny(new AnyMapping(null));
            inspector.Anys.First().ShouldBeOfType<IAnyInspector>();
        }

        [Test]
        public void AnysCollectionIsEmpty()
        {
            inspector.Anys.IsEmpty().ShouldBeTrue();
        }

        [Test]
        public void CollectionsCollectionHasSameCountAsMapping()
        {
            mapping.AddCollection(new BagMapping(null));
            inspector.Collections.Count().ShouldEqual(1);
        }

        [Test]
        public void CollectionsCollectionOfInspectors()
        {
            mapping.AddCollection(new BagMapping(null));
            inspector.Collections.First().ShouldBeOfType<ICollectionInspector>();
        }

        [Test]
        public void CollectionsCollectionIsEmpty()
        {
            inspector.Collections.IsEmpty().ShouldBeTrue();
        }

        [Test]
        public void DiscriminatorValueMapped()
        {
            mapping.DiscriminatorValue = "value";
            inspector.DiscriminatorValue.ShouldEqual("value");
        }

        [Test]
        public void DiscriminatorValueIsSet()
        {
            mapping.DiscriminatorValue = "value";
            inspector.IsSet(Prop(x => x.DiscriminatorValue))
                .ShouldBeTrue();
        }

        [Test]
        public void DiscriminatorValueIsNotSet()
        {
            inspector.IsSet(Prop(x => x.DiscriminatorValue))
                .ShouldBeFalse();
        }

        [Test]
        public void DynamicInsertMapped()
        {
            mapping.DynamicInsert = true;
            inspector.DynamicInsert.ShouldEqual(true);
        }

        [Test]
        public void DynamicInsertIsSet()
        {
            mapping.DynamicInsert = true;
            inspector.IsSet(Prop(x => x.DynamicInsert))
                .ShouldBeTrue();
        }

        [Test]
        public void DynamicInsertIsNotSet()
        {
            inspector.IsSet(Prop(x => x.DynamicInsert))
                .ShouldBeFalse();
        }

        [Test]
        public void DynamicUpdateMapped()
        {
            mapping.DynamicUpdate = true;
            inspector.DynamicUpdate.ShouldEqual(true);
        }

        [Test]
        public void DynamicUpdateIsSet()
        {
            mapping.DynamicUpdate = true;
            inspector.IsSet(Prop(x => x.DynamicUpdate))
                .ShouldBeTrue();
        }

        [Test]
        public void DynamicUpdateIsNotSet()
        {
            inspector.IsSet(Prop(x => x.DynamicUpdate))
                .ShouldBeFalse();
        }

        [Test]
        public void ExtendsMapped()
        {
            mapping.Extends = "other-class";
            inspector.Extends.ShouldEqual("other-class");
        }

        [Test]
        public void ExtendsIsSet()
        {
            mapping.Extends = "other-class";
            inspector.IsSet(Prop(x => x.Extends))
                .ShouldBeTrue();
        }

        [Test]
        public void ExtendsIsNotSet()
        {
            inspector.IsSet(Prop(x => x.Extends))
                .ShouldBeFalse();
        }

        [Test]
        public void JoinsCollectionHasSameCountAsMapping()
        {
            mapping.AddJoin(new JoinMapping());
            inspector.Joins.Count().ShouldEqual(1);
        }

        [Test]
        public void JoinsCollectionOfInspectors()
        {
            mapping.AddJoin(new JoinMapping());
            inspector.Joins.First().ShouldBeOfType<IJoinInspector>();
        }

        [Test]
        public void JoinsCollectionIsEmpty()
        {
            inspector.Joins.IsEmpty().ShouldBeTrue();
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
        public void OneToOnesCollectionHasSameCountAsMapping()
        {
            mapping.AddOneToOne(new OneToOneMapping(null));
            inspector.OneToOnes.Count().ShouldEqual(1);
        }

        [Test]
        public void OneToOnesCollectionOfInspectors()
        {
            mapping.AddOneToOne(new OneToOneMapping(null));
            inspector.OneToOnes.First().ShouldBeOfType<IOneToOneInspector>();
        }

        [Test]
        public void OneToOnesCollectionIsEmpty()
        {
            inspector.OneToOnes.IsEmpty().ShouldBeTrue();
        }

        [Test]
        public void PropertiesCollectionHasSameCountAsMapping()
        {
            mapping.AddProperty(new PropertyMapping(null));
            inspector.Properties.Count().ShouldEqual(1);
        }

        [Test]
        public void PropertiesCollectionOfInspectors()
        {
            mapping.AddProperty(new PropertyMapping(null));
            inspector.Properties.First().ShouldBeOfType<IPropertyInspector>();
        }

        [Test]
        public void PropertiesCollectionIsEmpty()
        {
            inspector.Properties.IsEmpty().ShouldBeTrue();
        }

        [Test]
        public void ProxyMapped()
        {
            mapping.Proxy = "proxy";
            inspector.Proxy.ShouldEqual("proxy");
        }

        [Test]
        public void ProxyIsSet()
        {
            mapping.Proxy = "proxy";
            inspector.IsSet(Prop(x => x.Proxy))
                .ShouldBeTrue();
        }

        [Test]
        public void ProxyIsNotSet()
        {
            inspector.IsSet(Prop(x => x.Proxy))
                .ShouldBeFalse();
        }

        [Test]
        public void ReferencesCollectionHasSameCountAsMapping()
        {
            mapping.AddReference(new ManyToOneMapping(null));
            inspector.References.Count().ShouldEqual(1);
        }

        [Test]
        public void ReferencesCollectionOfInspectors()
        {
            mapping.AddReference(new ManyToOneMapping(null));
            inspector.References.First().ShouldBeOfType<IManyToOneInspector>();
        }

        [Test]
        public void ReferencesCollectionIsEmpty()
        {
            inspector.References.IsEmpty().ShouldBeTrue();
        }

        [Test]
        public void SelectBeforeUpdateMapped()
        {
            mapping.SelectBeforeUpdate = true;
            inspector.SelectBeforeUpdate.ShouldEqual(true);
        }

        [Test]
        public void SelectBeforeUpdateIsSet()
        {
            mapping.SelectBeforeUpdate = true;
            inspector.IsSet(Prop(x => x.SelectBeforeUpdate))
                .ShouldBeTrue();
        }

        [Test]
        public void SelectBeforeUpdateIsNotSet()
        {
            inspector.IsSet(Prop(x => x.SelectBeforeUpdate))
                .ShouldBeFalse();
        }

        [Test]
        public void SubclassesCollectionHasSameCountAsMapping()
        {
            mapping.AddSubclass(new SubclassMapping(typeof(ExampleClass)));
            inspector.Subclasses.Count().ShouldEqual(1);
        }

        [Test]
        public void SubclassesCollectionOfInspectors()
        {
            mapping.AddSubclass(new SubclassMapping(typeof(ExampleClass)));
            inspector.Subclasses.First().ShouldBeOfType<ISubclassInspector>();
        }

        [Test]
        public void SubclassesCollectionIsEmpty()
        {
            inspector.Subclasses.IsEmpty().ShouldBeTrue();
        }

        [Test]
        public void TypeMapped()
        {
            mapping.Type = typeof(ExampleClass);
            inspector.Type.ShouldEqual(typeof(ExampleClass));
        }

        [Test]
        public void TypeIsSet()
        {
            mapping.Type = typeof(ExampleClass);
            inspector.IsSet(Prop(x => x.Type))
                .ShouldBeTrue();
        }

        [Test]
        public void TypeIsNotSet()
        {
            inspector.IsSet(Prop(x => x.Type))
                .ShouldBeFalse();
        }

        #region Helpers

        private Member Prop(Expression<Func<ISubclassInspector, object>> propertyExpression)
        {
            return ReflectionHelper.GetMember(propertyExpression);
        }

        #endregion
    }
}