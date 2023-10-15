using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.Points;

/// <summary>
/// Implements <see cref="IPointsDefinitionManager"/>.
/// </summary>
public class PointsDefinitionManager : IPointsDefinitionManager, ISingletonDependency
{
    protected Lazy<IDictionary<string, PointsDefinition>> PointsDefinitions { get; }
    protected PointsOptions Options { get; }
    protected IServiceProvider ServiceProvider { get; }

    public PointsDefinitionManager(
        IOptions<PointsOptions> options,
        IServiceProvider serviceProvider
        )
    {
        ServiceProvider = serviceProvider;
        Options = options.Value;
        PointsDefinitions = new Lazy<IDictionary<string, PointsDefinition>>(CreatePointsDefinitions, true);
    }

    /// <summary>
    /// Get defined points
    /// </summary>
    /// <returns></returns>
    public virtual IReadOnlyList<PointsDefinition> GetAll()
    {
        return PointsDefinitions.Value.Values.ToImmutableList();
    }

    /// <summary>
    /// Get defined points
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    /// <exception cref="AbpException"></exception>
    public virtual PointsDefinition Get(string name)
    {
        var pointsDefinition = GetOrNull(name);
        if (pointsDefinition == null)
        {
            throw new AbpException("Undefined points: " + name);
        }

        return pointsDefinition;
    }

    /// <summary>
    /// Get defined points
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public virtual PointsDefinition GetOrNull(string name)
    {
        Check.NotNull(name, nameof(name));
        return PointsDefinitions.Value.GetOrDefault(name);
    }


    protected virtual IDictionary<string, PointsDefinition> CreatePointsDefinitions()
    {
        var points = new Dictionary<string, PointsDefinition>();

        using (var scope = ServiceProvider.CreateScope())
        {
            var providers = Options
                .DefinitionProviders
                .Select(p => scope.ServiceProvider.GetRequiredService(p) as IPointsDefinitionProvider)
                .ToList();

            foreach (var provider in providers)
            {
                provider.Define(new PointsDefinitionContext(points));
            }
        }

        return points;
    }
}