using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection.Tests.Examples
{
    public interface IDependencyRecursiveB
    {
        IDependencyRecursiveA DependencyRecursiveA { get; set; }
    }
}
