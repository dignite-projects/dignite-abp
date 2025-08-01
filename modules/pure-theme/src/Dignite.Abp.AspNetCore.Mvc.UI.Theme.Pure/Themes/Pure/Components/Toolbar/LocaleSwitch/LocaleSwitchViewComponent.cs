using Dignite.Abp.AspNetCore.Locales.Routing;
using Dignite.Abp.Locales;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Themes.Pure.Components.Toolbar.LocaleSwitch;

[ViewComponent(Name = "Toolbar/LocaleSwitch")]
public class LocaleSwitchViewComponent : AbpViewComponent
{
    protected ILocaleRouteManager _localeRouteManager { get; }
    protected ILocaleProvider _localeProvider { get; }

    public LocaleSwitchViewComponent(
        ILocaleRouteManager localeRouteManager,
        ILocaleProvider localeProvider)
    {
        _localeRouteManager = localeRouteManager;
        _localeProvider = localeProvider;
    }

    public virtual async Task<IViewComponentResult> InvokeAsync()
    {
        var locale = await _localeProvider.GetLocaleAsync();
        var defaultRegionCultureName = locale.DefaultCulture.Name;
        var availableCultures = locale.AvailableCultures;
        var retionCultureNameInRoute = HttpContext.GetRouteValue(LocaleRouteDataRequestCultureProvider.LocaleRouteDataStringKey)?.ToString();
        var isMatchingLocaleRoute = _localeRouteManager.TryMatchUrl(HttpContext,out string routePattern);
        var currentRegionCultureName = retionCultureNameInRoute == null ?
            (isMatchingLocaleRoute ? defaultRegionCultureName : CultureInfo.CurrentCulture.Name) :
            availableCultures.FirstOrDefault(r => r.Name.Equals(retionCultureNameInRoute, System.StringComparison.OrdinalIgnoreCase))?.Name;

        var model = new LocaleSwitchViewComponentModel
        (
            defaultRegionCultureName,
            currentRegionCultureName,
            availableCultures,
            isMatchingLocaleRoute,
            routePattern
        );

        return View(model);
    }
}