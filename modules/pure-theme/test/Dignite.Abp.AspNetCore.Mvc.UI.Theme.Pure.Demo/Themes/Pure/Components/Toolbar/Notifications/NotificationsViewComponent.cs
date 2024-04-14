using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Themes.Pure.Demo.Components.Toolbar.Notifications;

[ViewComponent(Name = "Toolbar/Notifications")]
public class NotificationsViewComponent : AbpViewComponent
{

    public virtual IViewComponentResult Invoke()
    {
        return View();
    }
}