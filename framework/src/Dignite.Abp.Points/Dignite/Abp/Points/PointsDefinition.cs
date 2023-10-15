using System.Collections.Generic;
using JetBrains.Annotations;
using Volo.Abp.Localization;

namespace Dignite.Abp.Points;

/// <summary>
/// Definition for a points.
/// </summary>
public class PointsDefinition
{
    /// <summary>
    /// Unique name of the points.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Display name of the points.
    /// Optional.
    /// </summary>
    public ILocalizableString DisplayName { get; set; }

    /// <summary>
    /// Description for the points.
    /// Optional.
    /// </summary>
    public ILocalizableString Description { get; set; }

    /// <summary>
    /// Workflows for defining points 
    /// </summary>
    public IEnumerable<PointsWorkflow> Workflows { get; private set; }


    /// <summary>
    /// Initializes a new instance of the <see cref="PointsDefinition"/> class.
    /// </summary>
    /// <param name="name">Unique name of the points.</param>
    /// <param name="displayName">Display name of the points.</param>
    /// <param name="description">Description for the points</param>
    /// <param name="workflows">Workflows for defining points</param>
    public PointsDefinition(
        [NotNull] string name, 
        ILocalizableString displayName = null, 
        ILocalizableString description = null,
        params PointsWorkflow[] workflows)
    {
        Name = name;
        DisplayName = displayName;
        Description = description;
        Workflows =  workflows;
    }
}