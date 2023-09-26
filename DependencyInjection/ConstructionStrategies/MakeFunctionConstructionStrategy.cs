using System;
using DependencyInjection.Common;
using DependencyInjection.ConstructionMethods;

namespace DependencyInjection.ConstructionStrategies;

internal class MakeFunctionConstructionStrategy : ConstructionStrategy
{
    private readonly ConstructionMethod _constructionMethod;

    public MakeFunctionConstructionStrategy(Func<IResolvingProvider, object> makeFunction)
    {
        if (makeFunction is null)
        {
            throw new DependencyContainerException("MakeFunction cannot be null");
        }
        _constructionMethod = new MakeFunctionConstructionMethod(makeFunction);
    }

    public override ConstructionMethod Method => _constructionMethod;
}
