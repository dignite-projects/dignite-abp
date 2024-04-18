using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.UI.Navigation;

namespace Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Components.PageFooter;

public class PageFooterViewComponent : AbpViewComponent
{
    private readonly IMenuManager _menuManager;

    public PageFooterViewComponent(IMenuManager menuManager)
    {
        _menuManager = menuManager;
    }



    public virtual async Task<IViewComponentResult> InvokeAsync()
    {
        var menu = await _menuManager.GetAsync(PureMenus.SiteMap);
        return View(menu);
    }
}