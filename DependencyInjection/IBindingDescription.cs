using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection
{
    public interface IBindingDescription
    {
        IInstanceBindingDescription ToMethod(Func<object> makeFunction);

        void ToSingleton();
    }

    public interface IInstanceBindingDescription
    {
        void ToSingleton();
    }
}
