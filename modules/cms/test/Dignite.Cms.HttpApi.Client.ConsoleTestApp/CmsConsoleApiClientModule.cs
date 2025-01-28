using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Dignite.Cms;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(CmsHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class CmsConsoleApiClientModule : AbpModule
{

}
