using System;

namespace DependencyInjection.Tests.Examples
{
    public interface IDisposableDependency : IDisposable
    {
        public int Field { get; set; }

        public bool IsDisposed { get; set; }
    }

    public class DisposableDependency : IDisposableDependency
    {
        public int Field { get; set; }

        public bool IsDisposed { get; set; }

        public void Dispose()
        {
            IsDisposed = true;
        }
    }
}
