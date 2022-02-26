using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection.Tests.Examples
{
    public class DependencyImplementation : IDependency, IDisposable
    {
        public int Field { get; set; }

        public bool IsDisposed { get; set; }

        public void Dispose()
        {
            IsDisposed = true;
        }
    }
}
