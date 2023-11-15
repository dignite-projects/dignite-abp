using Dignite.CmsKit.Public;
using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Dignite.CmsKit;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(DigniteCmsKitPublicHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class CmsKitConsoleApiClientModule : AbpModule
{

}
