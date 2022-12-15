using System;
using System.Collections.Generic;
using System.Linq;
using DependencyInjection.Common;
using DependencyInjection.Resolving;

namespace DependencyInjection;

internal class DependencyContainer : IDependencyContainer
{
    private readonly BindingContainer _bindingContainer;
    private readonly Resolver _resolver;
    private readonly HashSet<IDisposable> _disposableInstances;

    public DependencyContainer(Resolver resolver)
    {
        _bindingContainer = new BindingContainer();
        _resolver = resolver;
        _resolver.Init(_bindingContainer, new InvokationContext(this));
        _disposableInstances = new HashSet<IDisposable>();
    }

    public void InitFromModules(params InjectModule[] modules)
    {
        foreach (var module in modules) module.Init(this);
    }

    public IBindingDescription Bind<TDependency, TImplementation>()
    {
        var description = new BindingDescription(typeof(TDependency), typeof(TImplementation));
        _bindingContainer.AddDescription(description);

        return description;
    }

    public IBindingDescription Bind<TDependency>()
    {
        return Bind<TDependency, TDependency>();
    }

    public IBindingDescription Bind(Type dependencyType, Type implementationType)
    {
        var description = new BindingDescription(dependencyType, implementationType);
        _bindingContainer.AddDescription(description);

        return description;
    }

    public IBindingDescription Bind(Type dependencyType)
    {
        return Bind(dependencyType, dependencyType);
    }

    public TDependency Resolve<TDependency>()
    {
        var instance = _resolver.Resolve(typeof(TDependency));
        foreach (var inst in _resolver.Instances.OfType<IDisposable>()) _disposableInstances.Add(inst);

        return (TDependency)instance;
    }

    public object Resolve(Type dependencyType)
    {
        var instance = _resolver.Resolve(dependencyType);
        foreach (var inst in _resolver.Instances.OfType<IDisposable>()) _disposableInstances.Add(inst);

        return instance;
    }

    public void Dispose()
    {
        foreach (var disposable in _disposableInstances) disposable.Dispose();
    }
}
