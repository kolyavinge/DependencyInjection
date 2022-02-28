using System;
using DependencyInjection.ConstructionStrategies;
using DependencyInjection.MakeInstanceStrategies;

namespace DependencyInjection
{
    internal class BindingDescription : IBindingDescription, IInstanceBindingDescription
    {
        public Type DependencyType { get; }
        public Type ImplementationType { get; }
        public ConstructionStrategy ConstructionStrategy { get; private set; }
        public MakeInstanceStrategy MakeInstanceStrategy { get; private set; }

        public BindingDescription(Type dependencyType, Type implementationType)
        {
            DependencyType = dependencyType ?? throw new ArgumentNullException(nameof(dependencyType));
            ImplementationType = implementationType ?? throw new ArgumentNullException(nameof(implementationType));
            ConstructionStrategy = new DefaultConstructionStrategy(implementationType);
            MakeInstanceStrategy = DefaultMakeInstanceStrategy.Instance;
        }

        public IInstanceBindingDescription ToMethod(Func<IResolvingProvider, object> makeFunction)
        {
            ConstructionStrategy = new MakeFunctionConstructionStrategy(makeFunction);
            return this;
        }

        public void ToSingleton()
        {
            MakeInstanceStrategy = new SingletonMakeInstanceStrategy();
        }
    }
}
