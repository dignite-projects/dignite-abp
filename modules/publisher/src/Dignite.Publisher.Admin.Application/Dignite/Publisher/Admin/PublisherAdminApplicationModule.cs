using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.CmsKit.Admin;

namespace Dignite.Publisher.Admin;

[DependsOn(
    typeof(CmsKitAdminApplicationModule),
    typeof(PublisherAdminApplicationContractsModule),
    typeof(PublisherCommonApplicationModule)
    )]
public class PublisherAdminApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<PublisherAdminApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<PublisherAdminApplicationModule>(validate: true);
        });
    }
}
