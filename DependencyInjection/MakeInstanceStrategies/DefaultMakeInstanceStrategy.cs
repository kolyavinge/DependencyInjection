using DependencyInjection.Common;

namespace DependencyInjection.MakeInstanceStrategies;

internal class DefaultMakeInstanceStrategy : MakeInstanceStrategy
{
    public static readonly DefaultMakeInstanceStrategy Instance = new();

    private DefaultMakeInstanceStrategy() { }

    public override object? GetInstance()
    {
        return null;
    }
}
