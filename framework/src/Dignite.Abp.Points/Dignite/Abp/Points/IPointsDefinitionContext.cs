using System.Collections.Generic;

namespace Dignite.Abp.Points;

/// <summary>
/// Used as a context while defining points.
/// </summary>
public interface IPointsDefinitionContext
{
    PointsDefinition GetOrNull(string name);

    IReadOnlyList<PointsDefinition> GetAll();

    void Add(params PointsDefinition[] definitions);
}