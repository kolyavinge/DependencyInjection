using System;
using System.Linq;

namespace DependencyInjection
{
    internal class Resolver
    {
        private BindingContainer _bindingContainer;

        public Resolver(BindingContainer bindingContainer)
        {
            _bindingContainer = bindingContainer;
        }

        public object Resolve(Type dependencyType)
        {
            var description = _bindingContainer.GetDescription(dependencyType);
            var instance = description.MakeInstanceStrategy.GetInstance();
            if (instance == null)
            {
                var constructionMethod = description.ConstructionStrategy.GetMethod();
                var resolvedParams = constructionMethod.Parameters.Select(param => Resolve(param.Type)).ToArray();
                constructionMethod.SetParameterValues(resolvedParams);
                instance = description.MakeInstanceStrategy.MakeInstance(constructionMethod);
                foreach (var prop in instance.GetType().GetProperties().Where(p => p.GetCustomAttributes(typeof(InjectAttribute), false).Any()))
                {
                    var propValue = Resolve(prop.PropertyType);
                    prop.SetValue(instance, propValue);
                }
            }

            return instance;
        }
    }
}
