using System;
using System.Collections.Generic;
using System.Text;
using DependencyInjection.ConstructionStrategies;
using DependencyInjection.MakeInstanceStrategies;

namespace DependencyInjection
{
    class BindingOptions : IBindingOptions
    {
        public ConstructionStrategy ConstructionStrategy { get; set; }

        public MakeInstanceStrategy MakeInstanceStrategy { get; set; }

        public BindingOptions()
        {
            ConstructionStrategy = new DefaultConstructionStrategy();
            MakeInstanceStrategy = new DefaultMakeInstanceStrategy();
        }

        public IBindingOptions ToSingleton()
        {
            MakeInstanceStrategy = new SingletonMakeInstanceStrategy();
            return this;
        }

        public IBindingOptions ToMethod(Func<object> makeFunction)
        {
            ConstructionStrategy = new MakeFunctionConstructionStrategy(makeFunction);
            return this;
        }
    }
}
