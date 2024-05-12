using Volo.Abp.Modularity;

namespace test_Item;

[DependsOn(
    typeof(test_ItemApplicationModule),
    typeof(test_ItemDomainTestModule)
    )]
public class test_ItemApplicationTestModule : AbpModule
{

}
