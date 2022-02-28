using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection
{
    public interface IBindingProvider
    {
        IBindingDescription Bind<TDependency, TImplementation>();

        IBindingDescription Bind<TDependency>();
    }
}
