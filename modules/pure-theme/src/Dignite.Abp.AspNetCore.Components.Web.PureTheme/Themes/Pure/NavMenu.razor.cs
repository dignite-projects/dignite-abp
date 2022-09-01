using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Volo.Abp.UI.Navigation;

namespace Dignite.Abp.AspNetCore.Components.Web.PureTheme.Themes.Pure
{
    public partial class NavMenu
    {
        public IMenuManager MenuManager { get; set; }

        protected ApplicationMenu Menu { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Menu = await MenuManager.GetMainMenuAsync();
        }

        private string GetNavUrl(ApplicationMenuItem menuItem)
        {
            string url = string.Empty;
            if (menuItem.Url.IsNullOrEmpty())
            {
                if (menuItem.Items.Any())
                {
                    foreach (var children in menuItem.Items)
                    {
                        return GetNavUrl(children);
                    }
                }
            }
            else
                url = menuItem.Url;


            return url.TrimStart('/', '~');
        }
    }
}
