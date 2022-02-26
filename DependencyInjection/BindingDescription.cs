using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection
{
    class BindingDescription
    {
        public BindingOptions Options { get; }
        public Type DependencyType { get; }
        public Type ImplementationType { get; }

        public BindingDescription(Type dependencyType, Type implementationType)
        {
            DependencyType = dependencyType;
            ImplementationType = implementationType;
            Options = new BindingOptions();
        }

        public object GetInstance()
        {
            return Options.MakeInstanceStrategy.GetInstance();
        }

        public object MakeInstance(ConstructionMethod constructionMethod)
        {
            return Options.MakeInstanceStrategy.MakeInstance(constructionMethod);
        }

        public ConstructionMethod GetConstructionMethod()
        {
            return Options.ConstructionStrategy.GetMethod(ImplementationType);
        }
    }
}
