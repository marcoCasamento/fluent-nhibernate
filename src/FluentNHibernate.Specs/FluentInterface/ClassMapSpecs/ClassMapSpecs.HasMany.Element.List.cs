using System.Linq;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.Specs.FluentInterface.Fixtures;
using Machine.Specifications;

namespace FluentNHibernate.Specs.FluentInterface.ClassMapSpecs
{
    public class when_class_map_is_told_to_map_a_has_many_list_of_string_elements_with_a_custom_column : ProviderSpec
    {
        Because of = () =>
        {
            var mapping = map_as_class<EntityWithCollections>(m => m.HasMany(x => x.BagOfStrings, "custom").AsList());
            collection = mapping.Collections.SingleOrDefault();
        };

        It should_use_the_specified_column_name_for_the_element = () =>
            collection.Element.Columns.Single().Name.ShouldEqual("custom");

        Behaves_like<HasManyListBehaviour> a_list;
        Behaves_like<HasManyElementBehaviour> an_has_many_element;

        protected static ICollectionMapping collection;
    }

    public class when_class_map_is_told_to_map_a_has_many_list_of_string_elements : ProviderSpec
    {
        Because of = () =>
        {
            var mapping = map_as_class<EntityWithCollections>(m => m.HasMany(x => x.BagOfStrings).AsList());
            collection = mapping.Collections.SingleOrDefault();
        };

        It should_use_the_default_column_name_for_the_element = () =>
            collection.Element.Columns.Single().Name.ShouldEqual("value");

        Behaves_like<HasManyListBehaviour> a_list;
        Behaves_like<HasManyElementBehaviour> an_has_many_element;

        protected static ICollectionMapping collection;
    }

    public class when_class_map_is_told_to_map_a_has_many_list_of_int_elements_with_a_custom_column : ProviderSpec
    {
        Because of = () =>
        {
            var mapping = map_as_class<EntityWithCollections>(m => m.HasMany(x => x.BagOfInts, "custom").AsList());
            collection = mapping.Collections.SingleOrDefault();
        };

        It should_use_the_specified_column_name_for_the_element = () =>
            collection.Element.Columns.Single().Name.ShouldEqual("custom");

        Behaves_like<HasManyListBehaviour> a_list;
        Behaves_like<HasManyElementBehaviour> an_has_many_element;

        protected static ICollectionMapping collection;
    }

    public class when_class_map_is_told_to_map_a_has_many_list_of_int_elements : ProviderSpec
    {
        Because of = () =>
        {
            var mapping = map_as_class<EntityWithCollections>(m => m.HasMany(x => x.BagOfInts).AsList());
            collection = mapping.Collections.SingleOrDefault();
        };

        It should_use_the_default_column_name_for_the_element = () =>
            collection.Element.Columns.Single().Name.ShouldEqual("value");

        Behaves_like<HasManyListBehaviour> a_list;
        Behaves_like<HasManyElementBehaviour> an_has_many_element;

        protected static ICollectionMapping collection;
    }

    public class when_class_map_is_told_to_map_a_has_many_list_of_double_elements_with_a_custom_column : ProviderSpec
    {
        Because of = () =>
        {
            var mapping = map_as_class<EntityWithCollections>(m => m.HasMany(x => x.BagOfDoubles, "custom").AsList());
            collection = mapping.Collections.SingleOrDefault();
        };

        It should_use_the_specified_column_name_for_the_element = () =>
            collection.Element.Columns.Single().Name.ShouldEqual("custom");

        Behaves_like<HasManyListBehaviour> a_list;
        Behaves_like<HasManyElementBehaviour> an_has_many_element;

        protected static ICollectionMapping collection;
    }

    public class when_class_map_is_told_to_map_a_has_many_list_of_double_elements : ProviderSpec
    {
        Because of = () =>
        {
            var mapping = map_as_class<EntityWithCollections>(m => m.HasMany(x => x.BagOfDoubles).AsList());
            collection = mapping.Collections.SingleOrDefault();
        };

        It should_use_the_default_column_name_for_the_element = () =>
            collection.Element.Columns.Single().Name.ShouldEqual("value");

        Behaves_like<HasManyListBehaviour> a_list;
        Behaves_like<HasManyElementBehaviour> an_has_many_element;

        protected static ICollectionMapping collection;
    }

    public class when_class_map_is_told_to_map_a_has_many_list_of_long_elements_with_a_custom_column : ProviderSpec
    {
        Because of = () =>
        {
            var mapping = map_as_class<EntityWithCollections>(m => m.HasMany(x => x.BagOfLongs, "custom").AsList());
            collection = mapping.Collections.SingleOrDefault();
        };

