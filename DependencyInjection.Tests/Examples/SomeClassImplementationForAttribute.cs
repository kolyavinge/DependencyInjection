using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection.Tests.Examples
{
    public class SomeClassImplementationForAttribute : ISomeClassForAttribute
    {
        [Inject]
        public IDependencyForAttribute DependencyForAttribute1 { get; set; }

        [CustomInjectAttribute]
        public IDependencyForAttribute DependencyForAttribute2 { get; set; }
    }
}
