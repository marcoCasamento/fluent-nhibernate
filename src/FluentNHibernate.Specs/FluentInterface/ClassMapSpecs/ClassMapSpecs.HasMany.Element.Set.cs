using System.Linq;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.Specs.FluentInterface.Fixtures;
using Machine.Specifications;

namespace FluentNHibernate.Specs.FluentInterface.ClassMapSpecs
{
    public class when_class_map_is_told_to_map_a_has_many_set_of_string_elements_with_a_custom_column : ProviderSpec
    {
        Because of = () =>
        {
            var mapping = map_as_class<EntityWithCollections>(m => m.HasMany(x => x.SetOfStrings, "custom"));
            collection = mapping.Collections.SingleOrDefault();
        };

        It should_use_the_specified_column_name_for_the_element = () =>
            collection.Element.Columns.Single().Name.ShouldEqual("custom");

        Behaves_like<HasManySetBehaviour> a_set;
        Behaves_like<HasManyElementBehaviour> an_has_many_element;

        protected static ICollectionMapping collection;
    }

    public class when_class_map_is_told_to_map_a_has_many_set_of_string_elements : ProviderSpec
    {
        Because of = () =>
        {
            var mapping = map_as_class<EntityWithCollections>(m => m.HasMany(x => x.SetOfStrings));
            collection = mapping.Collections.SingleOrDefault();
        };

        It should_use_the_default_column_name_for_the_element = () =>
            collection.Element.Columns.Single().Name.ShouldEqual("value");

        Behaves_like<HasManySetBehaviour> a_set;
        Behaves_like<HasManyElementBehaviour> an_has_many_element;

        protected static ICollectionMapping collection;
    }

    public class when_class_map_is_told_to_map_a_has_many_set_of_int_elements_with_a_custom_column : ProviderSpec
    {
        Because of = () =>
        {
            var mapping = map_as_class<EntityWithCollections>(m => m.HasMany(x => x.SetOfInts, "custom"));
            collection = mapping.Collections.SingleOrDefault();
        };

        It should_use_the_specified_column_name_for_the_element = () =>
            collection.Element.Columns.Single().Name.ShouldEqual("custom");

        Behaves_like<HasManySetBehaviour> a_set;
        Behaves_like<HasManyElementBehaviour> an_has_many_element;

        protected static ICollectionMapping collection;
    }

    public class when_class_map_is_told_to_map_a_has_many_set_of_int_elements : ProviderSpec
    {
        Because of = () =>
        {
            var mapping = map_as_class<EntityWithCollections>(m => m.HasMany(x => x.SetOfInts));
            collection = mapping.Collections.SingleOrDefault();
        };

        It should_use_the_default_column_name_for_the_element = () =>
            collection.Element.Columns.Single().Name.ShouldEqual("value");

        Behaves_like<HasManySetBehaviour> a_set;
        Behaves_like<HasManyElementBehaviour> an_has_many_element;

        protected static ICollectionMapping collection;
    }

    public class when_class_map_is_told_to_map_a_has_many_set_of_double_elements_with_a_custom_column : ProviderSpec
    {
        Because of = () =>
        {
            var mapping = map_as_class<EntityWithCollections>(m => m.HasMany(x => x.SetOfDoubles, "custom"));
            collection = mapping.Collections.SingleOrDefault();
        };

        It should_use_the_specified_column_name_for_the_element = () =>
            collection.Element.Columns.Single().Name.ShouldEqual("custom");

        Behaves_like<HasManySetBehaviour> a_set;
        Behaves_like<HasManyElementBehaviour> an_has_many_element;

        protected static ICollectionMapping collection;
    }

    public class when_class_map_is_told_to_map_a_has_many_set_of_double_elements : ProviderSpec
    {
        Because of = () =>
        {
            var mapping = map_as_class<EntityWithCollections>(m => m.HasMany(x => x.SetOfDoubles));
            collection = mapping.Collections.SingleOrDefault();
        };

        It should_use_the_default_column_name_for_the_element = () =>
            collection.Element.Columns.Single().Name.ShouldEqual("value");

        Behaves_like<HasManySetBehaviour> a_set;
        Behaves_like<HasManyElementBehaviour> an_has_many_element;

        protected static ICollectionMapping collection;
    }

    public class when_class_map_is_told_to_map_a_has_many_set_of_long_elements_with_a_custom_column : ProviderSpec
    {
        Because of = () =>
        {
            var mapping = map_as_class<EntityWithCollections>(m => m.HasMany(x => x.SetOfLongs, "custom"));
            collection = mapping.Collections.SingleOrDefault();
        };

