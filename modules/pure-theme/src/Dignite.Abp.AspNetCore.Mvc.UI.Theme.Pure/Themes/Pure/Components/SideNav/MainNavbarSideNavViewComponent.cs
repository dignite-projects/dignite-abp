using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.UI.Navigation;

namespace Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Themes.Pure.Components.SideNav
{
    [ViewComponent(Name = "SideNav")]
    public class MainNavbarSideNavViewComponent : AbpViewComponent
    {
        protected IMenuManager MenuManager { get; }

        public MainNavbarSideNavViewComponent(IMenuManager menuManager)
        {
            MenuManager = menuManager;
        }

        public virtual async Task<IViewComponentResult> InvokeAsync()
        {
            var menuItem = await FindRootMenuItem(Request.Path.Value.TrimStart('/', '~'));
            return View(menuItem);
        }

        private async Task<ApplicationMenuItem> FindRootMenuItem(string location)
        {
            var mainMenu = await MenuManager.GetMainMenuAsync();
            var rootMenuItem = mainMenu.Items.FirstOrDefault(menu =>
                menu.Url != null && !menu.Url.TrimStart('/', '~').IsNullOrEmpty() && location.StartsWith(menu.Url.TrimStart('/', '~'), StringComparison.OrdinalIgnoreCase)
                );
            if (rootMenuItem == null)
            {
                foreach (var topMenuItem in mainMenu.Items)
                {
                    rootMenuItem= FindRootMenuItemWithChildren(topMenuItem, topMenuItem.Items, location);
                    if (rootMenuItem != null)
                        return rootMenuItem;
                }
            }

            return rootMenuItem;
        }

        private ApplicationMenuItem FindRootMenuItemWithChildren(ApplicationMenuItem topMenuItem, ApplicationMenuItemList menuItems, string location)
        {
            var menu = menuItems.FirstOrDefault(menu => menu.Url != null && location.StartsWith(menu.Url.TrimStart('/', '~'), StringComparison.OrdinalIgnoreCase));
            if (menu != null)
            {
                return topMenuItem;
            }
            else
            {
                foreach (var menuItem in menuItems)
                {
                    var rootMenuItem = FindRootMenuItemWithChildren(topMenuItem, menuItem.Items, location);
                    if(rootMenuItem!=null)
                        return rootMenuItem;
                }
            }

            return null;
        }
    }

}
