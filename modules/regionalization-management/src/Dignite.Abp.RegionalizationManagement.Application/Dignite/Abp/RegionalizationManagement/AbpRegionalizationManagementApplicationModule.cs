using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using Volo.Abp.SettingManagement;

namespace Dignite.Abp.RegionalizationManagement;

[DependsOn(
    typeof(AbpRegionalizationManagementApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule),
    typeof(AbpSettingManagementDomainModule)
    )]
public class AbpRegionalizationManagementApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<AbpRegionalizationManagementApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<AbpRegionalizationManagementApplicationModule>(validate: true);
        });
    }
}
