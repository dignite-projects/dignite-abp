using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.CmsKit.Public;

namespace Dignite.CmsKit.Public;

[DependsOn(
    typeof(DigniteCmsKitDomainModule),
    typeof(DigniteCmsKitPublicApplicationContractsModule),
    typeof(CmsKitPublicApplicationModule)
    )]
public class DigniteCmsKitPublicApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<DigniteCmsKitPublicApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<DigniteCmsKitPublicApplicationModule>(validate: true);
        });
    }
}
