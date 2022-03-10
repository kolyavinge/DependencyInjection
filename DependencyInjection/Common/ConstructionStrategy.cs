using System;
using System.Collections.Generic;
using System.Text;
using DependencyInjection.Common;

namespace DependencyInjection.Common
{
    internal abstract class ConstructionStrategy
    {
        public abstract ConstructionMethod Method { get; }
    }
}
