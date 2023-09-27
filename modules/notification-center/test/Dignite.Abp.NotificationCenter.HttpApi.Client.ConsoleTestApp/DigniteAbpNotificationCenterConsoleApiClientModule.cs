using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Dignite.Abp.NotificationCenter;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(DigniteAbpNotificationCenterHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class DigniteAbpNotificationCenterConsoleApiClientModule : AbpModule
{
}