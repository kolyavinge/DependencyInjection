using System;
using System.Collections.Generic;
using System.Linq;

namespace DependencyInjection
{
    public class DependencyContainer : IDisposable
    {
        private readonly BindingContainer _bindingContainer;
        private readonly Resolver _resolver;
        private readonly HashSet<object> _allInstances;

        public DependencyContainer()
        {
            _bindingContainer = new BindingContainer();
            _resolver = new Resolver(_bindingContainer);
            _allInstances = new HashSet<object>();
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
            var instance = (TDependency)_resolver.Resolve(typeof(TDependency));
            _allInstances.Add(instance);

            return instance;
        }

        public void Dispose()
        {
            foreach (var disposable in _allInstances.OfType<IDisposable>())
            {
                disposable.Dispose();
            }
        }
    }
}
