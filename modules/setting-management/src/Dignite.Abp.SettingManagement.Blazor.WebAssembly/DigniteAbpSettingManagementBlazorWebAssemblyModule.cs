using Volo.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Modularity;

namespace Dignite.Abp.SettingManagement.Blazor.WebAssembly
{
    [DependsOn(
        typeof(DigniteAbpSettingManagementBlazorModule),
        typeof(DigniteAbpSettingManagementHttpApiClientModule),
        typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule)
        )]
    public class DigniteAbpSettingManagementBlazorWebAssemblyModule : AbpModule
    {
        
    }
}