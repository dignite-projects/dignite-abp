using Volo.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Modularity;

namespace Dignite.Abp.SettingManagement.Blazor.WebAssembly;

[DependsOn(
    typeof(AbpSettingManagementBlazorModule),
    typeof(AbpSettingManagementHttpApiClientModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule)
    )]
public class AbpSettingManagementBlazorWebAssemblyModule : AbpModule
{
}