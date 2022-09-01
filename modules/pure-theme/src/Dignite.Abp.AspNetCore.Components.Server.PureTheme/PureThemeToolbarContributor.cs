using System.Threading.Tasks;
using Dignite.Abp.AspNetCore.Components.Server.PureTheme.Themes.Pure;
using Volo.Abp.AspNetCore.Components.Web.Theming.Toolbars;

namespace Dignite.Abp.AspNetCore.Components.Server.PureTheme
{
    public class PureThemeToolbarContributor : IToolbarContributor
    {
        public Task ConfigureToolbarAsync(IToolbarConfigurationContext context)
        {
            if (context.Toolbar.Name == StandardToolbars.Main)
            {
                context.Toolbar.Items.Add(new ToolbarItem(typeof(LanguageSwitch)));
                context.Toolbar.Items.Add(new ToolbarItem(typeof(LoginDisplay)));
            }

            return Task.CompletedTask;
        }
    }
}
