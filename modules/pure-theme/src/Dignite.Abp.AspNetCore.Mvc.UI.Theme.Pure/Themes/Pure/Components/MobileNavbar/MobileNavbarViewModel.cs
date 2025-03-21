using System.Collections.Generic;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Toolbars;
using Volo.Abp.UI.Navigation;

namespace Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Themes.Pure.Components.MobileNavbar;
public class MobileNavbarViewModel
{
    public MobileNavbarViewModel(IReadOnlyList<ApplicationMenuItem> menuItems, IReadOnlyList<ToolbarItem> toolbarItems)
    {
        MenuItems = menuItems;
        ToolbarItems = toolbarItems;
    }

    public IReadOnlyList<ApplicationMenuItem> MenuItems { get; private set; }

    public IReadOnlyList<ToolbarItem> ToolbarItems { get; private set; }
}
