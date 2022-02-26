using System;
using System.Linq;
using DependencyInjection.ConstructionMethods;

namespace DependencyInjection.ConstructionStrategies
{
    internal class DefaultConstructionStrategy : ConstructionStrategy
    {
        private readonly Type _implementationType;

        public DefaultConstructionStrategy(Type implementationType)
        {
            _implementationType = implementationType;
        }

        public override ConstructionMethod GetMethod()
        {
            return new ClassConstructorConstructionMethod(_implementationType.GetConstructors().First());
        }
    }
}
