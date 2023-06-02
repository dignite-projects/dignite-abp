using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Authorization;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.EventBus;
using Volo.Abp.Features;
using Volo.Abp.Guids;
using Volo.Abp.Modularity;
using Volo.Abp.Timing;

namespace Dignite.Abp.Notifications;

[DependsOn(
    typeof(AbpNotificationsSharedModule),
    typeof(AbpFeaturesModule),
    typeof(AbpTimingModule),
    typeof(AbpBackgroundJobsAbstractionsModule),
    typeof(AbpGuidsModule),
    typeof(AbpEventBusModule)
    )]
public class AbpNotificationsModule : AbpModule
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
            if (typeof(INotificationDefinitionProvider).IsAssignableFrom(context.ImplementationType))
            {
                definitionProviders.Add(context.ImplementationType);
            }
        });

        services.Configure<NotificationOptions>(options =>
        {
            options.DefinitionProviders.AddIfNotContains(definitionProviders);
        });
    }
}