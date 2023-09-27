using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Dignite.Abp.Files.EntityFrameworkCore;

[DependsOn(
    typeof(DigniteAbpFilesDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class DigniteAbpFilesEntityFrameworkCoreModule : AbpModule
{
}