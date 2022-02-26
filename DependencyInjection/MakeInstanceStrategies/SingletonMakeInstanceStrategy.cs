using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection.MakeInstanceStrategies
{
    internal class SingletonMakeInstanceStrategy : MakeInstanceStrategy
    {
        private object _instance;

        public override object GetInstance()
        {
            return _instance;
        }

        public override object MakeInstance(ConstructionMethod constructionMethod)
        {
            _instance = base.MakeInstance(constructionMethod);
            return _instance;
        }
    }
}
