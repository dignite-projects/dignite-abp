using test_Item.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace test_Item;

[DependsOn(
    typeof(test_ItemEntityFrameworkCoreTestModule)
    )]
public class test_ItemDomainTestModule : AbpModule
{

}
