using Dignite.Abp.BlobStoring;
using Dignite.FileExplorer.TestObjects;
using Volo.Abp.BlobStoring;
using Volo.Abp.Modularity;

namespace Dignite.FileExplorer;

[DependsOn(
    typeof(FileExplorerApplicationModule),
    typeof(FileExplorerDomainTestModule)
    )]
public class FileExplorerApplicationTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBlobStoringOptions>(options =>
        {
            options.Containers
                .Configure<TestContainer1>(container =>
                {
                    container.SetAuthorizationConfiguration(config =>
                    {
                        config.CreateDirectoryPermissionName = "permissionName1";
                        config.CreateFilePermissionName = "permissionName2";
                        config.UpdateFilePermissionName = "permissionName2";
                        config.DeleteFilePermissionName = "permissionName2";
                        config.SetAuthorizationHandler<TestFileDescriptorEntityAuthorizationHandler>();
                    });
                });
        });
    }
}