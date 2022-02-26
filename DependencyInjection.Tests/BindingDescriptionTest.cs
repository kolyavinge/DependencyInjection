using System;
using DependencyInjection.Tests.Examples;
using NUnit.Framework;

namespace DependencyInjection.Tests
{
    public class BindingDescriptionTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void DependencyTypeNull_Error()
        {
            try
            {
                new BindingDescription(null, typeof(DependencyImplementation));
                Assert.Fail();
            }
            catch (ArgumentNullException exp)
            {
                Assert.AreEqual("Value cannot be null. (Parameter 'dependencyType')", exp.Message);
            }
        }

        [Test]
        public void ImplementationTypeNull_Error()
        {
            try
            {
                new BindingDescription(typeof(IDependency), null);
                Assert.Fail();
            }
            catch (ArgumentNullException exp)
            {
                Assert.AreEqual("Value cannot be null. (Parameter 'implementationType')", exp.Message);
            }
        }

        [Test]
        public void Constructor()
        {
            var description = new BindingDescription(typeof(IDependency), typeof(DependencyImplementation));
            Assert.NotNull(description.ConstructionStrategy);
            Assert.NotNull(description.MakeInstanceStrategy);
        }
    }
}
