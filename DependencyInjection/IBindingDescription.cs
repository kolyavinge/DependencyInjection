using System;

namespace DependencyInjection;

public interface IBindingDescription
{
    IInstanceBindingDescription ToMethod(Func<IResolvingProvider, object> makeFunction);

    void ToSingleton();
}

public interface IInstanceBindingDescription
{
    void ToSingleton();
}
