using System;
using System.Collections.Generic;

namespace DependencyInjection.ConstructionMethods
{
    internal class MakeFunctionConstructionMethod : ConstructionMethod
    {
        private readonly Func<IResolvingProvider, object> _makeFunction;

        public MakeFunctionConstructionMethod(Func<IResolvingProvider, object> makeFunction)
        {
            _makeFunction = makeFunction;
        }

        public override object Invoke(ConstructionMethodInvokationContext context)
        {
            return _makeFunction(context.ResolvingProvider);
        }
    }
}
