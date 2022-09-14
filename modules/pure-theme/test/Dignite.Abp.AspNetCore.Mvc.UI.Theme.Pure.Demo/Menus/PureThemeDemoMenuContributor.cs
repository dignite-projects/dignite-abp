using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Demo.Menus;

public class PureThemeDemoMenuContributor : IMenuContributor
{
    public Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            AddMainMenuItems(context);
        }

        return Task.CompletedTask;
    }

    private void AddMainMenuItems(MenuConfigurationContext context)
    {
        var menuItem = new ApplicationMenuItem(PureThemeDemoMenus.Components.Root, "Components");

        var items = new List<ApplicationMenuItem>()
            {
                new ApplicationMenuItem(PureThemeDemoMenus.Components.Alerts, "Alerts", url: "/Components/Alerts"),
                new ApplicationMenuItem(PureThemeDemoMenus.Components.Badges, "Badges", url: "/Components/Badges"),
                new ApplicationMenuItem(PureThemeDemoMenus.Components.Borders, "Borders", url: "/Components/Borders"),
                new ApplicationMenuItem(PureThemeDemoMenus.Components.Breadcrumbs, "Breadcrumbs", url: "/Components/Breadcrumbs"),
                new ApplicationMenuItem(PureThemeDemoMenus.Components.Buttons, "Buttons", url: "/Components/Buttons"),
                new ApplicationMenuItem(PureThemeDemoMenus.Components.ButtonGroups, "Button Groups", url: "/Components/ButtonGroups"),
                new ApplicationMenuItem(PureThemeDemoMenus.Components.Cards, "Cards", url: "/Components/Cards"),
                new ApplicationMenuItem(PureThemeDemoMenus.Components.Carousel, "Carousel", url: "/Components/Carousel"),
                new ApplicationMenuItem(PureThemeDemoMenus.Components.Collapse, "Collapse", url: "/Components/Collapse"),
                new ApplicationMenuItem(PureThemeDemoMenus.Components.Dropdowns, "Dropdowns", url: "/Components/Dropdowns"),
                new ApplicationMenuItem(PureThemeDemoMenus.Components.DynamicForms, "Dynamic Forms", url: "/Components/DynamicForms"),
                new ApplicationMenuItem(PureThemeDemoMenus.Components.FormElements, "Form Elements", url: "/Components/FormElements"),
                new ApplicationMenuItem(PureThemeDemoMenus.Components.Grids, "Grids", url: "/Components/Grids"),
                new ApplicationMenuItem(PureThemeDemoMenus.Components.ListGroups, "List Groups", url: "/Components/ListGroups"),
                new ApplicationMenuItem(PureThemeDemoMenus.Components.Modals, "Modals", url: "/Components/Modals"),
                new ApplicationMenuItem(PureThemeDemoMenus.Components.Navs, "Navs", url: "/Components/Navs"),
                new ApplicationMenuItem(PureThemeDemoMenus.Components.Navbars, "Navbars", url: "/Components/Navbars"),
                new ApplicationMenuItem(PureThemeDemoMenus.Components.Paginator, "Paginator", url: "/Components/Paginator"),
                new ApplicationMenuItem(PureThemeDemoMenus.Components.Popovers, "Popovers", url: "/Components/Popovers"),
                new ApplicationMenuItem(PureThemeDemoMenus.Components.ProgressBars, "Progress Bars", url: "/Components/ProgressBars"),
                new ApplicationMenuItem(PureThemeDemoMenus.Components.Tables, "Tables", url: "/Components/Tables"),
                new ApplicationMenuItem(PureThemeDemoMenus.Components.Tabs, "Tabs", url: "/Components/Tabs"),
                new ApplicationMenuItem(PureThemeDemoMenus.Components.Tooltips, "Tooltips", url: "/Components/Tooltips")
            };

        items.OrderBy(x => x.Name)
             .ToList()
             .ForEach(x => menuItem.AddItem(x));

        context.Menu.AddItem(menuItem);
    }
}
