using System;
using DependencyInjection.ConstructionStrategies;
using DependencyInjection.MakeInstanceStrategies;

namespace DependencyInjection.Common
{
    internal class BindingDescription : IBindingDescription, IInstanceBindingDescription
    {
        private ConstructionStrategy? _constructionStrategy;
        private MakeInstanceStrategy? _makeInstanceStrategy;

        public Type DependencyType { get; }

        public Type ImplementationType { get; }

        public ConstructionStrategy ConstructionStrategy => _constructionStrategy ??= new DefaultConstructionStrategy(ImplementationType);

        public MakeInstanceStrategy MakeInstanceStrategy => _makeInstanceStrategy ??= DefaultMakeInstanceStrategy.Instance;

        public BindingDescription(Type dependencyType, Type implementationType)
        {
            DependencyType = dependencyType ?? throw new ArgumentNullException(nameof(dependencyType));
            ImplementationType = implementationType ?? throw new ArgumentNullException(nameof(implementationType));
        }

        public IInstanceBindingDescription ToMethod(Func<IResolvingProvider, object> makeFunction)
        {
            _constructionStrategy = new MakeFunctionConstructionStrategy(makeFunction);
            return this;
        }

        public void ToSingleton()
        {
            _makeInstanceStrategy = new SingletonMakeInstanceStrategy();
        }
    }
}
