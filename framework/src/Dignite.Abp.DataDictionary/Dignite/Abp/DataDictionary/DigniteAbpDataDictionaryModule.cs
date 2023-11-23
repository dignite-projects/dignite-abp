using System;
using System.Collections.Generic;
using Dignite.Abp.DynamicForms;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Dignite.Abp.DataDictionary;

[DependsOn(
    typeof(DigniteAbpDynamicFormsModule)
)]
public class DigniteAbpDataDictionaryModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        AutoAddDefinitionProviders(context.Services);
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDataDictionaryOptions>(options =>
        {
            options.ValueProviders.Add<DefaultValueDataDictionaryValueProvider>();
            options.ValueProviders.Add<GlobalDataDictionaryValueProvider>();
        });
    }

    private static void AutoAddDefinitionProviders(IServiceCollection services)
    {
        var definitionProviders = new List<Type>();

        services.OnRegistered(context =>
        {
            if (typeof(IDataDictionaryDefinitionProvider).IsAssignableFrom(context.ImplementationType))
            {
                definitionProviders.Add(context.ImplementationType);
            }
        });

        services.Configure<AbpDataDictionaryOptions>(options =>
        {
            options.DefinitionProviders.AddIfNotContains(definitionProviders);
        });
    }
}
