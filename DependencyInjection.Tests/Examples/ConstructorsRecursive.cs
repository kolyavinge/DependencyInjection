namespace DependencyInjection.Tests.Examples
{
    public interface IConstructorsRecursiveA
    {
    }

    public class ConstructorsRecursiveA : IConstructorsRecursiveA
    {
        public ConstructorsRecursiveA(IConstructorsRecursiveB recursiveB)
        {
        }
    }

    public interface IConstructorsRecursiveB
    {
    }

    public class ConstructorsRecursiveB : IConstructorsRecursiveB
    {
        public ConstructorsRecursiveB(IConstructorsRecursiveA recursiveA)
        {
        }
    }
}
