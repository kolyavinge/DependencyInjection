using System;
using DependencyInjection.Tests.Examples;
using NUnit.Framework;

namespace DependencyInjection.Tests
{
    public class DependencyContainerTest
    {
        private DependencyContainer _container;

        [SetUp]
        public void Setup()
        {
            _container = new DependencyContainer();
        }

        [Test]
        public void BindResolve()
        {
            _container.Bind<IDependency, DependencyImplementation>();
            var instance = _container.Resolve<IDependency>();
            Assert.NotNull(instance);
            Assert.True(instance is IDependency);
            Assert.True(instance is DependencyImplementation);
        }

        [Test]
        public void BindResolve_SameClasses()
        {
            _container.Bind<DependencyImplementation, DependencyImplementation>();
            var instance = _container.Resolve<DependencyImplementation>();
            Assert.NotNull(instance);
            Assert.True(instance is DependencyImplementation);
        }

        [Test]
        public void BindResolve_SameInterfaces()
        {
            _container.Bind<IDependency, IDependency>();
            try
            {
                _container.Resolve<IDependency>();
                Assert.Fail();
            }
            catch (DependencyContainerException e)
            {
                Assert.AreEqual("Type 'DependencyInjection.Tests.Examples.IDependency' cannot be constructed", e.Message);
            }
        }

        [Test]
        public void BindResolve_SameInterfaces2()
        {
            _container.Bind<IDependency>();
            try
            {
                _container.Resolve<IDependency>();
                Assert.Fail();
            }
            catch (DependencyContainerException e)
            {
                Assert.AreEqual("Type 'DependencyInjection.Tests.Examples.IDependency' cannot be constructed", e.Message);
            }
        }

        [Test]
        public void Dispose()
        {
            _container.Bind<IDependency, DependencyImplementation>();
            var instance = _container.Resolve<IDependency>();
            Assert.NotNull(instance);
            Assert.False(instance.IsDisposed);
            _container.Dispose();
            Assert.True(instance.IsDisposed);
        }

        [Test]
        public void DisposeInner()
        {
            _container.Bind<IDependency, DependencyImplementation>();
            _container.Bind<ISomeClass, SomeClassImplementation>();
            var instance = _container.Resolve<ISomeClass>();
            Assert.False(instance.Dependency.IsDisposed);
            _container.Dispose();
            Assert.True(instance.Dependency.IsDisposed);
        }

        [Test]
        public void DisposeMethodNull_Error()
        {
            try
            {
                _container.Bind<IDependency>().ToMethod(null);
                Assert.Fail();
            }
            catch (DependencyContainerException e)
            {
                Assert.AreEqual("MakeFunction cannot be null", e.Message);
            }
        }

        [Test]
        public void DisposeMethod()
        {
            _container.Bind<IDependency>().ToMethod(_ => new DependencyImplementation());
            var instance = _container.Resolve<IDependency>();
            Assert.NotNull(instance);
            Assert.False(instance.IsDisposed);
            _container.Dispose();
            Assert.True(instance.IsDisposed);
        }

        [Test]
        public void DisposeMethodInner()
        {
            _container.Bind<IDependency>().ToMethod(_ => new DependencyImplementation());
            _container.Bind<ISomeClass>().ToMethod(p => new SomeClassImplementation(p.Resolve<IDependency>()));
            var instance = _container.Resolve<ISomeClass>();
            Assert.False(instance.Dependency.IsDisposed);
            _container.Dispose();
            Assert.True(instance.Dependency.IsDisposed);
        }

        [Test]
        public void DefaultInstance()
        {
            _container.Bind<IDependency, DependencyImplementation>();
            var instance1 = _container.Resolve<IDependency>();
            var instance2 = _container.Resolve<IDependency>();
            Assert.True(instance1 != instance2);
        }

        [Test]
        public void MakeFunctionInstance()
        {
            _container.Bind<IDependency, DependencyImplementation>().ToMethod(provider => new DependencyImplementation { Field = 123 });
            var instance1 = _container.Resolve<IDependency>();
            var instance2 = _container.Resolve<IDependency>();
            Assert.True(instance1 != instance2);
            Assert.AreEqual(123, instance1.Field);
            Assert.AreEqual(123, instance2.Field);
        }

        [Test]
        public void Singleton()
        {
            _container.Bind<IDependency, DependencyImplementation>().ToSingleton();
            var instance1 = _container.Resolve<IDependency>();
            var instance2 = _container.Resolve<IDependency>();
            Assert.True(instance1 == instance2);
        }

        [Test]
        public void Resolve()
        {
            _container.Bind<IDependency, DependencyImplementation>();
            _container.Bind<ISomeClass, SomeClassImplementation>();
            var instance = _container.Resolve<ISomeClass>();
            Assert.NotNull(instance.Dependency);
        }

        [Test]
        public void ResolveNoDependency_Error()
        {
            try
            {
                _container.Bind<ISomeClass, SomeClassImplementation>();
                _container.Resolve<ISomeClass>();
                Assert.Fail();
            }
            catch (DependencyContainerException exp)
            {
                Assert.AreEqual("Type 'DependencyInjection.Tests.Examples.IDependency' has not been added.", exp.Message);
            }
        }

        [Test]
        public void ResolveAttribute()
        {
            _container.Bind<IDependencyForAttribute, DependencyImplementationForAttribute>();
            _container.Bind<ISomeClassForAttribute, SomeClassImplementationForAttribute>();
            var instance = _container.Resolve<ISomeClassForAttribute>();
            Assert.NotNull(instance.DependencyForAttribute1);
            Assert.NotNull(instance.DependencyForAttribute2);
        }

        [Test]
        public void InitFromModule()
        {
            _container.InitFromModules(new TestInjectModule());
            var instance = _container.Resolve<ISomeClass>();
            Assert.NotNull(instance);
            Assert.NotNull(instance.Dependency);
        }
    }
}
