using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection.Tests.Examples
{
    public interface IDependency
    {
        public int Field { get; set; }

        public bool IsDisposed { get; set; }
    }
}
