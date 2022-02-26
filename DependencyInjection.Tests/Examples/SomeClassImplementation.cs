using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection.Tests.Examples
{
    public class SomeClassImplementation : ISomeClass
    {
        public IDependency Dependency { get; set; }

        public SomeClassImplementation(IDependency dependency)
        {
            Dependency = dependency;
        }
    }
}
