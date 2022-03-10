using DependencyInjection.Resolving;
using DependencyInjection.Tests.Examples;
using DependencyInjection.Tests.Infrastructure;
using NUnit.Framework;

namespace DependencyInjection.Tests
{
    internal class DependencyContainerTest
    {
        private IDependencyContainer _container;

        [SetUp]
        public void Setup()
        {
            _container = new DependencyContainer(new LiteResolver());
        }

        [Test]
        public void BindResolve()
        {
            _container.Bind<IDisposableDependency, DisposableDependency>();
            var instance = _container.Resolve<IDisposableDependency>();
            Assert.NotNull(instance);
        }

        [Test]
        public void BindResolve_SameClasses()
        {
            _container.Bind<DisposableDependency, DisposableDependency>();
            var instance = _container.Resolve<DisposableDependency>();
            Assert.NotNull(instance);
        }

        [Test]
        public void Dispose()
        {
            _container.Bind<IDisposableDependency, DisposableDependency>();
            var instance = _container.Resolve<IDisposableDependency>();
            Assert.NotNull(instance);
            Assert.False(instance.IsDisposed);
            _container.Dispose();
            Assert.True(instance.IsDisposed);
        }

        [Test]
        public void DisposeInner()
        {
            _container.Bind<IDisposableDependency, DisposableDependency>();
            _container.Bind<IClassWithConstructor, ClassWithConstructor>();
            var instance = _container.Resolve<IClassWithConstructor>();
            Assert.False(instance.Dependency.IsDisposed);
            _container.Dispose();
            Assert.True(instance.Dependency.IsDisposed);
        }

        [Test]
        public void DisposeMethod()
        {
            _container.Bind<IDisposableDependency>().ToMethod(_ => new DisposableDependency());
            var instance = _container.Resolve<IDisposableDependency>();
            Assert.NotNull(instance);
            Assert.False(instance.IsDisposed);
            _container.Dispose();
            Assert.True(instance.IsDisposed);
        }

        [Test]
        public void DisposeMethodInner()
        {
            _container.Bind<IDisposableDependency>().ToMethod(_ => new DisposableDependency());
            _container.Bind<IClassWithConstructor>().ToMethod(p => new ClassWithConstructor(p.Resolve<IDisposableDependency>()));
            var instance = _container.Resolve<IClassWithConstructor>();
            Assert.False(instance.Dependency.IsDisposed);
            _container.Dispose();
            Assert.True(instance.Dependency.IsDisposed);
        }

        [Test]
        public void DefaultInstance()
        {
            _container.Bind<IDisposableDependency, DisposableDependency>();
            var instance1 = _container.Resolve<IDisposableDependency>();
            var instance2 = _container.Resolve<IDisposableDependency>();
            Assert.True(instance1 != instance2);
        }

        [Test]
        public void MakeFunctionInstance()
        {
            _container.Bind<IDisposableDependency, DisposableDependency>().ToMethod(provider => new DisposableDependency());
            var instance1 = _container.Resolve<IDisposableDependency>();
            var instance2 = _container.Resolve<IDisposableDependency>();
            Assert.True(instance1 != instance2);
        }

        [Test]
        public void Singleton()
        {
            _container.Bind<IDisposableDependency, DisposableDependency>().ToSingleton();
            var instance1 = _container.Resolve<IDisposableDependency>();
            var instance2 = _container.Resolve<IDisposableDependency>();
            Assert.True(instance1 == instance2);
        }

        [Test]
        public void Resolve()
        {
            _container.Bind<IDisposableDependency, DisposableDependency>();
            _container.Bind<IClassWithConstructor, ClassWithConstructor>();
            var instance = _container.Resolve<IClassWithConstructor>();
            Assert.NotNull(instance.Dependency);
        }

        [Test]
        public void ResolveAttribute()
        {
            _container.Bind<IDependencyForAttribute, DependencyForAttribute>();
            _container.Bind<IClassForAttribute, ClassForAttribute>();
            var instance = _container.Resolve<IClassForAttribute>();
            Assert.NotNull(instance.DependencyForAttribute1);
            Assert.NotNull(instance.DependencyForAttribute2);
        }

        [Test]
        public void InitFromModule()
        {
            _container.InitFromModules(new TestInjectModule());
            var instance = _container.Resolve<IClassWithConstructor>();
            Assert.NotNull(instance);
            Assert.NotNull(instance.Dependency);
        }
    }
}
