using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace Dignite.Publisher.Public.Web.Menus;

public class PublisherPublicMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        //Add main menu items.
        context.Menu.AddItem(new ApplicationMenuItem(PublisherPublicMenus.Prefix, displayName: "Public", "~/Public", icon: "fa fa-globe"));

        return Task.CompletedTask;
    }
}
