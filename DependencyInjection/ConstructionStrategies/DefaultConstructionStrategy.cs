using System;
using System.Linq;
using DependencyInjection.ConstructionMethods;

namespace DependencyInjection.ConstructionStrategies
{
    internal class DefaultConstructionStrategy : ConstructionStrategy
    {
        public override ConstructionMethod GetMethod(Type implementationType)
        {
            return new ClassConstructorConstructionMethod(implementationType.GetConstructors().First());
        }
    }
}
