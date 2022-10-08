using Volo.Abp.AspNetCore.SignalR;
using Volo.Abp.Modularity;

namespace Dignite.Abp.Notifications.SignalR;

[DependsOn(
    typeof(AbpNotificationsModule),
    typeof(AbpAspNetCoreSignalRModule)
    )]
public class AbpNotificationsSignalRModule : AbpModule
{
}