        It should_use_the_specified_column_name_for_the_element = () =>
            collection.Element.Columns.Single().Name.ShouldEqual("custom");

        Behaves_like<HasManySetBehaviour> a_set;
        Behaves_like<HasManyElementBehaviour> an_has_many_element;

        protected static ICollectionMapping collection;
    }

    public class when_class_map_is_told_to_map_a_has_many_set_of_long_elements : ProviderSpec
    {
        Because of = () =>
        {
            var mapping = map_as_class<EntityWithCollections>(m => m.HasMany(x => x.SetOfLongs));
            collection = mapping.Collections.SingleOrDefault();
        };

        It should_use_the_default_column_name_for_the_element = () =>
            collection.Element.Columns.Single().Name.ShouldEqual("value");

        Behaves_like<HasManySetBehaviour> a_set;
        Behaves_like<HasManyElementBehaviour> an_has_many_element;

        protected static ICollectionMapping collection;
    }

    public class when_class_map_is_told_to_map_a_has_many_set_of_float_elements_with_a_custom_column : ProviderSpec
    {
        Because of = () =>
        {
            var mapping = map_as_class<EntityWithCollections>(m => m.HasMany(x => x.SetOfFloats, "custom"));
            collection = mapping.Collections.SingleOrDefault();
        };

        It should_use_the_specified_column_name_for_the_element = () =>
            collection.Element.Columns.Single().Name.ShouldEqual("custom");

        Behaves_like<HasManySetBehaviour> a_set;
        Behaves_like<HasManyElementBehaviour> an_has_many_element;

        protected static ICollectionMapping collection;
    }

    public class when_class_map_is_told_to_map_a_has_many_set_of_float_elements : ProviderSpec
    {
        Because of = () =>
        {
            var mapping = map_as_class<EntityWithCollections>(m => m.HasMany(x => x.SetOfFloats));
            collection = mapping.Collections.SingleOrDefault();
        };

        It should_use_the_default_column_name_for_the_element = () =>
            collection.Element.Columns.Single().Name.ShouldEqual("value");

        Behaves_like<HasManySetBehaviour> a_set;
        Behaves_like<HasManyElementBehaviour> an_has_many_element;

        protected static ICollectionMapping collection;
    }

    public class when_class_map_is_told_to_map_a_has_many_set_of_short_elements_with_a_custom_column : ProviderSpec
    {
        Because of = () =>
        {
            var mapping = map_as_class<EntityWithCollections>(m => m.HasMany(x => x.SetOfShorts, "custom"));
            collection = mapping.Collections.SingleOrDefault();
        };

        It should_use_the_specified_column_name_for_the_element = () =>
            collection.Element.Columns.Single().Name.ShouldEqual("custom");

        Behaves_like<HasManySetBehaviour> a_set;
        Behaves_like<HasManyElementBehaviour> an_has_many_element;

        protected static ICollectionMapping collection;
    }

    public class when_class_map_is_told_to_map_a_has_many_set_of_short_elements : ProviderSpec
    {
        Because of = () =>
        {
            var mapping = map_as_class<EntityWithCollections>(m => m.HasMany(x => x.SetOfShorts));
            collection = mapping.Collections.SingleOrDefault();
        };

        It should_use_the_default_column_name_for_the_element = () =>
            collection.Element.Columns.Single().Name.ShouldEqual("value");

        Behaves_like<HasManySetBehaviour> a_set;
        Behaves_like<HasManyElementBehaviour> an_has_many_element;

        protected static ICollectionMapping collection;
    }

    public class when_class_map_is_told_to_map_a_has_many_set_of_bool_elements_with_a_custom_column : ProviderSpec
    {
        Because of = () =>
        {
            var mapping = map_as_class<EntityWithCollections>(m => m.HasMany(x => x.SetOfBools, "custom"));
            collection = mapping.Collections.SingleOrDefault();
        };

        It should_use_the_specified_column_name_for_the_element = () =>
            collection.Element.Columns.Single().Name.ShouldEqual("custom");

        Behaves_like<HasManySetBehaviour> a_set;
        Behaves_like<HasManyElementBehaviour> an_has_many_element;

        protected static ICollectionMapping collection;
    }

