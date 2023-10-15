using System.Collections.Generic;

namespace Dignite.Abp.Points;

/// <summary>
/// Used to manage points definitions.
/// </summary>
public interface IPointsDefinitionManager
{
    /// <summary>
    /// Gets a points definition by name.
    /// Throws exception if there is no points definition with given name.
    /// </summary>
    PointsDefinition Get(string name);

    /// <summary>
    /// Gets a points definition by name.
    /// Returns null if there is no points definition with given name.
    /// </summary>
    PointsDefinition GetOrNull(string name);

    /// <summary>
    /// Gets all points definitions.
    /// </summary>
    IReadOnlyList<PointsDefinition> GetAll();
}