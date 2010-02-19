using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.Testing.Utils;
using NUnit.Framework;
using FluentNHibernate.MappingModel;
using System.Collections.Generic;

namespace FluentNHibernate.Testing.MappingModel
{
    [TestFixture]
    public class AttributeStoreTester
    {
        private sealed class TestStore : AttributeStore
        {
            public bool Lazy
            {
                get { return Get<bool>(Attr.Lazy); }
                set { Set(Attr.Lazy, value); }
            }

            public string Name
            {
                get { return Get(Attr.Name); }
                set { Set(Attr.Name, value); }
            }
        }

        [Test]
        public void UnsetAttributeShouldBeDefault()
        {
            var store = new TestStore();
            store.Lazy.ShouldBeFalse();
        }

        [Test]
        public void CanGetAndSetAttribute()
        {
            var store = new TestStore();
            store.Lazy = true;
            store.Lazy.ShouldBeTrue();            
        }

        [Test]
        public void CanCheckIfAttributeIsSpecified()
        {

            var store = new TestStore();            
            store.HasUserValue(Attr.Lazy).ShouldBeFalse();
            store.Lazy = true;
            store.HasUserValue(Attr.Lazy).ShouldBeTrue();
        }

        [Test]
        public void CanCopyAttributes()
        {
            var source = new TestStore();
            source.Lazy = true;

            var target = new TestStore();
            source.CopyTo(target);

            target.Lazy.ShouldBeTrue();
        }

        [Test]
        public void CopyingAttributesReplacesOldValues()
        {
            var source = new TestStore();
            source.Lazy = false;

            var target = new TestStore();
            target.Lazy = true;
            source.CopyTo(target);

            target.Lazy.ShouldBeFalse();
        }

        [Test]
        public void UnsetValuesAreNotCopied()
        {
            var source = new TestStore();

            var target = new TestStore();
            target.Lazy = true;
            source.CopyTo(target);

            target.Lazy.ShouldBeTrue();
        }

        [Test]
        public void CanSetDefaultValue()
        {
            var source = new TestStore();
            source.SetDefault(Attr.Lazy, true);
            
            source.Lazy.ShouldBeTrue();
        }

        [Test]
        public void DefaultValuesAreNotCopied()
        {
            var source = new TestStore();
            source.SetDefault(Attr.Lazy, true);

            var target = new TestStore();
            target.Lazy = false;
            source.CopyTo(target);

            target.Lazy.ShouldBeFalse();
        }
    }
}