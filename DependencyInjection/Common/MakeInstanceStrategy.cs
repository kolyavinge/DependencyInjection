using System;
using System.Collections.Generic;
using System.Text;
using DependencyInjection.Common;

namespace DependencyInjection.Common
{
    internal abstract class MakeInstanceStrategy
    {
        public abstract object? GetInstance();

        public virtual object MakeInstance(ConstructionMethod constructionMethod, IInvokationContext invokationContext)
        {
            return constructionMethod.Invoke(invokationContext);
        }
    }
}
