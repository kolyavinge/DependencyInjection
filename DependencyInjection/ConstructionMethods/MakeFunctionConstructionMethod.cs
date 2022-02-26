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
            Parameters = new List<MethodParameter>();
        }

        public override object Invoke()
        {
            return _makeFunction();
        }
    }
}
