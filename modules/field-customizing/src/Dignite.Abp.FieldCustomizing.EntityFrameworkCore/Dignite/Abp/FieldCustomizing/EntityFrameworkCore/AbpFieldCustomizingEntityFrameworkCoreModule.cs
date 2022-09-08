using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Dignite.Abp.FieldCustomizing.EntityFrameworkCore;

[DependsOn(
    typeof(AbpFieldCustomizingDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class AbpFieldCustomizingEntityFrameworkCoreModule : AbpModule
{
}
