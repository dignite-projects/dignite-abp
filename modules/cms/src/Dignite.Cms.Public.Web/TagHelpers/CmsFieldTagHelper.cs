using Dignite.Abp.Data;
using Dignite.Abp.DynamicForms;
using Dignite.Cms.Public.Web.Models;
using Dignite.Cms.Public.Web.Razor;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Threading.Tasks;

namespace Dignite.Cms.Public.Web.TagHelpers
{
    public class CmsFieldTagHelper : TagHelper
    {
        private const string filedFolder = "Fields";

        public FormField Field { get; set; }

        public IHasCustomFields Entry { get; set; }

        /// <summary>
        /// The name or path of the view that is rendered to the response.
        /// </summary>
        public string PartialName { get; set; }


        private readonly IRazorPartialRenderer _renderer;

        public CmsFieldTagHelper(
            IRazorPartialRenderer renderer
            )
        {
            _renderer = renderer;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var body = await _renderer.RenderAsync(
                GetPartialName(Field.FormControlName), 
                new EntryFieldViewModel(Field, Entry)
                );
            output.TagName = null;
            output.Content.SetHtmlContent(body);
        }

        private string GetPartialName(string fieldTypeName)
        {
            if (!string.IsNullOrEmpty(PartialName))
            {
                return PartialName;
            }
            else
            {
                return filedFolder.EnsureEndsWith('/') + fieldTypeName;
            }
        }
    }
}
