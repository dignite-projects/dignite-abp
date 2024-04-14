using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Toolbars;

namespace Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Components.BottomToolbar;

public class BottomToolbarViewComponent : AbpViewComponent
{
    protected IToolbarManager ToolbarManager { get; }

    public BottomToolbarViewComponent(IToolbarManager toolbarManager)
    {
        ToolbarManager = toolbarManager;
    }

    public virtual async Task<IViewComponentResult> InvokeAsync()
    {
        var toolbar = await ToolbarManager.GetAsync(PureToolbars.BottomNavigationBar);
        return View(toolbar);
    }
}