    public class when_class_map_is_told_to_map_a_has_many_set_of_bool_elements : ProviderSpec
    {
        Because of = () =>
        {
            var mapping = map_as_class<EntityWithCollections>(m => m.HasMany(x => x.SetOfBools));
            collection = mapping.Collections.SingleOrDefault();
        };

        It should_use_the_default_column_name_for_the_element = () =>
            collection.Element.Columns.Single().Name.ShouldEqual("value");

        Behaves_like<HasManySetBehaviour> a_set;
        Behaves_like<HasManyElementBehaviour> an_has_many_element;

        protected static ICollectionMapping collection;
    }

    public class when_class_map_is_told_to_map_a_has_many_set_of_char_elements_with_a_custom_column : ProviderSpec
    {
        Because of = () =>
        {
            var mapping = map_as_class<EntityWithCollections>(m => m.HasMany(x => x.SetOfChars, "custom"));
            collection = mapping.Collections.SingleOrDefault();
        };

        It should_use_the_specified_column_name_for_the_element = () =>
            collection.Element.Columns.Single().Name.ShouldEqual("custom");

        Behaves_like<HasManySetBehaviour> a_set;
        Behaves_like<HasManyElementBehaviour> an_has_many_element;

        protected static ICollectionMapping collection;
    }

    public class when_class_map_is_told_to_map_a_has_many_set_of_char_elements : ProviderSpec
    {
        Because of = () =>
        {
            var mapping = map_as_class<EntityWithCollections>(m => m.HasMany(x => x.SetOfChars));
            collection = mapping.Collections.SingleOrDefault();
        };

        It should_use_the_default_column_name_for_the_element = () =>
            collection.Element.Columns.Single().Name.ShouldEqual("value");

        Behaves_like<HasManySetBehaviour> a_set;
        Behaves_like<HasManyElementBehaviour> an_has_many_element;

        protected static ICollectionMapping collection;
    }

    public class when_class_map_is_told_to_map_a_has_many_set_of_date_time_elements_with_a_custom_column : ProviderSpec
    {
        Because of = () =>
        {
            var mapping = map_as_class<EntityWithCollections>(m => m.HasMany(x => x.SetOfDateTimes, "custom"));
            collection = mapping.Collections.SingleOrDefault();
        };

        It should_use_the_specified_column_name_for_the_element = () =>
            collection.Element.Columns.Single().Name.ShouldEqual("custom");

        Behaves_like<HasManySetBehaviour> a_set;
        Behaves_like<HasManyElementBehaviour> an_has_many_element;

        protected static ICollectionMapping collection;
    }

    public class when_class_map_is_told_to_map_a_has_many_set_of_date_time_elements : ProviderSpec
    {
        Because of = () =>
        {
            var mapping = map_as_class<EntityWithCollections>(m => m.HasMany(x => x.SetOfDateTimes));
            collection = mapping.Collections.SingleOrDefault();
        };

        It should_use_the_default_column_name_for_the_element = () =>
            collection.Element.Columns.Single().Name.ShouldEqual("value");

        Behaves_like<HasManySetBehaviour> a_set;
        Behaves_like<HasManyElementBehaviour> an_has_many_element;

        protected static ICollectionMapping collection;
    }

    public class when_class_map_is_told_to_map_a_has_many_set_of_decimal_elements_with_a_custom_column : ProviderSpec
    {
        Because of = () =>
        {
            var mapping = map_as_class<EntityWithCollections>(m => m.HasMany(x => x.SetOfDecimals, "custom"));
            collection = mapping.Collections.SingleOrDefault();
        };

        It should_use_the_specified_column_name_for_the_element = () =>
            collection.Element.Columns.Single().Name.ShouldEqual("custom");

        Behaves_like<HasManySetBehaviour> a_set;
        Behaves_like<HasManyElementBehaviour> an_has_many_element;

        protected static ICollectionMapping collection;
    }

    public class when_class_map_is_told_to_map_a_has_many_set_of_decimal_elements : ProviderSpec
    {
        Because of = () =>
        {
            var mapping = map_as_class<EntityWithCollections>(m => m.HasMany(x => x.SetOfDecimals));
            collection = mapping.Collections.SingleOrDefault();
        };

        It should_use_the_default_column_name_for_the_element = () =>
            collection.Element.Columns.Single().Name.ShouldEqual("value");

        Behaves_like<HasManySetBehaviour> a_set;
        Behaves_like<HasManyElementBehaviour> an_has_many_element;

        protected static ICollectionMapping collection;
    }
}
