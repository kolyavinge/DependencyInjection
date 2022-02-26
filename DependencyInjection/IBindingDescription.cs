using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection
{
    public interface IBindingDescription
    {
        IBindingDescription ToSingleton();

        IBindingDescription ToMethod(Func<object> makeFunction);
    }
}
