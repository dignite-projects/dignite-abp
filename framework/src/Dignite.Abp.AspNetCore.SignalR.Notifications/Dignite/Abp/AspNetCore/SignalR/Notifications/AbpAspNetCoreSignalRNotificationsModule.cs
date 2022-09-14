using Dignite.Abp.Notifications;
using Volo.Abp.AspNetCore.SignalR;
using Volo.Abp.Modularity;

namespace Dignite.Abp.AspNetCore.SignalR.Notifications;

[DependsOn(
    typeof(AbpNotificationsModule),
    typeof(AbpAspNetCoreSignalRModule)
    )]
public class AbpAspNetCoreSignalRNotificationsModule : AbpModule
{
}