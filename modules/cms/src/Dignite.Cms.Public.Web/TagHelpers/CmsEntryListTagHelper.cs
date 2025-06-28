using Dignite.Abp.Data;
using Dignite.Cms.Public.Entries;
using Dignite.Cms.Public.Sections;
using Dignite.Cms.Public.Web.Models;
using Dignite.Cms.Public.Web.Razor;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dignite.Cms.Public.Web.TagHelpers
{
    public class CmsEntryListTagHelper: TagHelper
    {
        /// <summary>
        /// 
        /// </summary>
        public SectionDto Section { get; set; }

        /// <summary>
        /// If no <see cref="Section"/> is specified, entries can be queried by <see cref="SectionName"/>
        /// </summary>
        public string SectionName{ get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Culture { get; set; }

        public Guid? EntryTypeId { get; set; }

        public Guid? CreatorId { get; set; }

        public DateTime? StartPublishDate { get; set; }

        public DateTime? ExpiryPublishDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IList<QueryingByField> QueryingByFields { get; set; }

        /// <summary>
        /// The name or path of the view that is rendered to the response.
        /// </summary>
        public string PartialName { get; set; }


        /// <summary>
        /// number of returned results;
        /// When the <see cref="Section"/> type is SectionType.Channel, the number of returned results must be designed
        /// </summary>
        public int MaxResultCount { get; set; } = 20;

        /// <summary>
        /// number of data paged
        /// default value : 1
        /// </summary>
        public int CurrentPage { get; set; } = 1;


        private readonly IRazorPartialRenderer _renderer;
        private readonly IEntryPublicAppService _entryAppService;
        private readonly ISectionPublicAppService _sectionAppService;

        public CmsEntryListTagHelper(
            IRazorPartialRenderer renderer,
            IEntryPublicAppService entryAppService,
            ISectionPublicAppService sectionAppService
            )
        {
            _renderer = renderer;
            _entryAppService = entryAppService;
            _sectionAppService = sectionAppService;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (Section == null)
            {
                Section = await _sectionAppService.FindByNameAsync(SectionName);
                if (Section==null)
                {
                    output.TagName = "p";
                    output.Attributes.Add("class", "p-2 bg-warning text-dark");
                    output.Content.SetContent($"The section named {SectionName} is null. Please check if the {SectionName} name is valid");
                    return;
                }
            }

            var model = await GetViewModel();
            var body = await _renderer.RenderAsync(PartialName, model);

            output.TagName = null;
            output.Content.SetHtmlContent(body);
            output.Attributes.Clear();
        }

        protected async Task<EntryListViewModel> GetViewModel()
        {
            var result = await _entryAppService.GetListAsync(new GetEntriesInput
            {
                SectionId = Section.Id,
                Culture = Culture,
                EntryTypeId = EntryTypeId,
                CreatorId = CreatorId,
                StartPublishDate = StartPublishDate,
                ExpiryPublishDate = ExpiryPublishDate,
                QueryingByFieldsJson = QueryingByFields == null ? null : JsonSerializer.Serialize(QueryingByFields),
                MaxResultCount = this.MaxResultCount,
                SkipCount = (this.CurrentPage - 1) * MaxResultCount
            });


            var model = new EntryListViewModel(Section, result.Items, (int)result.TotalCount, CurrentPage, MaxResultCount);
            return model;
        }

    }
}
