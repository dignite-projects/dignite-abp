using Dignite.Publisher.Localization;
using Dignite.Publisher.Posts.Serialization;
using Localization.Resources.AbpUi;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.CmsKit;

namespace Dignite.Publisher;

[DependsOn(
    typeof(CmsKitCommonHttpApiModule),
    typeof(PublisherCommonApplicationContractsModule)
    )]
public class PublisherCommonHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(PublisherCommonHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var serviceProvider = context.Services.BuildServiceProvider();

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<PublisherResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });

        //
        context.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ConfigurePostDtoConverters(serviceProvider);
            });
    }
}
