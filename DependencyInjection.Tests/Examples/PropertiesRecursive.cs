using DependencyInjection.Tests.Infrastructure;

namespace DependencyInjection.Tests.Examples
{
    public interface IPropertiesRecursiveA
    {
        IPropertiesRecursiveB PropertyRecursiveB { get; set; }
    }

    public class PropertiesRecursiveA : IPropertiesRecursiveA
    {
        [CustomInject]
        public IPropertiesRecursiveB PropertyRecursiveB { get; set; }
    }

    public interface IPropertiesRecursiveB
    {
        IPropertiesRecursiveA PropertyRecursiveA { get; set; }
    }

    public class PropertiesRecursiveB : IPropertiesRecursiveB
    {
        [CustomInject]
        public IPropertiesRecursiveA PropertyRecursiveA { get; set; }
    }
}
