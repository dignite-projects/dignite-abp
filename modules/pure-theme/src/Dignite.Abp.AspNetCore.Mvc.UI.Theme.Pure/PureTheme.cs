using Dignite.Abp.AspNetCore.Mvc.UI.TenantTheme;
using Volo.Abp.AspNetCore.Mvc.UI.Theming;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure;

[ThemeName(Name)]
public class PureTheme : TenantThemeBase, ITheme, ITransientDependency
{
    public const string Name = "Pure";

    public override string GetLayout(string name, bool fallbackToDefault = true)
    {
        return base.GetLayout(name, fallbackToDefault);
    }
}