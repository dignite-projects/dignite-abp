using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Toolbars;

namespace Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Components.NavbarToolbar;

public class NavbarToolbarViewComponent : AbpViewComponent
{
    protected IToolbarManager ToolbarManager { get; }

    public NavbarToolbarViewComponent(IToolbarManager toolbarManager)
    {
        ToolbarManager = toolbarManager;
    }

    public virtual async Task<IViewComponentResult> InvokeAsync()
    {
        var toolbar = await ToolbarManager.GetAsync(StandardToolbars.Main);
        return View(toolbar);
    }
}