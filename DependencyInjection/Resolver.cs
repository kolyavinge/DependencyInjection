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
            var description = _bindingContainer.GetDescription(dependencyType);
            var instance = description.MakeInstanceStrategy.GetInstance();
            if (instance == null)
            {
                var constructionMethod = description.ConstructionStrategy.GetMethod();
                var resolvedParams = constructionMethod.Parameters.Select(param => Resolve(param.Type)).ToArray();
                Dependencies.AddRange(resolvedParams);
                constructionMethod.SetParameterValues(resolvedParams);
                instance = description.MakeInstanceStrategy.MakeInstance(constructionMethod, _invokationContext);
                foreach (var prop in instance.GetType().GetProperties().Where(p => p.GetCustomAttributes(typeof(InjectAttribute), false).Any()))
                {
                    var resolvedProp = Resolve(prop.PropertyType);
                    prop.SetValue(instance, resolvedProp);
                    Dependencies.Add(resolvedProp);
                }
            }

            return instance;
        }
    }
}
