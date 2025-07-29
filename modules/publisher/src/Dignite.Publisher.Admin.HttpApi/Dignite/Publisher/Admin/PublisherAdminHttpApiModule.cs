using Dignite.Publisher.Admin.Posts.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.CmsKit.Admin;

namespace Dignite.Publisher.Admin;

[DependsOn(
    typeof(CmsKitAdminHttpApiModule),
    typeof(PublisherAdminApplicationContractsModule),
    typeof(PublisherCommonHttpApiModule))]
public class PublisherAdminHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(PublisherAdminHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var serviceProvider = context.Services.BuildServiceProvider();
        context.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ConfigureCreateOrUpdatePostDtoConverters(serviceProvider);
            });
    }
}
