using Volo.Abp.AspNetCore.Components;
using Volo.Abp.Modularity;

namespace Dignite.Abp.Notifications.Components;

[DependsOn(
    typeof(AbpAspNetCoreComponentsModule)
    )]
public class AbpNotificationsComponentsModule: AbpModule
{
}
