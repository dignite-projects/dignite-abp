using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;

namespace Dignite.Abp.FieldCustomizing.MongoDB;

[DependsOn(
    typeof(AbpFieldCustomizingDomainModule),
    typeof(AbpMongoDbModule)
    )]
public class FieldCustomizingMongoDbModule : AbpModule
{
}
