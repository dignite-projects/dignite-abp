using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Dignite.Abp.NotificationCenter;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AbpNotificationCenterHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class AbpNotificationCenterConsoleApiClientModule : AbpModule
{

}
