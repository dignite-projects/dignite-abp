using Volo.Abp.BlobStoring;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Dignite.Abp.Files;

[DependsOn(
    typeof(AbpBlobStoringModule),
    typeof(AbpDddDomainModule),
    typeof(DigniteAbpFilesDomainSharedModule)
)]
public class DigniteAbpFilesDomainModule : AbpModule
{
}