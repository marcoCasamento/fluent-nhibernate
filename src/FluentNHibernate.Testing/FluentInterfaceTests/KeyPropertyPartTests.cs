﻿using FluentNHibernate.Mapping;
using FluentNHibernate.MappingModel;
using NUnit.Framework;
using FluentNHibernate.MappingModel.Identity;

namespace FluentNHibernate.Testing.FluentInterfaceTests
{
    [TestFixture]
    public class KeyPropertyPartTests
    {
        KeyPropertyPart part;
        IMappingStructure<KeyPropertyMapping> structure;

        [SetUp]
        public void SetUp()
        {
            structure = new BucketStructure<KeyPropertyMapping>();
            part = new KeyPropertyPart(structure);
        }

        [Test]
        public void ShouldSetColumnName()
        {
            part.ColumnName("col1");
            structure.ShouldHaveValue(Attr.Name, "col1");
        }

        [Test]
        public void ShouldSetType()
        {
            part.Type(typeof(string));
            structure.ShouldHaveValue(Attr.Type, new TypeReference(typeof(string)));
        }

        [Test]
        public void ShouldSetAccessStrategy()
        {
            part.Access.Field();
            structure.ShouldHaveValue(Attr.Access, "field");
        }
    }
}
