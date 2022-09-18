using System;
using Volo.Abp.Authorization;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.EventBus;
using Volo.Abp.Features;
using Volo.Abp.Guids;
using Volo.Abp.Modularity;
using Volo.Abp.Timing;
using Volo.Abp.Identity;

namespace Dignite.Abp.Notifications.Identity
{
    [DependsOn(
        typeof(AbpAuthorizationModule),
        typeof(AbpNotificationsModule),
        typeof(AbpIdentityDomainModule)
        )]
    public class AbpNotificationsIdentityModule:AbpModule
    {
    }
}

