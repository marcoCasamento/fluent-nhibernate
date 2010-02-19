using System;
using System.Linq;
using FluentNHibernate.Mapping;
using FluentNHibernate.Mapping.Providers;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.Specs.Automapping.Fixtures;
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

    public class when_has_many_element_set_builder_is_told_to_set_the_property_access : HasManyElementSetBuilderSpec
    {
        Because of = () =>
            set(x => x.Access.Field());

        It should_set_the_access_property_on_the_mapping = () =>
            mapping.Access.ShouldEqual("field");
    }

    public class when_has_many_element_set_builder_is_told_to_set_the_batch_size : HasManyElementSetBuilderSpec
    {
        Because of = () =>
            set(x => x.BatchSize(10));

        It should_set_the_batch_size_property_on_the_mapping = () =>
            mapping.BatchSize.ShouldEqual(10);
    }

    public class when_has_many_element_set_builder_is_told_to_set_the_cascade : HasManyElementSetBuilderSpec
    {
        Because of = () =>
            set(x => x.Cascade.All());

        It should_set_the_cascade_property_on_the_mapping = () =>
            mapping.Cascade.ShouldEqual("all");
    }

    public class when_has_many_element_set_builder_is_told_to_set_the_check_constraint : HasManyElementSetBuilderSpec
    {
        Because of = () =>
            set(x => x.Check("constraint"));

        It should_set_the_check_property_on_the_mapping = () =>
            mapping.Check.ShouldEqual("constraint");
    }

    public class when_has_many_element_set_builder_is_told_to_set_the_collection_type : HasManyElementSetBuilderSpec
    {
        Because of = () =>
            set(x => x.CollectionType("type"));

        It should_set_the_collection_type_property_on_the_mapping = () =>
            mapping.CollectionType.ShouldEqual(new TypeReference("type"));
    }

    public class when_has_many_element_set_builder_is_told_to_set_fetch : HasManyElementSetBuilderSpec
    {
        Because of = () =>
            set(x => x.Fetch.Select());

        It should_set_the_fetch_property_on_the_mapping = () =>
            mapping.Fetch.ShouldEqual("select");
    }

    public class when_has_many_element_set_builder_is_told_to_set_inverse : HasManyElementSetBuilderSpec
    {
        Because of = () =>
            set(x => x.Inverse());

        It should_set_the_inverse_property_on_the_mapping = () =>
            mapping.Inverse.ShouldBeTrue();
    }

    public class when_has_many_element_set_builder_is_told_to_set_lazy_load : HasManyElementSetBuilderSpec
    {
        Because of = () =>
            set(x => x.LazyLoad());

        It should_set_the_lazy_property_on_the_mapping = () =>
            mapping.Lazy.ShouldBeTrue();
    }

    public class when_has_many_element_set_builder_is_told_to_set_read_only : HasManyElementSetBuilderSpec
    {
        Because of = () =>
            set(x => x.Not.ReadOnly());

        It should_set_the_mutable_property_on_the_mapping = () =>
            mapping.Mutable.ShouldBeTrue();
    }

    public class when_has_many_element_set_builder_is_told_to_set_optimistic_lock : HasManyElementSetBuilderSpec
    {
        Because of = () =>
            set(x => x.OptimisticLock.None());

        It should_set_the_optimistic_lock_property_on_the_mapping = () =>
            mapping.OptimisticLock.ShouldEqual("none");
    }

    public class when_has_many_element_set_builder_is_told_to_set_order_by : HasManyElementSetBuilderSpec
    {
        Because of = () =>
            set(x => x.OrderBy("asc"));

        It should_set_the_order_by_property_on_the_mapping = () =>
            mapping.OrderBy.ShouldEqual("asc");
    }

    public class when_has_many_element_set_builder_is_told_to_set_the_persister : HasManyElementSetBuilderSpec
    {
        Because of = () =>
            set(x => x.Persister("type"));

        It should_set_the_persister_property_on_the_mapping = () =>
            mapping.Persister.ShouldEqual(new TypeReference("type"));
    }

    public class when_has_many_element_set_builder_is_told_to_set_the_schema : HasManyElementSetBuilderSpec
    {
        Because of = () =>
            set(x => x.Schema("dbo"));

        It should_set_the_schema_property_on_the_mapping = () =>
            mapping.Schema.ShouldEqual("dbo");
    }

    public class when_has_many_element_set_builder_is_told_to_set_subselect : HasManyElementSetBuilderSpec
    {
        Because of = () =>
            set(x => x.Subselect("boo"));

        It should_set_the_subselect_property_on_the_mapping = () =>
            mapping.Subselect.ShouldEqual("boo");
    }

    public class when_has_many_element_set_builder_is_told_to_set_the_table : HasManyElementSetBuilderSpec
    {
        Because of = () =>
            set(x => x.Table("table"));

        It should_set_the_table_name_property_on_the_mapping = () =>
            mapping.TableName.ShouldEqual("table");
    }

    public class when_has_many_element_set_builder_is_told_to_set_the_where_clause : HasManyElementSetBuilderSpec
    {
        Because of = () =>
            set(x => x.Where("x = 1"));

        It should_set_the_where_property_on_the_mapping = () =>
            mapping.Where.ShouldEqual("x = 1");
    }

    public class when_has_many_element_set_builder_is_told_to_set_the_sort : HasManyElementSetBuilderSpec
    {
        Because of = () =>
            set(x => x.Sort("x = 1"));

        It should_set_the_where_property_on_the_mapping = () =>
            mapping.As<SetMapping>().Sort.ShouldEqual("x = 1");
    }

    public abstract class HasManyElementSetBuilderSpec
    {
        Establish context = () =>
        {
            var factory = new HasManyBuilderFactory(FakeMembers.Type, x => collection = x);
            builder = factory.has_many_elements_in_a_set<string>(FakeMembers.IListOfStrings);
        };

        protected static void set(Action<HasManyElementSetBuilder<string>> alter)
        {
            alter(builder);
            mapping = collection.GetCollectionMapping();
        }

        static ICollectionMappingProvider collection;
        static HasManyElementSetBuilder<string> builder;
        protected static ICollectionMapping mapping;
    }
}
