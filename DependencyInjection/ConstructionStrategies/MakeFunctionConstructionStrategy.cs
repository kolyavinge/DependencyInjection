using System;
using DependencyInjection.ConstructionMethods;

namespace DependencyInjection.ConstructionStrategies
{
    internal class MakeFunctionConstructionStrategy : ConstructionStrategy
    {
        private readonly Func<object> _makeFunction;

        public MakeFunctionConstructionStrategy(Func<object> makeFunction)
        {
            _makeFunction = makeFunction;
        }

        public override ConstructionMethod GetMethod()
        {
            return new MakeFunctionConstructionMethod(_makeFunction);
        }
    }
}
