namespace DependencyInjection;

public interface IBindingProvider
{
    IBindingDescription Bind<TDependency, TImplementation>();

    IBindingDescription Bind<TDependency>();
}
