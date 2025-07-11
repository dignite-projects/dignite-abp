using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using Volo.Abp.SettingManagement;
using Dignite.Abp.TenantDomain;

namespace Dignite.Abp.TenantDomainManagement;

[DependsOn(
    typeof(AbpTenantDomainManagementApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule),
    typeof(AbpTenantDomainModule),
    typeof(AbpSettingManagementDomainModule)
    )]
public class AbpTenantDomainManagementApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<AbpTenantDomainManagementApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<AbpTenantDomainManagementApplicationModule>(validate: true);
        });
    }
}
