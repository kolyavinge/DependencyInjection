using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection.Tests.Examples
{
    public interface ISomeClassForAttribute
    {
        IDependencyForAttribute DependencyForAttribute1 { get; set; }
        IDependencyForAttribute DependencyForAttribute2 { get; set; }
    }
}
