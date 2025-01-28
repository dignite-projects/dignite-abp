using Blazorise;
using Dignite.Abp.DynamicForms.TextEdit;
using Dignite.Cms.Admin.Fields;
using Dignite.Cms.Localization;
using System;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;

namespace Dignite.Cms.Admin.Blazor.Pages.Cms.Admin.Fields
{
    public partial class CreateField
    {
        protected PageToolbar Toolbar { get; } = new();
        protected CreateFieldInput NewEntity = new CreateFieldInput 
        { 
            FormControlName = TextEditFormControl.ControlName 
        };
        protected Validations CreateValidationsRef;

        public CreateField()
        {
            ObjectMapperContext = typeof(CmsAdminBlazorModule);
            LocalizationResource = typeof(CmsResource);
        }

        protected async override Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
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
                    Navigation.NavigateTo("cms/admin/fields");
                }
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }
    }
}
