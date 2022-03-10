using System;
using System.Linq;

namespace DependencyInjection.Resolving
{
    internal class LiteResolver : Resolver
    {
        private const int MaxRecursiveDepth = 1000;

        public override object Resolve(Type dependencyType)
        {
            try
            {
                Instances.Clear();
                return TryResolve(dependencyType, 0);
            }
            catch (StackOverflowException)
            {
                throw new DependencyContainerException($"Type '{dependencyType}' cannot be resolved because it has recursive dependencies.");
            }
        }

        private object TryResolve(Type dependencyType, int depth)
        {
            if (depth >= MaxRecursiveDepth) throw new StackOverflowException();
            var description = _bindingContainer!.GetDescription(dependencyType);
            var instance = description.MakeInstanceStrategy.GetInstance();
            if (instance == null)
            {
                var constructionMethod = description.ConstructionStrategy.Method;
                var resolvedParams = constructionMethod.Parameters.Select(param => TryResolve(param.Type, depth + 1)).ToArray();
                constructionMethod.SetParameterValues(resolvedParams);
                instance = description.MakeInstanceStrategy.MakeInstance(constructionMethod, _invokationContext!);
                foreach (var prop in description.InjectedProperties)
                {
                    var resolvedProp = TryResolve(prop.PropertyType, depth + 1);
                    prop.SetValue(instance, resolvedProp);
                }
            }

            Instances.Add(instance);

            return instance;
        }
    }
}
