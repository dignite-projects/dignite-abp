using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Asp.Versioning;
using Dignite.Abp.AspNetCore.Mvc.Regionalization;
using Dignite.Abp.Regionalization;
using Dignite.Cms.Entries;
using Dignite.Cms.Localization;
using Dignite.Cms.Public.Entries;
using Dignite.Cms.Public.Sections;
using Dignite.Cms.Public.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
            return await Entry(null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task<IActionResult> Entry(string path)
        {
            var regionalization = await _regionalizationProvider.GetRegionalizationAsync();
            var defaultCultureName = regionalization.DefaultCulture.Name;
            if (regionalization.AvailableCultures.Count == 1)
            {
                return await GetEntryActionResult(defaultCultureName, path);
            }
            else
            {
                var cultureName = CultureInfo.CurrentCulture.Name;
                if (regionalization.AvailableCultures.Any(c => c.Name.Equals(cultureName, StringComparison.OrdinalIgnoreCase)))
                {
                    return RedirectToAction(nameof(CultureEntry), new { culture = cultureName, path });
                }
                else
                {
                    return RedirectToAction(nameof(CultureEntry), new { culture = defaultCultureName, path });
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="culture"></param>
        /// <param name="path">
        /// There are several formats:
        /// 1.{culture}
        /// 2.{culture}/{path}
        /// </param>
        /// <returns></returns>
        public async Task<IActionResult> CultureEntry(string culture, string path = null)
        {
            path = path ?? "/";
            return await GetEntryActionResult(culture, path);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="culture"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        protected async Task<IActionResult> GetEntryActionResult(string culture,string path="/")
        {
            path = path.EnsureStartsWith('/').EnsureEndsWith('/');

            if (path.EndsWith($"/{EntryConsts.DefaultSlug}/", StringComparison.OrdinalIgnoreCase))
            {
                path = path.Substring(0, path.Length - (EntryConsts.DefaultSlug.Length + 2)).Trim('/');
                return RedirectToAction(nameof(CultureEntry), new { culture, path });
            }

            var section = await GetSectionByEntityPath(path);
            if (section == null)
            {
                Logger.LogError("Section not found for path: {Path}", path);  
                return NotFound();
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
                Logger.LogError("Entry not found for path: {Path}, culture: {Culture}", path, culture);
                return NotFound();
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
