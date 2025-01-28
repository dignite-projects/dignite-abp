using Dignite.Cms.Public.Sections;
using Dignite.Cms.Public.Web.Razor;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace Dignite.Cms.Public.Web.TagHelpers
{
    public class CmsSectionTagHelper : TagHelper
    {
        public string SectionName{ get; set; }

        /// <summary>
        /// The name or path of the view that is rendered to the response.
        /// </summary>
        public string PartialName { get; set; }


        private readonly IRazorPartialRenderer _renderer;
        private readonly ISectionPublicAppService _sectionAppService;

        public CmsSectionTagHelper(
            IRazorPartialRenderer renderer,
            ISectionPublicAppService sectionAppService
            )
        {
            _renderer = renderer;
            _sectionAppService = sectionAppService;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var model = await _sectionAppService.FindByNameAsync(SectionName);
            var body = await _renderer.RenderAsync(PartialName, model);

            output.TagName = null;
            output.Content.SetHtmlContent(body);
            output.Attributes.Clear();
        }
    }
}
