using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.UI.Navigation;

namespace Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Themes.Pure.Components.Footer.FooterLinks;

[ViewComponent(Name = "Footer/FooterLinks")]
public class FooterLinksViewComponent : AbpViewComponent
{
    protected IMenuManager MenuManager { get; }

    public FooterLinksViewComponent(IMenuManager menuManager)
    {
        MenuManager = menuManager;
    }

    public virtual async Task<IViewComponentResult> InvokeAsync()
    {
        var menu = await MenuManager.GetAsync(PureMenus.Footer);
        return View(menu);
    }
}