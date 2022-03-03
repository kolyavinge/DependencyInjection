using System;
using System.Collections.Generic;
using System.Text;
using DependencyInjection.Common;

namespace DependencyInjection.MakeInstanceStrategies
{
    internal class SingletonMakeInstanceStrategy : MakeInstanceStrategy
    {
        private object? _instance;

        public override object? GetInstance()
        {
            return _instance;
        }

        public override object MakeInstance(ConstructionMethod constructionMethod, InvokationContext invokationContext)
        {
            _instance = base.MakeInstance(constructionMethod, invokationContext);
            return _instance;
        }
    }
}
