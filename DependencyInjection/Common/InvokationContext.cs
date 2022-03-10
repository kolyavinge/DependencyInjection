using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection.Common
{
    internal interface IInvokationContext
    {
        IResolvingProvider ResolvingProvider { get; }
    }

    internal class InvokationContext : IInvokationContext
    {
        public IResolvingProvider ResolvingProvider { get; }

        public InvokationContext(IResolvingProvider resolvingProvider)
        {
            ResolvingProvider = resolvingProvider;
        }
    }
}
