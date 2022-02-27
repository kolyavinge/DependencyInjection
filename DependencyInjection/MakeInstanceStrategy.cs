using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection
{
    abstract class MakeInstanceStrategy
    {
        public abstract object? GetInstance();

        public virtual object MakeInstance(ConstructionMethod constructionMethod)
        {
            return constructionMethod.Invoke();
        }
    }
}
