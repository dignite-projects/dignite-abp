using System;
using System.Collections.Generic;
using Dignite.Abp.FieldCustomizing;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.Settings;

namespace Dignite.Abp.SettingsGrouping;

[DependsOn(
    typeof(AbpSettingsModule),
    typeof(AbpFieldCustomizingModule)
    )]
public class AbpSettingsGroupingModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        AutoAddDefinitionProviders(context.Services);
    }

    private static void AutoAddDefinitionProviders(IServiceCollection services)
    {
        var groupingDefinitionProviders = new List<Type>();

        services.OnRegistred(context =>
        {
            if (typeof(ISettingDefinitionGroupProvider).IsAssignableFrom(context.ImplementationType))
            {
                groupingDefinitionProviders.Add(context.ImplementationType);
            }
        });

        services.Configure<AbpSettingGroupOptions>(options =>
        {
            options.GroupingDefinitionProviders.AddIfNotContains(groupingDefinitionProviders);
        });
    }
}