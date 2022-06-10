using System;

namespace DependencyInjection
{
    public interface IResolvingProvider
    {
        TDependency Resolve<TDependency>();

        object Resolve(Type dependencyType);
    }
}
