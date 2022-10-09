using Volo.Abp.AspNetCore.SignalR;
using Volo.Abp.Modularity;

namespace Dignite.Abp.Notifications.SignalRNotifier;

[DependsOn(
    typeof(AbpNotificationsModule),
    typeof(AbpAspNetCoreSignalRModule)
    )]
public class AbpNotificationsSignalRNotifierModule : AbpModule
{
}