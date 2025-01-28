using Blazorise;
using Dignite.Cms.Admin.Entries;
using Dignite.Cms.Admin.Sections;
using Dignite.Cms.Localization;
using Dignite.Cms.Permissions;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;

namespace Dignite.Cms.Admin.Blazor.Pages.Cms.Admin.Entries
{
    public partial class CreateEntry
    {
        [Parameter]
        [SupplyParameterFromQuery]
        public string CultureName { get; set; }

        [Parameter]
        [SupplyParameterFromQuery]
        public Guid SectionId { get; set; }

        [Parameter]
        [SupplyParameterFromQuery]
        public Guid EntryTypeId { get; set; }

        [Parameter]
        [SupplyParameterFromQuery]
        public Guid? RevisionEntryId { get; set; }
        protected CreateEntryInput NewEntity { get; set; }
        protected SectionDto Section { get; set; }
        protected PageToolbar Toolbar { get; } = new();

        protected Validations CreateValidationsRef;

        public CreateEntry()
        {
            ObjectMapperContext = typeof(CmsAdminBlazorModule);
            LocalizationResource = typeof(CmsResource);
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            if (RevisionEntryId.HasValue && RevisionEntryId.Value != default)
            {
                var revisionEntry = await EntryAppService.GetAsync(RevisionEntryId.Value);
                NewEntity = ObjectMapper.Map<EntryDto, CreateEntryInput>(revisionEntry);
                NewEntity.InitialVersionId = revisionEntry.InitialVersionId.HasValue ? revisionEntry.InitialVersionId.Value : revisionEntry.Id;
                NewEntity.PublishTime = Clock.Now;
                NewEntity.VersionNotes = "";
                SectionId = revisionEntry.SectionId;
                CultureName = revisionEntry.Culture;
                EntryTypeId = revisionEntry.EntryTypeId;
            }
            else
            {
                NewEntity = new CreateEntryInput()
                {
                    EntryTypeId = EntryTypeId,
                    PublishTime = Clock.Now,
                    Culture = CultureName,
                };
            }

            Section = await SectionAppService.GetAsync(SectionId);
        }


        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await SetToolbarItemsAsync();
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        protected async ValueTask SetToolbarItemsAsync()
        {
            Toolbar.AddButton(L["Cancel"],
                CancelAsync,
                color: Color.Light);

            Toolbar.AddButton(L["Save"],
                SaveAsDraftAsync,
                IconName.Save,
                color: Color.Info,
                requiredPolicyName: CmsAdminPermissions.Entry.Create);

            Toolbar.AddButton(L["Publish"],
                SaveAsync,
                IconName.PaperPlane,
                requiredPolicyName: CmsAdminPermissions.Entry.Create);
            await InvokeAsync(StateHasChanged);
        }

        protected async Task SaveAsync()
        {
            try
            {
                var validate = true;
                if (CreateValidationsRef != null)
                {
                    validate = await CreateValidationsRef.ValidateAll();
                }
                if (validate)
                {
                    await EntryAppService.CreateAsync(NewEntity);
                    Navigation.NavigateTo($"cms/admin/entries?sectionId={SectionId}&cultureName={NewEntity.Culture}");
                }
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        protected async Task SaveAsDraftAsync()
        {
            NewEntity.Draft = true;
            await SaveAsync();
        }
        protected Task CancelAsync()
        {
            Navigation.NavigateTo($"cms/admin/entries?sectionId={SectionId}&cultureName={NewEntity.Culture}");
            return Task.CompletedTask;
        }
    }
}
