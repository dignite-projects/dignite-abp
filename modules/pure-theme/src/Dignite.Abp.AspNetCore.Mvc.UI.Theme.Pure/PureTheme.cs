using Dignite.Abp.AspNetCore.Mvc.UI.MultiTenancyTheme;
using Volo.Abp.AspNetCore.Mvc.UI.Theming;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure;

[ThemeName(Name)]
public class PureTheme : MultiTenancyThemeBase, ITheme, ITransientDependency
{
    public const string Name = "Pure";

    public override string GetLayout(string name, bool fallbackToDefault = true)
    {
        return base.GetLayout(name, fallbackToDefault);
    }
}