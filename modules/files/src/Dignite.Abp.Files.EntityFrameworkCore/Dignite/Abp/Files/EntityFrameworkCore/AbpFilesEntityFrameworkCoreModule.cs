using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Dignite.Abp.Files.EntityFrameworkCore;

[DependsOn(
    typeof(AbpFilesDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class AbpFilesEntityFrameworkCoreModule : AbpModule
{
}