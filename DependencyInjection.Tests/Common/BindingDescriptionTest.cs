using System;
using DependencyInjection.Common;
using DependencyInjection.Tests.Examples;
using NUnit.Framework;

namespace DependencyInjection.Tests.Common
{
    internal class BindingDescriptionTest
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
                new BindingDescription(null, typeof(DisposableDependency));
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
                new BindingDescription(typeof(IDisposableDependency), null);
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
            var description = new BindingDescription(typeof(IDisposableDependency), typeof(DisposableDependency));
            Assert.NotNull(description.ConstructionStrategy);
            Assert.NotNull(description.MakeInstanceStrategy);
        }
    }
}
