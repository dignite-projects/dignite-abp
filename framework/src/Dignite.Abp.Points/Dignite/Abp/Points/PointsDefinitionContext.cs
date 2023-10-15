using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Dignite.Abp.Points;

internal class PointsDefinitionContext : IPointsDefinitionContext
{
    protected Dictionary<string, PointsDefinition> PointsDefinitions { get; }

    public PointsDefinitionContext(Dictionary<string, PointsDefinition> points)
    {
        PointsDefinitions = points;
    }

    public virtual PointsDefinition GetOrNull(string name)
    {
        return PointsDefinitions.GetOrDefault(name);
    }

    public virtual IReadOnlyList<PointsDefinition> GetAll()
    {
        return PointsDefinitions.Values.ToImmutableList();
    }

    public virtual void Add(params PointsDefinition[] definitions)
    {
        if (definitions.IsNullOrEmpty())
        {
            return;
        }

        foreach (var definition in definitions)
        {
            PointsDefinitions[definition.Name] = definition;
        }
    }
}