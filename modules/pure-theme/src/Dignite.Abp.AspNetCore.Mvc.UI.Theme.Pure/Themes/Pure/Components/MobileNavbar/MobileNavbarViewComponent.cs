using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Toolbars;
using Volo.Abp.UI.Navigation;

namespace Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Themes.Pure.Components.MobileNavbar;

[ViewComponent(Name = "MobileNavbar")]
public class MobileNavbarViewComponent : AbpViewComponent
{
    protected IMenuManager MenuManager { get; }
    protected IToolbarManager ToolbarManager { get; }
    protected PureThemeMvcOptions Options { get; }

    public MobileNavbarViewComponent(IMenuManager menuManager, IToolbarManager toolbarManager, IOptions<PureThemeMvcOptions> options)
    {
        MenuManager = menuManager;
        ToolbarManager = toolbarManager;
        Options = options.Value;
    }

    public virtual async Task<IViewComponentResult> InvokeAsync()
    {
        var menu = await MenuManager.GetMainMenuAsync();
        var toolbar = await ToolbarManager.GetAsync(StandardToolbars.Main);
        var model = new MobileNavbarViewModel(
            Options.MobileMenuSelector(menu.Items?.AsReadOnly()).ToList(),
            Options.MobileToolbarSelector(toolbar.Items?.AsReadOnly()).ToList());
        return View(model);
    }
}