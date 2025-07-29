using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.CmsKit.Public;

namespace Dignite.Publisher.Public;

[DependsOn(
    typeof(CmsKitPublicApplicationModule),
    typeof(PublisherCommonApplicationModule),
    typeof(PublisherPublicApplicationContractsModule)
    )]
public class PublisherPublicApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<PublisherPublicApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<PublisherPublicApplicationModule>(validate: true);
        });
    }
}
