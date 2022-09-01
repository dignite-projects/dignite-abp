using Dignite.Abp.BlobStoring.InfoPersistent.Localization;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Authorization;
using Volo.Abp.BlobStoring;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Dignite.Abp.BlobStoring.InfoPersistent;

[DependsOn(
    typeof(AbpAuthorizationModule),
    typeof(AbpBlobStoringModule),
    typeof(AbpLocalizationModule)
    )]
public class AbpBlobStoringInfoPersistentModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSingleton<ICurrentBlobInfoAccessor>(AsyncLocalCurrentBlobInfoAccessor.Instance);

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AbpBlobStoringInfoPersistentModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<BlobStoringInfoPersistentResource>("en")
                .AddVirtualJson("Dignite/Abp/BlobStroring/InfoPersistent/Localization");
        });
    }
}
