using System;

namespace DependencyInjection;

public interface IDependencyContainer : IBindingProvider, IResolvingProvider, IDisposable
{
    void InitFromModules(params InjectModule[] modules);
}
