using Dignite.Publisher.Posts.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Json.SystemTextJson;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;
using Volo.CmsKit;

namespace Dignite.Publisher;

[DependsOn(
    typeof(CmsKitCommonHttpApiClientModule),
    typeof(PublisherCommonApplicationContractsModule)
    )]
public class PublisherCommonHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var serviceProvider = context.Services.BuildServiceProvider();

        context.Services.AddHttpClientProxies(
            typeof(PublisherCommonApplicationContractsModule).Assembly,
            PublisherCommonRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<PublisherCommonHttpApiClientModule>();
        });

        // Register the custom JSON serialization options for PostDto
        Configure<AbpSystemTextJsonSerializerOptions>(options =>
        {
            options.JsonSerializerOptions.ConfigurePostDtoConverters(serviceProvider);
        });
    }
}
