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
            AddPublicMenuItems(context);
        }
        if (context.Menu.Name == NavbarConsts.HomeMenus)
        {
            AddHomePageMenuItems(context);
        }

        return Task.CompletedTask;
    }

    private void AddPublicMenuItems(MenuConfigurationContext context)
    {
        context.Menu.AddItem(new ApplicationMenuItem("home", "Home", "/", "fa fa-home"));

        var menuItem = new ApplicationMenuItem(PureThemeDemoMenus.Components.Root, "Components");

        var items = new List<ApplicationMenuItem>()
            {
                new ApplicationMenuItem(PureThemeDemoMenus.Components.Alerts, "Alerts", url: "#"),
                new ApplicationMenuItem(PureThemeDemoMenus.Components.Badges, "Badges", url: "#"),
                new ApplicationMenuItem(PureThemeDemoMenus.Components.Borders, "Borders", url: "#"),
                new ApplicationMenuItem(PureThemeDemoMenus.Components.Breadcrumbs, "Breadcrumbs", url: "#"),
                new ApplicationMenuItem(PureThemeDemoMenus.Components.Buttons, "Buttons", url: "#"),
                new ApplicationMenuItem(PureThemeDemoMenus.Components.ButtonGroups, "Button Groups", url: "#"),
            };

        items.OrderBy(x => x.Name)
             .ToList()
             .ForEach(x => menuItem.AddItem(x));

        context.Menu.AddItem(menuItem);

        context.Menu.AddItem(new ApplicationMenuItem("like", "Like", "#"));

        var disabledLink = new ApplicationMenuItem("disabled", "Disabled", "#");
        disabledLink.IsDisabled = true;
        context.Menu.AddItem(disabledLink);


        /*******  multilevel menu  **********************************************************/
        var servicesMenuItem1 = new ApplicationMenuItem(
                "Services",
                "Services"
            );

        var servicesMenuItem2 = new ApplicationMenuItem(
                "ChildServices",
                "Child Services"
            );
        servicesMenuItem2.AddItem(new ApplicationMenuItem("WebDevelopment","Web Development", url: "#"));
        servicesMenuItem2.AddItem(new ApplicationMenuItem("Ecommerce","Ecommerce", url: "#"));
        servicesMenuItem2.AddItem(new ApplicationMenuItem("SoftwareDevelopment","Software Development", url: "#"));
        servicesMenuItem2.AddItem(new ApplicationMenuItem("DigniteSupport","Dignite Support", url: "#"));
        servicesMenuItem1.AddItem(servicesMenuItem2);


        var servicesMenuItem3 = new ApplicationMenuItem(
                "ChildServices",
                "Child Services"
            );
        servicesMenuItem3.AddItem(new ApplicationMenuItem("WebDevelopment","Web Development", url: "#"));
        servicesMenuItem3.AddItem(new ApplicationMenuItem("Ecommerce", "Ecommerce", url: "#"));
        servicesMenuItem3.AddItem(new ApplicationMenuItem("SoftwareDevelopment","Software Development", url: "#"));
        servicesMenuItem2.AddItem(new ApplicationMenuItem("DigniteSupport","Dignite Support", url: "#"));
        servicesMenuItem1.AddItem(servicesMenuItem3);

        servicesMenuItem1.AddItem(new ApplicationMenuItem("WebDevelopment","Web Development", url: "#"));
        servicesMenuItem1.AddItem(new ApplicationMenuItem("Ecommerce", "Ecommerce", url: "#"));
        servicesMenuItem1.AddItem(new ApplicationMenuItem("SoftwareDevelopment","Software Development", url: "#"));
        servicesMenuItem1.AddItem(new ApplicationMenuItem("DigniteSupport","Dignite Support", url: "#"));
        context.Menu.AddItem(servicesMenuItem1);
    }
    private void AddHomePageMenuItems(MenuConfigurationContext context)
    {
        var menuItem = new ApplicationMenuItem(PureThemeDemoMenus.Components.Root, "Components");

        var items = new List<ApplicationMenuItem>()
            {
                new ApplicationMenuItem(PureThemeDemoMenus.Components.Alerts, "Alerts", url: "#"),
                new ApplicationMenuItem(PureThemeDemoMenus.Components.Badges, "Badges", url: "#"),
                new ApplicationMenuItem(PureThemeDemoMenus.Components.Borders, "Borders", url: "#"),
                new ApplicationMenuItem(PureThemeDemoMenus.Components.Breadcrumbs, "Breadcrumbs", url: "#"),
                new ApplicationMenuItem(PureThemeDemoMenus.Components.Buttons, "Buttons", url: "#"),
                new ApplicationMenuItem(PureThemeDemoMenus.Components.ButtonGroups, "Button Groups", url: "#"),
            };

        items.OrderBy(x => x.Name)
             .ToList()
             .ForEach(x => menuItem.AddItem(x));

        context.Menu.AddItem(menuItem);

        context.Menu.AddItem(new ApplicationMenuItem("like", "Like", "#"));

        var disabledLink = new ApplicationMenuItem("disabled", "Disabled", "#");
        disabledLink.IsDisabled = true;
        context.Menu.AddItem(disabledLink);

    }
}