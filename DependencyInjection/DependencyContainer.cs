using System;
using System.Collections.Generic;
using System.Linq;
using DependencyInjection.Common;

namespace DependencyInjection
{
    public class DependencyContainer : IBindingProvider, IResolvingProvider, IDisposable
    {
        private readonly BindingContainer _bindingContainer;
        private readonly HashSet<IDisposable> _disposableInstances;

        public DependencyContainer()
        {
            _bindingContainer = new BindingContainer();
            _disposableInstances = new HashSet<IDisposable>();
        }

        public void InitFromModules(params InjectModule[] modules)
        {
            foreach (var module in modules) module.Init(this);
        }

        public IBindingDescription Bind<TDependency, TImplementation>()
        {
            var description = new BindingDescription(typeof(TDependency), typeof(TImplementation));
            _bindingContainer.AddDescription(description);

            return description;
        }

        public IBindingDescription Bind<TDependency>()
        {
            return Bind<TDependency, TDependency>();
        }

        public TDependency Resolve<TDependency>()
        {
            var resolver = new Resolver(_bindingContainer, new InvokationContext(this));
            var instance = resolver.Resolve(typeof(TDependency));
            if (instance is IDisposable disposable) _disposableInstances.Add(disposable);
            foreach (var d in resolver.Dependencies.OfType<IDisposable>()) _disposableInstances.Add(d);

            return (TDependency)instance;
        }

        public void Dispose()
        {
            foreach (var disposable in _disposableInstances) disposable.Dispose();
        }
    }
}
