using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection.Tests.Examples
{
    public class DependencyRecursiveAImplementation : IDependencyRecursiveA
    {
        [CustomInject]
        public IDependencyRecursiveB DependencyRecursiveB { get; set; }
    }
}
