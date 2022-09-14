using Volo.Abp.Modularity;

namespace Dignite.Abp.BlazoriseUI;

[DependsOn(
    typeof(Volo.Abp.BlazoriseUI.AbpBlazoriseUIModule)
    )]
public class AbpBlazoriseUiModule : AbpModule
{
}