using System;
using System.Collections.Generic;
using System.Linq;

namespace DependencyInjection
{
    internal class Resolver
    {
        private readonly BindingContainer _bindingContainer;
        private readonly ConstructionMethodInvokationContext _invokationContext;

        public List<object> Dependencies { get; }

        public Resolver(BindingContainer bindingContainer, ConstructionMethodInvokationContext invokationContext)
        {
            _bindingContainer = bindingContainer;
            _invokationContext = invokationContext;
            Dependencies = new List<object>();
        }

        public object Resolve(Type dependencyType)
        {
            var instance = ResolveConstructionMethod(dependencyType);
            ResolveProperties(Dependencies.Union(new[] { instance }));

            return instance;
        }

        private object ResolveConstructionMethod(Type dependencyType)
        {
            var description = _bindingContainer.GetDescription(dependencyType);
            var instance = description.MakeInstanceStrategy.GetInstance();
            if (instance == null)
            {
                var constructionMethod = description.ConstructionStrategy.GetMethod();
                var resolvedParams = constructionMethod.Parameters.Select(param => ResolveConstructionMethod(param.Type)).ToArray();
                Dependencies.AddRange(resolvedParams);
                constructionMethod.SetParameterValues(resolvedParams);
                instance = description.MakeInstanceStrategy.MakeInstance(constructionMethod, _invokationContext);
            }

            return instance;
        }

        private void ResolveProperties(IEnumerable<object> instances)
        {
            foreach (var instance in instances)
            {
                foreach (var prop in instance.GetType().GetProperties().Where(p => p.GetCustomAttributes(typeof(InjectAttribute), false).Any()))
                {
                    var resolvedProp = ResolveConstructionMethod(prop.PropertyType);
                    prop.SetValue(instance, resolvedProp);
                    Dependencies.Add(resolvedProp);
                }
            }
        }
    }
}
