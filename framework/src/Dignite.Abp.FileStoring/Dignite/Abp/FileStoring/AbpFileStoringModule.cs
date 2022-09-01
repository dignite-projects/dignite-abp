using Volo.Abp.BlobStoring;
using Volo.Abp.Modularity;

namespace Dignite.Abp.FileStoring;

[DependsOn(
    typeof(AbpBlobStoringModule)
    )]
public class AbpFileStoringModule : AbpModule
{
}
