using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection.Tests.Examples
{
    public class DependencyRecursiveBImplementation : IDependencyRecursiveB
    {
        [CustomInject]
        public IDependencyRecursiveA DependencyRecursiveA { get; set; }
    }
}
