using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection.Common
{
    internal class InvokationContext
    {
        public IResolvingProvider ResolvingProvider { get; }

        public InvokationContext(IResolvingProvider resolvingProvider)
        {
            ResolvingProvider = resolvingProvider;
        }
    }
}
