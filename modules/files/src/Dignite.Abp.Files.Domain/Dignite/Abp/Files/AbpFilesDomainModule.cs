using Dignite.Abp.FileStoring;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Dignite.Abp.Files;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(AbpFilesDomainSharedModule),
    typeof(AbpFileStoringModule)
)]
public class AbpFilesDomainModule : AbpModule
{    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSingleton<ICurrentFileAccessor>(AsyncLocalCurrentFileAccessor.Instance);
    }
}
