using System;
using System.Collections.Generic;
using System.Linq;
using Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Themes.Pure.Components.Toolbar.NavbarToggler;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Toolbars;
using Volo.Abp.UI.Navigation;

namespace Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Themes.Pure;
public class PureThemeMvcOptions
{
    /// <summary>
    /// A selector that defines which menu items will be displayed at mobile layout.
    /// </summary>
    public Func<IReadOnlyList<ApplicationMenuItem>, IEnumerable<ApplicationMenuItem>> MobileMenuSelector { get; set; } = (menuItems) => menuItems.Where(x => x.Items.IsNullOrEmpty());


    /// <summary>
    /// A selector that defines which toolbar items will be displayed at mobile layout.
    /// </summary>
    public Func<IReadOnlyList<ToolbarItem>, IEnumerable<ToolbarItem>> MobileToolbarSelector { get; set; } = (menuItems) => menuItems.Where(x => x.ComponentType == typeof(NavbarTogglerViewComponent));
}
