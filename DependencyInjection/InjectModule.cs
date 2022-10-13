namespace DependencyInjection;

public abstract class InjectModule
{
    public abstract void Init(IBindingProvider bindingProvider);
}
