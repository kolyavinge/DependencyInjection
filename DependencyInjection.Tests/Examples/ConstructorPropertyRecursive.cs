using DependencyInjection.Tests.Infrastructure;

namespace DependencyInjection.Tests.Examples
{
    public interface IConstructorPropertyRecursiveA
    {
        IConstructorPropertyRecursiveB PropertyRecursiveB { get; }
    }

    public class ConstructorPropertyRecursiveA : IConstructorPropertyRecursiveA
    {
        public IConstructorPropertyRecursiveB PropertyRecursiveB { get; }

        public ConstructorPropertyRecursiveA(IConstructorPropertyRecursiveB propertyRecursiveB)
        {
            PropertyRecursiveB = propertyRecursiveB;
        }
    }

    public interface IConstructorPropertyRecursiveB
    {
        IConstructorPropertyRecursiveA ConstructorRecursiveA { get; }
    }

    public class ConstructorPropertyRecursiveB : IConstructorPropertyRecursiveB
    {
        [CustomInject]
        public IConstructorPropertyRecursiveA ConstructorRecursiveA { get; set; }
    }
}
