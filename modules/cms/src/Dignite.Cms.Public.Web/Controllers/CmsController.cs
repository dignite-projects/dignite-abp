using Asp.Versioning;
using Dignite.Abp.AspNetCore.Mvc.Regionalization;
using Dignite.Abp.AspNetCore.Mvc.Regionalization.Routing;
using Dignite.Abp.Regionalization;
using Dignite.Cms.Entries;
using Dignite.Cms.Localization;
using Dignite.Cms.Public.Entries;
using Dignite.Cms.Public.Sections;
using Dignite.Cms.Public.Web.Models;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Text.Formatting;

namespace Dignite.Cms.Public.Web.Controllers
{
    [ControllerName(ControllerName)]
    public class CmsController : AbpController, IRegionalizationRouteable
    {
        public const string ControllerName = "Cms";

        private readonly IRegionalizationProvider _regionalizationProvider;
        private readonly ISectionPublicAppService _sectionPublicAppService;
        private readonly IEntryPublicAppService _entryPublicAppService;

        public CmsController(IRegionalizationProvider regionalizationProvider, ISectionPublicAppService sectionPublicAppService, IEntryPublicAppService entryPublicAppService)
        {
            LocalizationResource = typeof(CmsResource);
            _regionalizationProvider = regionalizationProvider;
            _sectionPublicAppService = sectionPublicAppService;
            _entryPublicAppService = entryPublicAppService;
        }

        public async Task<IActionResult> Default()
        {
            return await GetEntryActionResult();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task<IActionResult> Entry(string path)
        {
            return await GetEntryActionResult(null, path);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="culture"></param>
        /// <param name="path">
        /// There are several formats:
        /// 1.{culture}
        /// 2.{culture}/{route}
        /// </param>
        /// <returns></returns>
        public async Task<IActionResult> CultureEntry(string culture, string path)
        {
            return await GetEntryActionResult(culture, path);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="culture"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        protected async Task<IActionResult> GetEntryActionResult(string culture = null, string path = "/")
        {
            path = path.IsNullOrEmpty() ? "/" : path.EnsureStartsWith('/');
            var section = await GetSectionByEntityPath(path);
            if (section == null)
            {
                Logger.LogError("Section not found for path: {Path}", path);  
                return NotFound();
            }

            //
            var regionalization = await _regionalizationProvider.GetRegionalizationAsync();
            var defaultCultureName = regionalization.DefaultCulture.Name;
            if (culture.IsNullOrEmpty())
            {
                culture = defaultCultureName;
            }
            else
            {
                /* Remove the default culture prefix and redirect to the new route.
                 * Example: the default culture is en, the current request route is /en/about, will jump to /about route
                 */
                if (culture.Equals(defaultCultureName, StringComparison.OrdinalIgnoreCase) 
                    && Request.Path.Value.EnsureEndsWith('/').StartsWith($"/{culture}/"))
                {
                    return LocalRedirectPermanent(Request.GetEncodedPathAndQuery().RemovePreFix($"/{culture}").EnsureStartsWith('/').EnsureStartsWith('~'));
                }

                if (!culture.Equals(defaultCultureName, StringComparison.OrdinalIgnoreCase) 
                    && !Request.RouteValues.Any(r=>r.Key.Equals(RegionalizationRouteDataRequestCultureProvider.RegionalizationRouteDataStringKey,StringComparison.OrdinalIgnoreCase)))
                {
                    return Redirect(culture.ToLower());
                }
            }

            //
            var entry = await GetEntry(culture, path, section);
            if (entry != null)
            {
                var viewModel = new EntryViewModel(entry, section);
                return View(section.Template, viewModel);
            }
            else
            {
                if (!culture.Equals(defaultCultureName, StringComparison.OrdinalIgnoreCase))
                {
                    return Redirect(path);
                }
                else
                {
                    Logger.LogError("Entry not found for path: {Path}, culture: {Culture}", path, culture);
                    return NotFound();
                }
            }
        }

        protected async Task<SectionDto> GetSectionByEntityPath(string entityPath)
        {
            if (entityPath.IsNullOrEmpty() || entityPath == "/")
            {
                return await _sectionPublicAppService.GetDefaultAsync();
            }
            else
            {
                return await _sectionPublicAppService.FindByEntityPathAsync(entityPath);
            }
        }

        protected async Task<EntryDto> GetEntry(string culture, string entityPath, SectionDto section)
        {
            var slug = ExtractSlug(section.Route, entityPath);
            if (slug.IsNullOrEmpty())
            {
                slug = EntryConsts.DefaultSlug;
            }

            EntryDto entry = await _entryPublicAppService.FindBySlugAsync(
                                                            new FindBySlugInput
                                                                {
                                                                    Culture = culture,
                                                                    SectionId = section.Id,
                                                                    Slug = slug
                                                                });
            return entry;
        }



        protected virtual string ExtractSlug(string sectionRoute, string entryPath)
        {
            string slug = null;
            //Extract Slug value from entryRoute
            var extractResult = FormattedStringValueExtracter.Extract(entryPath.Trim('/'), sectionRoute.Trim('/'), ignoreCase: true);
            if (extractResult.IsMatch)
            {
                slug = extractResult.Matches.FirstOrDefault(m => m.Name.Equals(nameof(EntryDto.Slug), StringComparison.InvariantCultureIgnoreCase))?.Value;
            }

            return slug;
        }
    }
}
