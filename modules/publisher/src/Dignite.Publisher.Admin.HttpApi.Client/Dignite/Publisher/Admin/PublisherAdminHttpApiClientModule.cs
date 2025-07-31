
using System;
using Dignite.Publisher.Admin.Posts.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Json.SystemTextJson;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;
using Volo.CmsKit.Admin;

namespace Dignite.Publisher.Admin;

[DependsOn(
    typeof(CmsKitAdminHttpApiClientModule),
    typeof(PublisherAdminApplicationContractsModule),
    typeof(PublisherCommonHttpApiClientModule))]
public class PublisherAdminHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var serviceProvider = context.Services.BuildServiceProvider();

        context.Services.AddHttpClientProxies(
            typeof(PublisherAdminApplicationContractsModule).Assembly,
            PublisherAdminRemoteServiceConsts.RemoteServiceName
            );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<PublisherAdminHttpApiClientModule>();
        });

        // Register the custom JSON serialization options for PostDto
        Configure<AbpSystemTextJsonSerializerOptions>(options =>
        {
            options.JsonSerializerOptions.ConfigurePostAdminDtoConverters(serviceProvider);
        });
    }
}
