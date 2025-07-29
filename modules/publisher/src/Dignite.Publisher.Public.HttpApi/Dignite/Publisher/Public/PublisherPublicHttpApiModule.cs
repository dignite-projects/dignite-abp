using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.CmsKit.Public;

namespace Dignite.Publisher.Public;

[DependsOn(
    typeof(CmsKitPublicHttpApiModule),
    typeof(PublisherPublicApplicationContractsModule),
    typeof(PublisherCommonHttpApiModule))]
public class PublisherPublicHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(PublisherPublicHttpApiModule).Assembly);
        });
    }
}
