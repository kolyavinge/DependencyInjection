using DependencyInjection.Common;
using DependencyInjection.Resolving;

namespace DependencyInjection
{
    public static class DependencyContainerFactory
    {
        public static IDependencyContainer MakeLiteContainer()
        {
            return new DependencyContainer(new LiteResolver());
        }

        public static IDependencyContainer MakeRecursiveContainer()
        {
            return new DependencyContainer(new RecursiveResolver());
        }
    }
}
