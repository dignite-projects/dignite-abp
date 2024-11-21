using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Alerts;

namespace Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Themes.Pure.Components.PageAlerts;

public class PageAlertsViewComponent : AbpViewComponent
{
    protected IAlertManager AlertManager { get; }

    public PageAlertsViewComponent(IAlertManager alertManager)
    {
        AlertManager = alertManager;
    }

    public IViewComponentResult Invoke(string name)
    {
        return View(AlertManager.Alerts);
    }
}
