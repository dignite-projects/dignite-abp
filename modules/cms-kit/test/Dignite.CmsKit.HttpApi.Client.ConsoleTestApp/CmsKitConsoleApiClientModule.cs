using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Dignite.CmsKit;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(DigniteCmsKitHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class CmsKitConsoleApiClientModule : AbpModule
{

}
