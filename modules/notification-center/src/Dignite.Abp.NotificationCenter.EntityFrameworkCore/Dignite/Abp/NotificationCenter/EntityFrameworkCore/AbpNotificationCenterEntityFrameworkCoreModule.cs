﻿using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Dignite.Abp.NotificationCenter.EntityFrameworkCore;

[DependsOn(
    typeof(AbpNotificationCenterDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class AbpNotificationCenterEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<NotificationCenterDbContext>(options =>
        {
            options.AddRepository<Notification, EfCoreNotificationRepository>();
            options.AddRepository<UserNotification, EfCoreUserNotificationRepository>();
            options.AddRepository<NotificationSubscription, EfCoreNotificationSubscriptionRepository>();
        });
    }
}