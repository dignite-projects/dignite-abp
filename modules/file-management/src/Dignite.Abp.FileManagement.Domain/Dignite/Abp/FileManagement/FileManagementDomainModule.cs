using Dignite.Abp.BlobStoring.InfoPersistent;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Dignite.Abp.FileManagement;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(FileManagementDomainSharedModule),
    typeof(AbpBlobStoringInfoPersistentModule)
)]
public class FileManagementDomainModule : AbpModule
{
}
