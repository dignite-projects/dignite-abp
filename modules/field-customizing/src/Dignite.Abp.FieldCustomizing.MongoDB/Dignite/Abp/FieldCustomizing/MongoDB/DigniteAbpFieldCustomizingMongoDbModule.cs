using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;

namespace Dignite.Abp.FieldCustomizing.MongoDB;

[DependsOn(
    typeof(DigniteAbpFieldCustomizingDomainModule),
    typeof(AbpMongoDbModule)
    )]
public class DigniteAbpFieldCustomizingMongoDbModule : AbpModule
{
}