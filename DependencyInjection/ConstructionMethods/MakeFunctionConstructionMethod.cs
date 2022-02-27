using System;
using System.Collections.Generic;

namespace DependencyInjection.ConstructionMethods
{
    internal class MakeFunctionConstructionMethod : ConstructionMethod
    {
        private readonly Func<object> _makeFunction;

        public MakeFunctionConstructionMethod(Func<object> makeFunction)
        {
            _makeFunction = makeFunction;
        }

        public override object Invoke()
        {
            return _makeFunction();
        }
    }
}
