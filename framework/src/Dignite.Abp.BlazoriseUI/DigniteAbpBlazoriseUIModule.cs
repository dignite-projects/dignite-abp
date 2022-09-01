using Volo.Abp.BlazoriseUI;
using Volo.Abp.Modularity;

namespace Dignite.Abp.BlazoriseUI;

[DependsOn(
    typeof(AbpBlazoriseUIModule)
    )]
public class DigniteAbpBlazoriseUIModule: AbpModule
{
}

