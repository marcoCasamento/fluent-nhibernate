using System;
using System.Linq.Expressions;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.Utils.Reflection;
using FluentNHibernate.Testing.Utils;
using NUnit.Framework;

namespace FluentNHibernate.Testing.ConventionsTests.Inspection
{
    [TestFixture, Category("Inspection DSL")]
    public class BagInspectorMapsToBagMapping
    {
        private BagMapping mapping;
        private IBagInspector inspector;

        [SetUp]
        public void CreateDsl()
        {
            mapping = new BagMapping();
            inspector = new BagInspector(mapping);
        }

        [Test]
        public void OrderByIsSet()
        {
            mapping.OrderBy = "AField";
            inspector.IsSet(Attr.OrderBy)
                .ShouldBeTrue();
        }

        [Test]
        public void OrderByIsNotSet()
        {
            inspector.IsSet(Attr.OrderBy)
                .ShouldBeFalse();
        }
    }
}