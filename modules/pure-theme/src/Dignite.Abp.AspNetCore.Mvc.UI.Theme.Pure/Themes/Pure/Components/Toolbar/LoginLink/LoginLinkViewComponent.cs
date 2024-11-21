using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Themes.Pure.Components.Toolbar.LoginLink;

[ViewComponent(Name = "Toolbar/LoginLink")]
public class LoginLinkViewComponent : AbpViewComponent
{
    public virtual IViewComponentResult Invoke()
    {
        return View();
    }
}
