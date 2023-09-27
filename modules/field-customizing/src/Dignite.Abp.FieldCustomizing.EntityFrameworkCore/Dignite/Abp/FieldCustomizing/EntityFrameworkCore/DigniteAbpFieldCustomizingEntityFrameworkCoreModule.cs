using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Dignite.Abp.FieldCustomizing.EntityFrameworkCore;

[DependsOn(
    typeof(DigniteAbpFieldCustomizingDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class DigniteAbpFieldCustomizingEntityFrameworkCoreModule : AbpModule
{
}