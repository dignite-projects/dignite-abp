using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;

namespace Dignite.Abp.MultiTenancyDomains;

[DependsOn(
    typeof(AbpMultiTenancyDomainsDomainModule),
    typeof(AbpMultiTenancyDomainsApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule)
    )]
public class AbpMultiTenancyDomainsApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<AbpMultiTenancyDomainsApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<AbpMultiTenancyDomainsApplicationModule>(validate: true);
        });
    }
}
