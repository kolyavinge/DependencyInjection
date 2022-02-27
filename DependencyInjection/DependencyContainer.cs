using System;
using System.Collections.Generic;
using System.Linq;

namespace DependencyInjection
{
    public class DependencyContainer : IDisposable
    {
        private readonly BindingContainer _bindingContainer;
        private readonly HashSet<IDisposable> _disposableInstances;

        public DependencyContainer()
        {
            _bindingContainer = new BindingContainer();
            _disposableInstances = new HashSet<IDisposable>();
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
            var resolver = new Resolver(_bindingContainer);
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
