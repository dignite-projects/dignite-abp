using Dignite.Abp.DynamicForms;
using Dignite.Cms.Public.Sections;
using Dignite.Cms.Public.Web.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;
using Dignite.Abp.Data;
using Dignite.Abp.AspNetCore.Mvc.Razor;

namespace Dignite.Cms.Public.Web.TagHelpers
{
    public class CmsEntryFieldTagHelper : TagHelper
    {

        public string FieldName { get; set; }

        public EntryViewModel Entry { get; set; }

        /// <summary>
        /// The name or path of the view that is rendered to the response.
        /// </summary>
        public string PartialName { get; set; }


        private readonly IRazorPartialRenderer _renderer;

        public CmsEntryFieldTagHelper(
            IRazorPartialRenderer renderer
            )
        {
            _renderer = renderer;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var field = Entry.Section.GetField(Entry.Entry.EntryTypeId, FieldName);
            if (field == null)
            {
                output.TagName = "p";
                output.Attributes.Add("class", "p-2 bg-warning text-dark");
                output.Content.SetContent($"There is no field with the name {FieldName}.");
            }
            else
            {
                var tagHelper = new CmsFieldTagHelper(_renderer);
                tagHelper.Field = new FormField(field.Name,field.DisplayName,field.Description,field.FormControlName,field.FormConfiguration,false,Entry.Entry.GetField(field.Name));
                tagHelper.Entry = Entry.Entry;
                tagHelper.PartialName = PartialName;
                await tagHelper.ProcessAsync(context, output);
            }
        }
    }
}
