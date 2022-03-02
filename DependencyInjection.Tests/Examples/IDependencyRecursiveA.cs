using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection.Tests.Examples
{
    public interface IDependencyRecursiveA
    {
        IDependencyRecursiveB DependencyRecursiveB { get; set; }
    }
}
