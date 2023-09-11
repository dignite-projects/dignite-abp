using Dignite.Abp.AspNetCore.Mvc.UI.Theming;
using Volo.Abp.AspNetCore.Mvc.UI.Theming;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure;

[ThemeName(Name)]
public class PureTheme : MultiTenancyThemeBase, ITransientDependency
{
    public const string Name = "Pure";

}