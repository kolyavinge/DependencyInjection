using System;
using DependencyInjection.Tests.Examples;
using NUnit.Framework;

namespace DependencyInjection.Tests
{
    public class BindingContainerTest
    {
        private BindingContainer _container;

        [SetUp]
        public void Setup()
        {
            _container = new BindingContainer();
        }

        [Test]
        public void AddNull_Error()
        {
            try
            {
                _container.AddDescription(null);
                Assert.Fail();
            }
            catch (ArgumentNullException exp)
            {
                Assert.AreEqual("Value cannot be null. (Parameter 'binding')", exp.Message);
            }
        }

        [Test]
        public void GetNull_Error()
        {
            try
            {
                _container.GetDescription(null);
                Assert.Fail();
            }
            catch (ArgumentNullException exp)
            {
                Assert.AreEqual("Value cannot be null. (Parameter 'dependencyType')", exp.Message);
            }
        }

        [Test]
        public void GetNotAdded_Error()
        {
            try
            {
                _container.GetDescription(typeof(IDependency));
                Assert.Fail();
            }
            catch (DependencyContainerException exp)
            {
                Assert.AreEqual("Type 'DependencyInjection.Tests.Examples.IDependency' has not been added.", exp.Message);
            }
        }

        [Test]
        public void AddGet_NotNull()
        {
            _container.AddDescription(new BindingDescription(typeof(IDependency), typeof(DependencyImplementation)));
            var obj = _container.GetDescription(typeof(IDependency));
            Assert.NotNull(obj);
        }

        [Test]
        public void AddTwice_Error()
        {
            _container.AddDescription(new BindingDescription(typeof(IDependency), typeof(DependencyImplementation)));
            try
            {
                _container.AddDescription(new BindingDescription(typeof(IDependency), typeof(DependencyImplementation)));
                Assert.Fail();
            }
            catch (DependencyContainerException exp)
            {
                Assert.AreEqual("Type 'DependencyInjection.Tests.Examples.IDependency' has already been added.", exp.Message);
            }
        }
    }
}
