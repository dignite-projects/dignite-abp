using Volo.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Modularity;

namespace Dignite.Abp.AspNetCore.Components.CkEditor.WebAssembly;

[DependsOn(
    typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule)
    )]
public class AbpAspNetCoreComponentsCkEditorWebAssemblyModule : AbpModule
{
}
