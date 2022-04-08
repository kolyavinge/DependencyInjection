using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DependencyInjection.Utils;

namespace DependencyInjection.Resolving
{
    internal class RecursiveResolver : Resolver
    {
        private readonly List<RecursiveResolvedProperty> _recursiveProperties = new();
        private readonly HashSet<Type> _resolvingChain = new();

        protected override void BeforeResolve()
        {
            _recursiveProperties.Clear();
            _resolvingChain.Clear();
        }

        protected override object TryResolve(Type dependencyType)
        {
            Depth++;
            var description = _bindingContainer!.GetDescription(dependencyType);
            var instance = description.MakeInstanceStrategy.GetInstance();
            if (instance == null)
            {
                _resolvingChain.Add(dependencyType);
                var constructionMethod = description.ConstructionStrategy.Method;
                var resolvedParams = constructionMethod.Parameters.Select(param => TryResolve(param.Type)).ToArray();
                constructionMethod.SetParameterValues(resolvedParams);
                instance = description.MakeInstanceStrategy.MakeInstance(constructionMethod, _invokationContext!);
                foreach (var prop in description.InjectedProperties)
                {
                    if (_resolvingChain.Contains(prop.PropertyType))
                    {
                        _recursiveProperties.Add(new RecursiveResolvedProperty(prop, instance));
                    }
                    else
                    {
                        var resolvedProp = TryResolve(prop.PropertyType);
                        prop.SetValue(instance, resolvedProp);
                    }
                }
                _resolvingChain.Remove(dependencyType);
            }
            _recursiveProperties.Where(prop => prop.PropertyType == dependencyType).Each(prop => prop.SetFunc(instance));
            Instances.Add(instance);

            return instance;
        }
    }

    class RecursiveResolvedProperty
    {
        public Type PropertyType { get; }
        public Action<object> SetFunc { get; }

        public RecursiveResolvedProperty(PropertyInfo prop, object instance)
        {
            PropertyType = prop.PropertyType;
            SetFunc = resolvedProp => prop.SetValue(instance, resolvedProp);
        }
    }
}
