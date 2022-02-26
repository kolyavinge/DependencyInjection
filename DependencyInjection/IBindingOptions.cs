using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection
{
    public interface IBindingOptions
    {
        IBindingOptions ToSingleton();

        IBindingOptions ToMethod(Func<object> makeFunction);
    }
}
