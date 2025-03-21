using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Themes.Pure.Components.Toolbar.ColorModeSwitch;
using Volo.Abp.UI.Navigation;

namespace Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Demo.Menus;

public class PureThemeDemoMenuContributor : IMenuContributor
{
    public Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == PureMenus.Main)
        {
            ConfigureMainMenuItems(context);
        }
        else if (context.Menu.Name == PureMenus.Shortcut)
        {
            ConfigureShortcutMenuAsync(context);
        }
        else if (context.Menu.Name == PureMenus.Footer)
        {
            ConfigureFooterMenuAsync(context);
        }

        return Task.CompletedTask;
    }

    private void ConfigureMainMenuItems(MenuConfigurationContext context)
    {
        context.Menu.AddItem(new ApplicationMenuItem(PureThemeDemoMenus.Home, "Home", "/", "fa fa-home"));

        var menuItem = new ApplicationMenuItem(PureThemeDemoMenus.Components.Root, "Components","#", "fab fa-buromobelexperte");

        var items = new List<ApplicationMenuItem>()
            {
                new ApplicationMenuItem(PureThemeDemoMenus.Components.Alerts, "Alerts", url: "/Alerts"),
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

    private Task ConfigureShortcutMenuAsync(MenuConfigurationContext context)
    {
        var learn = "Learn";
        var forDevelopersGroupName = "ForDevelopers";
        var productGroupName = "Products";
        context.Menu.AddGroup(new ApplicationMenuGroup(learn, "Learn"));
        context.Menu.AddGroup(new ApplicationMenuGroup(forDevelopersGroupName, "For Developers"));
        context.Menu.AddGroup(new ApplicationMenuGroup(productGroupName, "Products"));


        context.Menu.AddItem(
            new ApplicationMenuItem(
                "home",
                "Home",
                "~/",
                groupName: learn
            )
        );
        context.Menu.AddItem(
            new ApplicationMenuItem(
                "work",
                "Work",
                "~/work",
                groupName: learn
            )
        );
        context.Menu.AddItem(
            new ApplicationMenuItem(
                "blog",
                "Blog",
                "~/blog",
                groupName: learn
            )
        );
        context.Menu.AddItem(
            new ApplicationMenuItem(
                "about",
                "About",
                "~/about",
                groupName: learn
            )
        );
        context.Menu.AddItem(
            new ApplicationMenuItem(
                "contact-us",
                "Contact Us",
                "~/contact",
                groupName: learn
            )
        );

        context.Menu.AddItem(
            new ApplicationMenuItem(
                "dignite-abp",
                "Dignite Abp",
                "~/dignite-abp",
                groupName: forDevelopersGroupName
            )
        );
        context.Menu.AddItem(
            new ApplicationMenuItem(
                "dignite-cms",
                "Dignite Cms",
                "~/dignite-cms",
                groupName: forDevelopersGroupName
            )
        );

        context.Menu.AddItem(
            new ApplicationMenuItem(
                "web-design",
                "Web Design",
                "~/products/web-design",
                groupName: productGroupName
            )
        );
        context.Menu.AddItem(
            new ApplicationMenuItem(
                "seo",
                "SEO",
                "~/products/seo",
                groupName: productGroupName
            )
        );
        context.Menu.AddItem(
            new ApplicationMenuItem(
                "travely",
                "Travely",
                "~/products/travely",
                groupName: productGroupName
            )
        );
        
        context.Menu.AddItem(
            new ApplicationMenuItem(
                "all",
                "All",
                "~/products",
                groupName: productGroupName
            )
        );



        return Task.CompletedTask;
    }

    private Task ConfigureFooterMenuAsync(MenuConfigurationContext context)
    {
        context.Menu.AddItem(new ApplicationMenuItem("policy", "Privacy Policy", "/policy"));
        context.Menu.AddItem(new ApplicationMenuItem("sitemap", "Site Map", "/site-map"));
        context.Menu.AddItem(new ApplicationMenuItem("legal", "Legal", "/legal"));

        var menuItem = new ApplicationMenuItem(PureThemeDemoMenus.Components.Root, "Components");
        var items = new List<ApplicationMenuItem>()
            {
                new ApplicationMenuItem(PureThemeDemoMenus.Components.Alerts, "Alerts", url: "/Alerts"),
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


        context.Menu.Items.Add(
          new ApplicationMenuItem("color-mode-switch", "Color Mode Switch", "#")
            .UseComponent(typeof(ColorModeSwitchViewComponent))
            );

        return Task.CompletedTask;
    }
}