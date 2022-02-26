using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection
{
    abstract class ConstructionStrategy
    {
        public abstract ConstructionMethod GetMethod(Type implementationType);
    }
}
