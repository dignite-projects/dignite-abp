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
    public partial class EditEntry
    {
        [Parameter] public Guid Id { get; set; }


        protected EntryDto Entry{ get; set; }
        protected UpdateEntryInput EditingEntity { get; set; }
        protected SectionDto Section { get; set; }
        protected PageToolbar Toolbar { get; } = new();

        protected Validations EditValidationsRef;


        public EditEntry()
        {
            ObjectMapperContext = typeof(CmsAdminBlazorModule);
            LocalizationResource = typeof(CmsResource);
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            Entry = await EntryAppService.GetAsync(Id);
            Section = await SectionAppService.GetAsync(Entry.SectionId);
            EditingEntity = ObjectMapper.Map<EntryDto, UpdateEntryInput>(Entry);
        }


        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await SetToolbarItemsAsync();
            }
            await base.OnAfterRenderAsync(firstRender);
        }


        private async ValueTask SetToolbarItemsAsync()
        {
            Toolbar.AddButton(L["Cancel"],
                CancelAsync,
                color: Color.Light);

            Toolbar.AddButton(L["Save"],
                SaveAsDraftAsync,
                color: Color.Info,
                requiredPolicyName: CmsAdminPermissions.Entry.Update);

            Toolbar.AddButton(L["Publish"],
                SaveAsync,
                IconName.Save,
                requiredPolicyName: CmsAdminPermissions.Entry.Update);
            await InvokeAsync(StateHasChanged);
        }

        private async Task SaveAsync()
        {
            try
            {
                var validate = true;
                if (EditValidationsRef != null)
                {
                    validate = await EditValidationsRef.ValidateAll();
                }
                if (validate)
                {
                    await EntryAppService.UpdateAsync(Id, EditingEntity);
                    Navigation.NavigateTo($"cms/admin/entries?sectionId={Entry.SectionId}&cultureName={EditingEntity.Culture}");
                }
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }
        protected async Task SaveAsDraftAsync()
        {
            EditingEntity.Draft = true;
            await SaveAsync();
        }

        protected Task CancelAsync()
        {
            Navigation.NavigateTo($"cms/admin/entries?sectionId={Entry.SectionId}&cultureName={Entry.Culture}");
            return Task.CompletedTask;
        }
    }
}
