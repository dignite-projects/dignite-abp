using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Themes.Pure.Components.Footer;
public class FooterViewComponent : AbpViewComponent
{
    public virtual IViewComponentResult Invoke()
    {
        return View();
    }
}