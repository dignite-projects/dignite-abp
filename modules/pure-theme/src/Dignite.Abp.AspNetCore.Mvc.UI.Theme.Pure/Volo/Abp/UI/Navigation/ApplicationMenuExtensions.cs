using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Volo.Abp.UI.Navigation;
public static class ApplicationMenuExtensions
{
    public static List<ApplicationMenuItem> GetMenuChains(this ApplicationMenu menu, [NotNull] string menuItemName)
    {
        var menuItems = new List<ApplicationMenuItem>();
        var menuItem = menu.FindMenuItem(menuItemName);
        if (menuItem != null)
        {
            menuItems.Add(menuItem);

            if (!menu.Items.Contains(menuItem))
            {
                foreach (var item in menu.Items)
                {
                    GetParentMenuItem(menu, item, menuItem.Name, menuItems);
                }
            }
        }

        return menuItems;
    }

    private static void GetParentMenuItem(ApplicationMenu menu, ApplicationMenuItem parentMenuItem, string menuItemName, List<ApplicationMenuItem> breadcrumbMenuItems)
    {
        if (parentMenuItem.Items.Any(i => i.Name == menuItemName))
        {
            breadcrumbMenuItems.Insert(0, parentMenuItem);
            foreach (var item in menu.Items)
            {
                GetParentMenuItem(menu, item, parentMenuItem.Name, breadcrumbMenuItems);
            }
        }
        else
        {
            foreach (var item in parentMenuItem.Items)
            {
                GetParentMenuItem(menu, item, menuItemName, breadcrumbMenuItems);
            }
        }
    }
}
