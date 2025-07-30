using Dignite.Publisher.Admin.Posts;
using Microsoft.AspNetCore.Authorization;
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

        Configure<AuthorizationOptions>(options =>
        {
            //TODO: Rename UpdatePolicy/DeletePolicy since it's candidate to conflicts with other modules!
            options.AddPolicy("PublisherUpdatePolicy", policy => policy.Requirements.Add(CommonOperations.Update));
            options.AddPolicy("PublisherDeletePolicy", policy => policy.Requirements.Add(CommonOperations.Delete));
        });

        context.Services.AddSingleton<IAuthorizationHandler, PostAuthorizationHandler>();
    }
}
