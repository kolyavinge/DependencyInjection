using DependencyInjection.Tests.Infrastructure;

namespace DependencyInjection.Tests.Examples
{
    public interface IClassForAttribute
    {
        IDependencyForAttribute DependencyForAttribute1 { get; set; }
        IDependencyForAttribute DependencyForAttribute2 { get; set; }
    }

    public class ClassForAttribute : IClassForAttribute
    {
        [Inject]
        public IDependencyForAttribute DependencyForAttribute1 { get; set; }

        [CustomInject]
        public IDependencyForAttribute DependencyForAttribute2 { get; set; }
    }
}
