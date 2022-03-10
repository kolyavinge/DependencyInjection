using DependencyInjection.Tests.Examples;

namespace DependencyInjection.Tests.Infrastructure
{
    public class TestInjectModule : InjectModule
    {
        public override void Init(IBindingProvider bindingProvider)
        {
            bindingProvider.Bind<IDisposableDependency, DisposableDependency>();
            bindingProvider.Bind<IClassWithConstructor, ClassWithConstructor>();
        }
    }
}
