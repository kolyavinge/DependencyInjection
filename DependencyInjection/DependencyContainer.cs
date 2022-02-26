using System;
using System.Collections.Generic;
using System.Linq;

namespace DependencyInjection
{
    public class DependencyContainer : IDisposable
    {
        private readonly BindingContainer _bindingContainer;
        private readonly Resolver _resolver;
        private readonly List<object> _allInstances;

        public DependencyContainer()
        {
            _bindingContainer = new BindingContainer();
            _resolver = new Resolver(_bindingContainer);
            _allInstances = new List<object>();
        }

        public IBindingDescription Bind<TDependency, TImplementation>()
        {
            var binding = new BindingDescription(typeof(TDependency), typeof(TImplementation));
            _bindingContainer.Add(binding);

            return binding;
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
