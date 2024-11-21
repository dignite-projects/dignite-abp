using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Layout;

namespace Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Themes.Pure.Components.BreadCrumbs;

public class BreadCrumbsViewComponent : AbpViewComponent
{
    protected IPageLayout PageLayout { get; }

    public BreadCrumbsViewComponent(IPageLayout pageLayout)
    {
        PageLayout = pageLayout;
    }

    public virtual IViewComponentResult Invoke()
    {
        return View(PageLayout.Content);
    }
}
