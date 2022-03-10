using DependencyInjection.Resolving;
using DependencyInjection.Tests.Examples;
using DependencyInjection.Tests.Infrastructure;
using NUnit.Framework;

namespace DependencyInjection.Tests.Resolving
{
    internal class LiteResolverTest : BaseResolverTest<LiteResolver>
    {
        [SetUp]
        public void Setup()
        {
            Init();
        }

        [Test]
        public void BindResolve()
        {
            Bind<IDisposableDependency, DisposableDependency>();
            var instance = Resolve<IDisposableDependency>();
            Assert.NotNull(instance);
            Assert.True(instance is IDisposableDependency);
            Assert.True(instance is DisposableDependency);
        }

        [Test]
        public void BindResolve_SameClasses()
        {
            Bind<DisposableDependency, DisposableDependency>();
            var instance = Resolve<DisposableDependency>();
            Assert.NotNull(instance);
            Assert.True(instance is DisposableDependency);
        }

        [Test]
        public void BindResolve_SameInterfaces()
        {
            Bind<IDisposableDependency, IDisposableDependency>();
            try
            {
                Resolve<IDisposableDependency>();
                Assert.Fail();
            }
            catch (DependencyContainerException e)
            {
                Assert.AreEqual("Type 'DependencyInjection.Tests.Examples.IDisposableDependency' cannot be constructed", e.Message);
            }
        }

        [Test]
        public void BindResolve_SameInterfaces2()
        {
            Bind<IDisposableDependency>();
            try
            {
                Resolve<IDisposableDependency>();
                Assert.Fail();
            }
            catch (DependencyContainerException e)
            {
                Assert.AreEqual("Type 'DependencyInjection.Tests.Examples.IDisposableDependency' cannot be constructed", e.Message);
            }
        }

        [Test]
        public void DefaultInstance()
        {
            Bind<IDisposableDependency, DisposableDependency>();
            var instance1 = Resolve<IDisposableDependency>();
            var instance2 = Resolve<IDisposableDependency>();
            Assert.True(instance1 != instance2);
        }

        [Test]
        public void MakeFunctionInstance()
        {
            Bind<IDisposableDependency, DisposableDependency>().ToMethod(provider => new DisposableDependency { Field = 123 });
            var instance1 = Resolve<IDisposableDependency>();
            var instance2 = Resolve<IDisposableDependency>();
            Assert.True(instance1 != instance2);
            Assert.AreEqual(123, instance1.Field);
            Assert.AreEqual(123, instance2.Field);
        }

        [Test]
        public void Singleton()
        {
            Bind<IDisposableDependency, DisposableDependency>().ToSingleton();
            var instance1 = Resolve<IDisposableDependency>();
            var instance2 = Resolve<IDisposableDependency>();
            Assert.True(instance1 == instance2);
        }

        [Test]
        public void Resolve()
        {
            Bind<IDisposableDependency, DisposableDependency>();
            Bind<IClassWithConstructor, ClassWithConstructor>();
            var instance = Resolve<IClassWithConstructor>();
            Assert.NotNull(instance.Dependency);
        }

        [Test]
        public void ResolveNoDependency_Error()
        {
            try
            {
                Bind<IClassWithConstructor, ClassWithConstructor>();
                Resolve<IClassWithConstructor>();
                Assert.Fail();
            }
            catch (DependencyContainerException exp)
            {
                Assert.AreEqual("Type 'DependencyInjection.Tests.Examples.IDisposableDependency' has not been added.", exp.Message);
            }
        }

        [Test]
        public void ResolveAttribute()
        {
            Bind<IDependencyForAttribute, DependencyForAttribute>();
            Bind<IClassForAttribute, ClassForAttribute>();
            var instance = Resolve<IClassForAttribute>();
            Assert.NotNull(instance.DependencyForAttribute1);
            Assert.NotNull(instance.DependencyForAttribute2);
        }

        [Test]
        public void RecursiveConstructors_Error()
        {
            Bind<IConstructorsRecursiveA, ConstructorsRecursiveA>();
            Bind<IConstructorsRecursiveB, ConstructorsRecursiveB>();

            try
            {
                Resolve<IConstructorsRecursiveA>();
                Assert.Fail();
            }
            catch (DependencyContainerException exp)
            {
                Assert.AreEqual("Type 'DependencyInjection.Tests.Examples.IConstructorsRecursiveA' cannot be resolved because it has recursive dependencies.", exp.Message);
            }

            try
            {
                Resolve<IConstructorsRecursiveB>();
                Assert.Fail();
            }
            catch (DependencyContainerException exp)
            {
                Assert.AreEqual("Type 'DependencyInjection.Tests.Examples.IConstructorsRecursiveB' cannot be resolved because it has recursive dependencies.", exp.Message);
            }
        }

        [Test]
        public void RecursiveConstructorAndProperty_Error()
        {
            Bind<IConstructorPropertyRecursiveA, ConstructorPropertyRecursiveA>();
            Bind<IConstructorPropertyRecursiveB, ConstructorPropertyRecursiveB>();

            try
            {
                Resolve<IConstructorPropertyRecursiveA>();
                Assert.Fail();
            }
            catch (DependencyContainerException exp)
            {
                Assert.AreEqual("Type 'DependencyInjection.Tests.Examples.IConstructorPropertyRecursiveA' cannot be resolved because it has recursive dependencies.", exp.Message);
            }

            try
            {
                Resolve<IConstructorPropertyRecursiveB>();
                Assert.Fail();
            }
            catch (DependencyContainerException exp)
            {
                Assert.AreEqual("Type 'DependencyInjection.Tests.Examples.IConstructorPropertyRecursiveB' cannot be resolved because it has recursive dependencies.", exp.Message);
            }
        }

        [Test]
        public void RecursiveProperties_Error()
        {
            Bind<IPropertiesRecursiveA, PropertiesRecursiveA>();
            Bind<IPropertiesRecursiveB, PropertiesRecursiveB>();

            try
            {
                Resolve<IPropertiesRecursiveA>();
                Assert.Fail();
            }
            catch (DependencyContainerException exp)
            {
                Assert.AreEqual("Type 'DependencyInjection.Tests.Examples.IPropertiesRecursiveA' cannot be resolved because it has recursive dependencies.", exp.Message);
            }

            try
            {
                Resolve<IPropertiesRecursiveB>();
                Assert.Fail();
            }
            catch (DependencyContainerException exp)
            {
                Assert.AreEqual("Type 'DependencyInjection.Tests.Examples.IPropertiesRecursiveB' cannot be resolved because it has recursive dependencies.", exp.Message);
            }
        }
    }
}
