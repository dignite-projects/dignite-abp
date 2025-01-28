using Blazorise;
using Dignite.Cms.Admin.Sections;
using Dignite.Cms.Localization;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;

namespace Dignite.Cms.Admin.Blazor.Pages.Cms.Admin.Sections
{
    public partial class CreateEntryType
    {
        [Parameter] public Guid SectionId { get; set; }
        protected PageToolbar Toolbar { get; } = new();
        protected CreateEntryTypeInput NewEntity;
        protected Validations CreateValidationsRef;

        public CreateEntryType()
        {
            ObjectMapperContext = typeof(CmsAdminBlazorModule);
            LocalizationResource = typeof(CmsResource);
        }

        protected override Task OnInitializedAsync()
        {
            NewEntity = new CreateEntryTypeInput(SectionId);
            NewEntity.FieldTabs.Add(new EntryFieldTabInput(L["FieldTab"]));
            return base.OnInitializedAsync();
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
            Toolbar.AddButton(L["Save"],
                CreateEntityAsync,
                IconName.Save);
            await InvokeAsync(StateHasChanged);
        }

        protected virtual async Task CreateEntityAsync()
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
                    await AppService.CreateAsync(NewEntity);
                    Navigation.NavigateTo("cms/admin/sections");
                }
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }
    }
}
