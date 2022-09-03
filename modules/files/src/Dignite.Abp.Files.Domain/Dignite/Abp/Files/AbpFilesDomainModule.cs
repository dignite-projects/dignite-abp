using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.BlobStoring;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Dignite.Abp.Files;

[DependsOn(
    typeof(AbpBlobStoringModule),
    typeof(AbpDddDomainModule),
    typeof(AbpFilesDomainSharedModule)
)]
public class AbpFilesDomainModule : AbpModule
{    
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSingleton<ICurrentFileAccessor>(AsyncLocalCurrentFileAccessor.Instance);
    }
}
