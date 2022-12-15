using System;

namespace DependencyInjection;

public interface IBindingProvider
{
    IBindingDescription Bind<TDependency, TImplementation>();

    IBindingDescription Bind<TDependency>();

    IBindingDescription Bind(Type dependencyType, Type implementationType);

    IBindingDescription Bind(Type dependencyType);
}
