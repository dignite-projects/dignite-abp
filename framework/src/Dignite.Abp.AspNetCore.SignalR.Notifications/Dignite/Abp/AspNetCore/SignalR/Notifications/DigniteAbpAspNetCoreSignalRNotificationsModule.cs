using Dignite.Abp.Notifications;
using Volo.Abp.AspNetCore.SignalR;
using Volo.Abp.Modularity;

namespace Dignite.Abp.AspNetCore.SignalR.Notifications
{
    [DependsOn(
        typeof(DigniteAbpNotificationsModule),
        typeof(AbpAspNetCoreSignalRModule)
        )]
    public class DigniteAbpAspNetCoreSignalRNotificationsModule : AbpModule
    {
    }
}
