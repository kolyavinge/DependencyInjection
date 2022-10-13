namespace DependencyInjection.Tests.Examples
{
    public interface IClassWithConstructor
    {
        public IDisposableDependency Dependency { get; }
    }

    public class ClassWithConstructor : IClassWithConstructor
    {
        public IDisposableDependency Dependency { get; }

        public ClassWithConstructor(IDisposableDependency dependency)
        {
            Dependency = dependency;
        }
    }
}
