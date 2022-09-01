using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace Dignite.Abp.NotificationCenter.Blazor.Menus
{
    public class NotificationCenterMenuContributor : IMenuContributor
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
            context.Menu.AddItem(new ApplicationMenuItem(NotificationCenterMenus.Prefix, displayName: "NotificationCenter", "/NotificationCenter", icon: "fa fa-globe"));
            
            return Task.CompletedTask;
        }
    }
}