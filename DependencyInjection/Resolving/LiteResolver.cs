using System;
using System.Linq;

namespace DependencyInjection.Resolving
{
    internal class LiteResolver : Resolver
    {
        protected override object TryResolve(Type dependencyType)
        {
            Depth++;
            var description = _bindingContainer!.GetDescription(dependencyType);
            var instance = description.MakeInstanceStrategy.GetInstance();
            if (instance == null)
            {
                var constructionMethod = description.ConstructionStrategy.Method;
                var resolvedParams = constructionMethod.Parameters.Select(param => TryResolve(param.Type)).ToArray();
                constructionMethod.SetParameterValues(resolvedParams);
                instance = description.MakeInstanceStrategy.MakeInstance(constructionMethod, _invokationContext!);
                foreach (var prop in description.InjectedProperties)
                {
                    var resolvedProp = TryResolve(prop.PropertyType);
                    prop.SetValue(instance, resolvedProp);
                }
            }
            Instances.Add(instance);

            return instance;
        }
    }
}