        It should_use_the_specified_column_name_for_the_element = () =>
            collection.Element.Columns.Single().Name.ShouldEqual("custom");

        Behaves_like<HasManyListBehaviour> a_list;
        Behaves_like<HasManyElementBehaviour> an_has_many_element;

        protected static ICollectionMapping collection;
    }

    public class when_class_map_is_told_to_map_a_has_many_list_of_long_elements : ProviderSpec
    {
        Because of = () =>
        {
            var mapping = map_as_class<EntityWithCollections>(m => m.HasMany(x => x.BagOfLongs).AsList());
            collection = mapping.Collections.SingleOrDefault();
        };

        It should_use_the_default_column_name_for_the_element = () =>
            collection.Element.Columns.Single().Name.ShouldEqual("value");

        Behaves_like<HasManyListBehaviour> a_list;
        Behaves_like<HasManyElementBehaviour> an_has_many_element;

        protected static ICollectionMapping collection;
    }

    public class when_class_map_is_told_to_map_a_has_many_list_of_float_elements_with_a_custom_column : ProviderSpec
    {
        Because of = () =>
        {
            var mapping = map_as_class<EntityWithCollections>(m => m.HasMany(x => x.BagOfFloats, "custom").AsList());
            collection = mapping.Collections.SingleOrDefault();
        };

        It should_use_the_specified_column_name_for_the_element = () =>
            collection.Element.Columns.Single().Name.ShouldEqual("custom");

        Behaves_like<HasManyListBehaviour> a_list;
        Behaves_like<HasManyElementBehaviour> an_has_many_element;

        protected static ICollectionMapping collection;
    }

    public class when_class_map_is_told_to_map_a_has_many_list_of_float_elements : ProviderSpec
    {
        Because of = () =>
        {
            var mapping = map_as_class<EntityWithCollections>(m => m.HasMany(x => x.BagOfFloats).AsList());
            collection = mapping.Collections.SingleOrDefault();
        };

        It should_use_the_default_column_name_for_the_element = () =>
            collection.Element.Columns.Single().Name.ShouldEqual("value");

        Behaves_like<HasManyListBehaviour> a_list;
        Behaves_like<HasManyElementBehaviour> an_has_many_element;

        protected static ICollectionMapping collection;
    }

    public class when_class_map_is_told_to_map_a_has_many_list_of_short_elements_with_a_custom_column : ProviderSpec
    {
        Because of = () =>
        {
            var mapping = map_as_class<EntityWithCollections>(m => m.HasMany(x => x.BagOfShorts, "custom").AsList());
            collection = mapping.Collections.SingleOrDefault();
        };

        It should_use_the_specified_column_name_for_the_element = () =>
            collection.Element.Columns.Single().Name.ShouldEqual("custom");

        Behaves_like<HasManyListBehaviour> a_list;
        Behaves_like<HasManyElementBehaviour> an_has_many_element;

        protected static ICollectionMapping collection;
    }

    public class when_class_map_is_told_to_map_a_has_many_list_of_short_elements : ProviderSpec
    {
        Because of = () =>
        {
            var mapping = map_as_class<EntityWithCollections>(m => m.HasMany(x => x.BagOfShorts).AsList());
            collection = mapping.Collections.SingleOrDefault();
        };

        It should_use_the_default_column_name_for_the_element = () =>
            collection.Element.Columns.Single().Name.ShouldEqual("value");

        Behaves_like<HasManyListBehaviour> a_list;
        Behaves_like<HasManyElementBehaviour> an_has_many_element;

        protected static ICollectionMapping collection;
    }

    public class when_class_map_is_told_to_map_a_has_many_list_of_bool_elements_with_a_custom_column : ProviderSpec
    {
        Because of = () =>
        {
            var mapping = map_as_class<EntityWithCollections>(m => m.HasMany(x => x.BagOfBools, "custom").AsList());
            collection = mapping.Collections.SingleOrDefault();
        };

        It should_use_the_specified_column_name_for_the_element = () =>
            collection.Element.Columns.Single().Name.ShouldEqual("custom");

        Behaves_like<HasManyListBehaviour> a_list;
        Behaves_like<HasManyElementBehaviour> an_has_many_element;

        protected static ICollectionMapping collection;
    }

