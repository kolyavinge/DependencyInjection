using System;
using System.Collections.Generic;
using DependencyInjection.Common;

namespace DependencyInjection.Resolving;

internal abstract class Resolver
{
    private const int MaxRecursiveDepth = 1000;

    protected BindingContainer? _bindingContainer;
    protected IInvokationContext? _invokationContext;
    private Type? _resolvedType;

    public List<object> Instances { get; }

    public Resolver()
    {
        Instances = new List<object>();
    }

    public void Init(BindingContainer bindingContainer, IInvokationContext invokationContext)
    {
        _bindingContainer = bindingContainer;
        _invokationContext = invokationContext;
    }

    public object Resolve(Type dependencyType)
    {
        _resolvedType = dependencyType;
        Instances.Clear();
        BeforeResolve();

        return TryResolve(dependencyType);
    }

    protected abstract object TryResolve(Type dependencyType);

    protected virtual void BeforeResolve() { }

    private int _depth;
    protected int Depth
    {
        get => _depth;
        set
        {
            _depth++;
            if (_depth >= MaxRecursiveDepth)
            {
                throw new DependencyContainerException($"Type '{_resolvedType!}' cannot be resolved because it has recursive dependencies.");
            }
        }
    }
}
