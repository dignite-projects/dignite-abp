using Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure;
using Dignite.Cms.Public.Web.Localization;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.UI.Navigation;

namespace Dignite.Cms.Menus;

public class CmsWebHostMenuContributor : IMenuContributor
{
    private readonly IConfiguration _configuration;

    public CmsWebHostMenuContributor(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == PureMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
        else if (context.Menu.Name == PureMenus.Shortcut)
        {
            await ConfigureSiteMapMenuAsync(context);
        }
        else if (context.Menu.Name == StandardMenus.User)
        {
            AddLogoutItemToMenu(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<CmsPublicWebResource>();

        // Home
        context.Menu.AddItem(
            new ApplicationMenuItem(
                CmsWebHostMenus.HomePage,
                l["Menu:HomePage"],
                "~/",
                icon: "fa fa-home",
                order: 1
            )
            .SetSupportsLocalePrefix(true)
        );

        // service items
        var servicesMenuItem = new ApplicationMenuItem(
                CmsWebHostMenus.Services,
                l["Menu:Services"]
            );
        servicesMenuItem.AddItem(new ApplicationMenuItem(CmsWebHostMenus.Services_WebDesign, l["Menu:WebDesign"], url: "~/service/web-design").SetSupportsLocalePrefix(true));
        servicesMenuItem.AddItem(new ApplicationMenuItem(CmsWebHostMenus.Services_Ecommerce, l["Menu:eCommerce"], url: "~/service/ecommerce").SetSupportsLocalePrefix(true));
        context.Menu.AddItem(servicesMenuItem);


        // blog
        context.Menu.AddItem(new ApplicationMenuItem(CmsWebHostMenus.Blog, l["Menu:Blog"], "~/blog").SetSupportsLocalePrefix(true));


        // contact
        context.Menu.AddItem(new ApplicationMenuItem(CmsWebHostMenus.Contact, l["Menu:Contact"], "~/contact").SetSupportsLocalePrefix(true));

        // Razor page
        context.Menu.AddItem(new ApplicationMenuItem(CmsWebHostMenus.RazorPageTest, "Razor Page", "~/razor-page-test"));


        return Task.CompletedTask;
    }

    private Task ConfigureSiteMapMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<CmsPublicWebResource>();
        var serviceGroupName = "Menu:Services";
        var blogGroupName = "Menu:Blog";
        var learn = "Menu:Learn";
        context.Menu.AddGroup(new ApplicationMenuGroup(learn, l[$"{learn}"]));
        context.Menu.AddGroup(new ApplicationMenuGroup(serviceGroupName, l[$"{serviceGroupName}"]));
        context.Menu.AddGroup(new ApplicationMenuGroup(blogGroupName, l[$"{blogGroupName}"]));



        // learn
        context.Menu.AddItem(
            new ApplicationMenuItem(
                "Home",
                l["Menu:HomePage"],
                "~/",
                groupName: learn
            ).SetSupportsLocalePrefix(true)
        );
        context.Menu.AddItem(
            new ApplicationMenuItem(
                "Contact",
                l["Menu:Contact"],
                "~/contact",
                groupName: learn
            ).SetSupportsLocalePrefix(true)
        );

        // services
        context.Menu.AddItem(
            new ApplicationMenuItem(
                "web-design",
                l["Menu:WebDesign"],
                "~/service/web-design",
                groupName: serviceGroupName
            ).SetSupportsLocalePrefix(true)
        );
        context.Menu.AddItem(
            new ApplicationMenuItem(
                "ecommerce",
                l["Menu:eCommerce"],
                "~/service/ecommerce",
                groupName: serviceGroupName
            ).SetSupportsLocalePrefix(true)
        );


        /* Blog */
        context.Menu.AddItem(
            new ApplicationMenuItem(
                "blog",
                l["Menu:Blog-All"],
                "~/blog",
                groupName: blogGroupName
            ).SetSupportsLocalePrefix(true)
        );
        context.Menu.AddItem(
            new ApplicationMenuItem(
                "blog-company-news",
                l["Menu:Blog-company-news"],
                "~/blog?category=company-news",
                groupName: blogGroupName
            ).SetSupportsLocalePrefix(true)
        );
        context.Menu.AddItem(
            new ApplicationMenuItem(
                "blog-tutorials",
                l["Menu:Blog-tutorials"],
                "~/blog?category=tutorials",
                groupName: blogGroupName
            ).SetSupportsLocalePrefix(true)
        );
        context.Menu.AddItem(
            new ApplicationMenuItem(
                "blog-essays",
                l["Menu:Blog-essays"],
                "~/blog?category=essays",
                groupName: blogGroupName
            ).SetSupportsLocalePrefix(true)
        );

        return Task.CompletedTask;
    }

    private void AddLogoutItemToMenu(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<Localization.CmsResource>();

        context.Menu.Items.Add(new ApplicationMenuItem(
            "Account.Manage",
            l["MyAccount"],
            $"{_configuration["AuthServer:Authority"].EnsureEndsWith('/')}Account/Manage",
            icon: "fa fa-cog",
            order: int.MaxValue - 1001,
            null,
            "_blank"
        ).RequireAuthenticated());

        context.Menu.Items.Add(new ApplicationMenuItem(
            "Account.Logout",
            l["Logout"],
            "~/Account/Logout",
            "fas fa-power-off",
            order: int.MaxValue - 1000
        ).RequireAuthenticated());
    }
}
