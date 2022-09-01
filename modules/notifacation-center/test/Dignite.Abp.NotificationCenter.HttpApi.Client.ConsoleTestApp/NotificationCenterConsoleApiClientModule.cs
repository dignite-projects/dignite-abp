using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Dignite.Abp.NotificationCenter
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(NotificationCenterHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class NotificationCenterConsoleApiClientModule : AbpModule
    {

    }
}
