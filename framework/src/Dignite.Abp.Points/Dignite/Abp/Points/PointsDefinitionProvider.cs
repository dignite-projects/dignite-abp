using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.Points;

public abstract class PointsDefinitionProvider : IPointsDefinitionProvider, ITransientDependency
{
    public abstract void Define(IPointsDefinitionContext context);
}