using System;
using System.Linq;
using DependencyInjection.Common;
using DependencyInjection.ConstructionMethods;

namespace DependencyInjection.ConstructionStrategies
{
    internal class DefaultConstructionStrategy : ConstructionStrategy
    {
        private readonly ConstructionMethod _constructionMethod;

        public DefaultConstructionStrategy(Type implementationType)
        {
            if (implementationType.IsInterface)
            {
                throw new DependencyContainerException($"Type '{implementationType}' cannot be constructed");
            }
            _constructionMethod = new ClassConstructorConstructionMethod(implementationType.GetConstructors().First());
        }

        public override ConstructionMethod Method => _constructionMethod;
    }
}
