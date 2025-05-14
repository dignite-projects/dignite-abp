using Dignite.Abp.AspNetCore.Mvc.Regionalization.Routing;
using Dignite.Abp.Regionalization;
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
    protected IRegionalizationRouteManager _regionalizationRouteManager { get; }
    protected IRegionalizationProvider _regionalizationProvider { get; }

    public LocaleSwitchViewComponent(
        IRegionalizationRouteManager regionalizationRouteManager,
        IRegionalizationProvider regionalizationProvider)
    {
        _regionalizationRouteManager = regionalizationRouteManager;
        _regionalizationProvider = regionalizationProvider;
    }

    public virtual async Task<IViewComponentResult> InvokeAsync()
    {
        var regionalization = await _regionalizationProvider.GetRegionalizationAsync();
        var defaultRegionCultureName = regionalization.DefaultCulture.Name;
        var availableCultures = regionalization.AvailableCultures;
        var retionCultureNameInRoute = HttpContext.GetRouteValue(RegionalizationRouteDataRequestCultureProvider.RegionalizationRouteDataStringKey)?.ToString();
        var isMatchingRegionalizationRoute = _regionalizationRouteManager.TryMatchUrl(HttpContext,out string routePattern);
        var currentRegionCultureName = retionCultureNameInRoute == null ?
            (isMatchingRegionalizationRoute ? defaultRegionCultureName : CultureInfo.CurrentCulture.Name) :
            availableCultures.FirstOrDefault(r => r.Name.Equals(retionCultureNameInRoute, System.StringComparison.OrdinalIgnoreCase))?.Name;

        var model = new LocaleSwitchViewComponentModel
        (
            defaultRegionCultureName,
            currentRegionCultureName,
            availableCultures,
            isMatchingRegionalizationRoute,
            routePattern
        );

        return View(model);
    }
}