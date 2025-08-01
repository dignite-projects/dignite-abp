using Volo.Abp.Modularity;

namespace Dignite.Abp.LocaleManagement;

[DependsOn(
    typeof(AbpLocaleManagementApplicationModule),
    typeof(AbpLocaleManagementTestBaseModule)
    )]
public class AbpLocaleManagementApplicationTestModule : AbpModule
{

}
