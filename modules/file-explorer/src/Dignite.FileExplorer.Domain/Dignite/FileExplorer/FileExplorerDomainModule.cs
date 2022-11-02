using Dignite.Abp.Files;
using Dignite.FileExplorer.Directories;
using Dignite.FileExplorer.Files;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Domain;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.Modularity;

namespace Dignite.FileExplorer;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(FileExplorerDomainSharedModule),
    typeof(AbpFilesDomainModule),
    typeof(AbpAutoMapperModule)
)]
public class FileExplorerDomainModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<FileExplorerDomainModule>();

        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddProfile<FileExplorerDomainMappingProfile>(validate: true);
        });

        Configure<AbpDistributedEntityEventOptions>(options =>
        {
            options.EtoMappings.Add<FileDescriptor, FileDescriptorEto>(typeof(FileExplorerDomainModule));
            options.EtoMappings.Add<DirectoryDescriptor, DirectoryDescriptorEto>(typeof(FileExplorerDomainModule));

            options.AutoEventSelectors.Add<FileDescriptor>();
        });
    }
}