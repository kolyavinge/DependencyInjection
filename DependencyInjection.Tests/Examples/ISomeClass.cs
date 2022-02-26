using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection.Tests.Examples
{
    public interface ISomeClass
    {
        public IDependency Dependency { get; set; }
    }
}
