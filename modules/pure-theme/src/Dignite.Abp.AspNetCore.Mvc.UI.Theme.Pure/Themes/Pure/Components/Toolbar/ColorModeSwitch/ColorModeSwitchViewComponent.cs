using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Themes.Pure.Components.Toolbar.ColorModeSwitch;

[ViewComponent(Name = "Toolbar/ColorModeSwitch")]
public class ColorModeSwitchViewComponent : AbpViewComponent
{
    public virtual IViewComponentResult Invoke()
    {
        return View();
    }
}
