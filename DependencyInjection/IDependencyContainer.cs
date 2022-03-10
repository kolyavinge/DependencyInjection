using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection
{
    public interface IDependencyContainer : IBindingProvider, IResolvingProvider, IDisposable
    {
        void InitFromModules(params InjectModule[] modules);
    }
}
