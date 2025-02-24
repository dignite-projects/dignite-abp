using Dignite.Abp.TenantDomain;
using Dignite.Abp.TenantDomain.OpenIddict;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp.Modularity;

namespace Dignite.Abp.TenantDomainManagement;

[DependsOn(
    typeof(AbpTenantDomainManagementApplicationModule),
    typeof(AbpTenantDomainManagementTestBaseModule),
    typeof(AbpTenantDomainOpenIddictModule)
    )]
public class AbpTenantDomainManagementApplicationTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpTenantDomainManagementOptions>(options =>
        {
            options.TenantDomainFormat = "{0}.travely.dignite.com";
            options.AuthServerClientId = "Client1";
        });

#if DEBUG
        context.Services.Replace(ServiceDescriptor.Singleton<IWebServerManager, NullWebServerManager>());
#endif
    }
}