    public class when_class_map_is_told_to_map_a_has_many_list_of_bool_elements : ProviderSpec
    {
        Because of = () =>
        {
            var mapping = map_as_class<EntityWithCollections>(m => m.HasMany(x => x.BagOfBools).AsList());
            collection = mapping.Collections.SingleOrDefault();
        };

        It should_use_the_default_column_name_for_the_element = () =>
            collection.Element.Columns.Single().Name.ShouldEqual("value");

        Behaves_like<HasManyListBehaviour> a_list;
        Behaves_like<HasManyElementBehaviour> an_has_many_element;

        protected static ICollectionMapping collection;
    }

    public class when_class_map_is_told_to_map_a_has_many_list_of_char_elements_with_a_custom_column : ProviderSpec
    {
        Because of = () =>
        {
            var mapping = map_as_class<EntityWithCollections>(m => m.HasMany(x => x.BagOfChars, "custom").AsList());
            collection = mapping.Collections.SingleOrDefault();
        };

        It should_use_the_specified_column_name_for_the_element = () =>
            collection.Element.Columns.Single().Name.ShouldEqual("custom");

        Behaves_like<HasManyListBehaviour> a_list;
        Behaves_like<HasManyElementBehaviour> an_has_many_element;

        protected static ICollectionMapping collection;
    }

    public class when_class_map_is_told_to_map_a_has_many_list_of_char_elements : ProviderSpec
    {
        Because of = () =>
        {
            var mapping = map_as_class<EntityWithCollections>(m => m.HasMany(x => x.BagOfChars).AsList());
            collection = mapping.Collections.SingleOrDefault();
        };

        It should_use_the_default_column_name_for_the_element = () =>
            collection.Element.Columns.Single().Name.ShouldEqual("value");

        Behaves_like<HasManyListBehaviour> a_list;
        Behaves_like<HasManyElementBehaviour> an_has_many_element;

        protected static ICollectionMapping collection;
    }

    public class when_class_map_is_told_to_map_a_has_many_list_of_date_time_elements_with_a_custom_column : ProviderSpec
    {
        Because of = () =>
        {
            var mapping = map_as_class<EntityWithCollections>(m => m.HasMany(x => x.BagOfDateTimes, "custom").AsList());
            collection = mapping.Collections.SingleOrDefault();
        };

        It should_use_the_specified_column_name_for_the_element = () =>
            collection.Element.Columns.Single().Name.ShouldEqual("custom");

        Behaves_like<HasManyListBehaviour> a_list;
        Behaves_like<HasManyElementBehaviour> an_has_many_element;

        protected static ICollectionMapping collection;
    }

    public class when_class_map_is_told_to_map_a_has_many_list_of_date_time_elements : ProviderSpec
    {
        Because of = () =>
        {
            var mapping = map_as_class<EntityWithCollections>(m => m.HasMany(x => x.BagOfDateTimes).AsList());
            collection = mapping.Collections.SingleOrDefault();
        };

        It should_use_the_default_column_name_for_the_element = () =>
            collection.Element.Columns.Single().Name.ShouldEqual("value");

        Behaves_like<HasManyListBehaviour> a_list;
        Behaves_like<HasManyElementBehaviour> an_has_many_element;

        protected static ICollectionMapping collection;
    }

    public class when_class_map_is_told_to_map_a_has_many_list_of_decimal_elements_with_a_custom_column : ProviderSpec
    {
        Because of = () =>
        {
            var mapping = map_as_class<EntityWithCollections>(m => m.HasMany(x => x.BagOfDecimals, "custom").AsList());
            collection = mapping.Collections.SingleOrDefault();
        };

        It should_use_the_specified_column_name_for_the_element = () =>
            collection.Element.Columns.Single().Name.ShouldEqual("custom");

        Behaves_like<HasManyListBehaviour> a_list;
        Behaves_like<HasManyElementBehaviour> an_has_many_element;

        protected static ICollectionMapping collection;
    }

    public class when_class_map_is_told_to_map_a_has_many_list_of_decimal_elements : ProviderSpec
    {
        Because of = () =>
        {
            var mapping = map_as_class<EntityWithCollections>(m => m.HasMany(x => x.BagOfDecimals).AsList());
            collection = mapping.Collections.SingleOrDefault();
        };

        It should_use_the_default_column_name_for_the_element = () =>
            collection.Element.Columns.Single().Name.ShouldEqual("value");

        Behaves_like<HasManyListBehaviour> a_list;
        Behaves_like<HasManyElementBehaviour> an_has_many_element;

        protected static ICollectionMapping collection;
    }
}
