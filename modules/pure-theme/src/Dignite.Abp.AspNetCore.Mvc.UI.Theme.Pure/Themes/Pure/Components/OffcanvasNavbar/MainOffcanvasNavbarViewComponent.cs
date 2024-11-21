using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Themes.Pure.Components.OffcanvasNavbar;

[ViewComponent(Name = "OffcanvasNavbar")]
public class MainOffcanvasNavbarViewComponent : AbpViewComponent
{
    public virtual IViewComponentResult Invoke()
    {
        return View();
    }
}