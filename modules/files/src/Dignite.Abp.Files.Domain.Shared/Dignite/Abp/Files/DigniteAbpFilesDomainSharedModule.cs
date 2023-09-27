using Volo.Abp.Modularity;
using Volo.Abp.Validation;

namespace Dignite.Abp.Files;

[DependsOn(
    typeof(AbpValidationModule)
)]
public class DigniteAbpFilesDomainSharedModule : AbpModule
{
}