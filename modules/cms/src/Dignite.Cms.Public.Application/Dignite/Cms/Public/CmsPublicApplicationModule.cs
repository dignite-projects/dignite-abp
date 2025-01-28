using Volo.CmsKit.Public;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Dignite.FileExplorer;

namespace Dignite.Cms.Public;

[DependsOn(
    typeof(CmsDomainModule),
    typeof(CmsPublicApplicationContractsModule),
    typeof(CmsKitPublicApplicationModule),
    typeof(FileExplorerApplicationModule)
    )]
public class CmsPublicApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<CmsPublicApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<CmsPublicApplicationModule>(validate: true);
        });
    }
}
