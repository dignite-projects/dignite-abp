using Volo.Abp.AspNetCore.SignalR;
using Volo.Abp.Modularity;

namespace Dignite.Abp.Notifications.SignalRNotifier;

[DependsOn(
    typeof(DigniteAbpNotificationsSharedModule),
    typeof(AbpAspNetCoreSignalRModule)
    )]
public class DigniteAbpNotificationsSignalRNotifierModule : AbpModule
{
}