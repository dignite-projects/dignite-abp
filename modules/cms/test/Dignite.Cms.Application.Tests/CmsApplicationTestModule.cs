using Volo.Abp.Modularity;

namespace Dignite.Cms;

[DependsOn(
    typeof(CmsApplicationModule),
    typeof(CmsDomainTestModule)
    )]
public class CmsApplicationTestModule : AbpModule
{

}
