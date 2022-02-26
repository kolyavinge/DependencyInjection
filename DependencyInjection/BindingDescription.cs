using System;
using DependencyInjection.ConstructionStrategies;
using DependencyInjection.MakeInstanceStrategies;

namespace DependencyInjection
{
    class BindingDescription : IBindingDescription
    {
        public Type DependencyType { get; }
        public Type ImplementationType { get; }
        public ConstructionStrategy ConstructionStrategy { get; private set; }
        public MakeInstanceStrategy MakeInstanceStrategy { get; private set; }

        public BindingDescription(Type dependencyType, Type implementationType)
        {
            DependencyType = dependencyType;
            ImplementationType = implementationType;
            ConstructionStrategy = new DefaultConstructionStrategy(implementationType);
            MakeInstanceStrategy = DefaultMakeInstanceStrategy.Instance;
        }

        public IBindingDescription ToSingleton()
        {
            MakeInstanceStrategy = new SingletonMakeInstanceStrategy();
            return this;
        }

        public IBindingDescription ToMethod(Func<object> makeFunction)
        {
            ConstructionStrategy = new MakeFunctionConstructionStrategy(makeFunction);
            return this;
        }
    }
}
