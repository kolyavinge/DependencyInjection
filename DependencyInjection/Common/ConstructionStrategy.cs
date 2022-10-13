namespace DependencyInjection.Common;

internal abstract class ConstructionStrategy
{
    public abstract ConstructionMethod Method { get; }
}
