using System;
using FluentNHibernate.Mapping;

namespace FluentNHibernate.Testing.FluentInterfaceTests
{
    public class ModelTester<TFluentClass, TModel>
    {
        private readonly Func<TFluentClass> instantiatePart;
        private readonly Func<IMappingFactory, TModel> getModel;
        private TFluentClass fluentClass;

        public ModelTester(Func<TFluentClass> instantiatePart, Func<IMappingFactory, TModel> getModel)
        {
            this.instantiatePart = instantiatePart;
            this.getModel = getModel;
        }

        public ModelTester<TFluentClass, TModel> Mapping(Action<TFluentClass> action)
        {
            fluentClass = instantiatePart();
            action(fluentClass);
            return this;
        }

        public void ModelShouldMatch(Action<TModel> action)
        {
            var model = getModel(new DefaultMappingFactory());
            action(model);
        }
    }
}