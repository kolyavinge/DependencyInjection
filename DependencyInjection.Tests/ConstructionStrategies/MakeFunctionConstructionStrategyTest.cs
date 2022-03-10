using DependencyInjection.ConstructionStrategies;
using NUnit.Framework;

namespace DependencyInjection.Tests.ConstructionStrategies
{
    internal class MakeFunctionConstructionStrategyTest
    {
        [Test]
        public void MethodNull_Error()
        {
            try
            {
                new MakeFunctionConstructionStrategy(null);
                Assert.Fail();
            }
            catch (DependencyContainerException e)
            {
                Assert.AreEqual("MakeFunction cannot be null", e.Message);
            }
        }
    }
}
