using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.UI.Navigation;

namespace Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Themes.Pure.Components.Footer.ShortcutMenu;

[ViewComponent(Name = "Footer/ShortcutMenu")]
public class ShortcutMenuViewComponent : AbpViewComponent
{
    protected IMenuManager MenuManager { get; }

    public ShortcutMenuViewComponent(IMenuManager menuManager)
    {
        MenuManager = menuManager;
    }

    public virtual async Task<IViewComponentResult> InvokeAsync()
    {
        var menu = await MenuManager.GetAsync(PureMenus.Shortcut);
        return View(menu);
    }
}