using System.Linq;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Demo.Menus;
using Volo.Abp.UI.Navigation;

namespace Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Demo.Pages.Components;

public class IndexModel : AbpPageModel
{
    public readonly IMenuManager _menuManager;

    public IndexModel(IMenuManager menuManager)
    {
        _menuManager = menuManager;
    }

    public void OnGet()
    {

    }
}
