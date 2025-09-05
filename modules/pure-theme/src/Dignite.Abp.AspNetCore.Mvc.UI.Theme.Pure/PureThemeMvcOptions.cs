using System;
using System.Collections.Generic;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Toolbars;
using Volo.Abp.UI.Navigation;

namespace Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Themes.Pure;
public class PureThemeMvcOptions
{
    /// <summary>
    /// Determines layout of application. Default value is <see cref="PureMvcLayouts.TopMenu"/>
    /// </summary>
    public string ApplicationLayout { get; set; } = PureMvcLayouts.TopMenu;

    /// <summary>
    /// A selector that defines which menu items will be displayed at mobile layout.
    /// </summary>
    public Func<IReadOnlyList<ApplicationMenuItem>, IEnumerable<ApplicationMenuItem>> MobileMenuSelector { get; set; } = (menuItems) => menuItems;


    /// <summary>
    /// A selector that defines which toolbar items will be displayed at mobile layout.
    /// </summary>
    public Func<IReadOnlyList<ToolbarItem>, IEnumerable<ToolbarItem>> MobileToolbarSelector { get; set; } = (menuItems) => menuItems;
}
