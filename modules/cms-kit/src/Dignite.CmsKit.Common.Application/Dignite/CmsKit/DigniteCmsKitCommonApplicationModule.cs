using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.CmsKit;

namespace Dignite.CmsKit;

[DependsOn(
    typeof(DigniteCmsKitDomainModule),
    typeof(DigniteCmsKitCommonApplicationContractsModule),
    typeof(CmsKitCommonApplicationModule)
    )]
public class DigniteCmsKitCommonApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<DigniteCmsKitCommonApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<DigniteCmsKitCommonApplicationModule>(validate: true);
        });
    }
}
