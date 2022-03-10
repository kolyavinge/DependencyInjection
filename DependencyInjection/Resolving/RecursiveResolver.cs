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

        public override object Resolve(Type dependencyType)
        {
            Instances.Clear();
            _recursiveProperties.Clear();

            return TryResolve(dependencyType);
        }

        private object TryResolve(Type dependencyType)
        {
            var description = _bindingContainer!.GetDescription(dependencyType);
            var instance = description.MakeInstanceStrategy.GetInstance();
            if (instance == null)
            {
                _resolvingChain.Add(dependencyType);
                var constructionMethod = description.ConstructionStrategy.Method;
                constructionMethod.Parameters.Each(param => ErrorIfCycledByConstructionMethods(param.Type, dependencyType));
                var resolvedParams = constructionMethod.Parameters.Select(param => TryResolve(param.Type)).ToArray();
                constructionMethod.SetParameterValues(resolvedParams);
                instance = description.MakeInstanceStrategy.MakeInstance(constructionMethod, _invokationContext!);
                foreach (var prop in description.InjectedProperties)
                {
                    var isCycled = IsCycled(prop.PropertyType, dependencyType);
                    if (isCycled && _resolvingChain.Contains(prop.PropertyType))
                    {
                        _recursiveProperties.Add(new RecursiveResolvedProperty(prop, instance));
                    }
                    else if (!isCycled || !_recursiveProperties.Any(x => x.PropertyType == prop.PropertyType))
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

        private bool IsCycled(Type fromType, Type toType)
        {
            var dependencyStack = new Stack<Type>();
            dependencyStack.Push(fromType);
            while (dependencyStack.Any())
            {
                var type = dependencyStack.Pop();
                if (type == toType) return true;
                var description = _bindingContainer!.GetDescription(type);
                description.ConstructionStrategy.Method.Parameters.Each(param => dependencyStack.Push(param.Type));
                description.InjectedProperties.Each(prop => dependencyStack.Push(prop.PropertyType));
            }

            return false;
        }

        private bool IsCycledByConstructionMethods(Type fromType, Type toType)
        {
            var dependencyStack = new Stack<Type>();
            dependencyStack.Push(fromType);
            while (dependencyStack.Any())
            {
                var type = dependencyStack.Pop();
                if (type == toType) return true;
                var description = _bindingContainer!.GetDescription(type);
                description.ConstructionStrategy.Method.Parameters.Each(param => dependencyStack.Push(param.Type));
            }

            return false;
        }

        private void ErrorIfCycledByConstructionMethods(Type fromType, Type toType)
        {
            if (IsCycledByConstructionMethods(fromType, toType))
            {
                throw new DependencyContainerException($"Type '{toType}' cannot be resolved because it has recursive construction dependencies.");
            }
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
