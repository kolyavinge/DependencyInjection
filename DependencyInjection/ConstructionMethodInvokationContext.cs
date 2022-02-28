using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection
{
    internal class ConstructionMethodInvokationContext
    {
        public IResolvingProvider ResolvingProvider { get; }

        public ConstructionMethodInvokationContext(IResolvingProvider resolvingProvider)
        {
            ResolvingProvider = resolvingProvider;
        }
    }
}
