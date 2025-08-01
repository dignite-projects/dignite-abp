using Dignite.FileExplorer;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.CmsKit;


namespace Dignite.Publisher;

[DependsOn(
    typeof(CmsKitCommonApplicationModule),
    typeof(FileExplorerApplicationModule),
    typeof(PublisherDomainModule),
    typeof(PublisherCommonApplicationContractsModule)
    )]
public class PublisherCommonApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<PublisherCommonApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<PublisherCommonApplicationModule>(validate: true);
        });
    }
}
