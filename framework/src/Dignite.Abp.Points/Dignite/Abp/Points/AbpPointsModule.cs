using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System;
using Volo.Abp.Modularity;
using Volo.Abp.Timing;

namespace Dignite.Abp.Points;

[DependsOn(
    typeof(AbpTimingModule)
    )]
public class AbpPointsModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        AutoAddDefinitionProviders(context.Services);
    }

    private static void AutoAddDefinitionProviders(IServiceCollection services)
    {
        var definitionProviders = new List<Type>();

        services.OnRegistered(context =>
        {
            if (typeof(IPointsDefinitionProvider).IsAssignableFrom(context.ImplementationType))
            {
                definitionProviders.Add(context.ImplementationType);
            }
        });

        services.Configure<PointsOptions>(options =>
        {
            options.DefinitionProviders.AddIfNotContains(definitionProviders);
        });
    }
}
