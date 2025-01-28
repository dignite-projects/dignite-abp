using Blazorise;
using Dignite.Cms.Admin.Fields;
using Dignite.Cms.Localization;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;

namespace Dignite.Cms.Admin.Blazor.Pages.Cms.Admin.Fields
{
    public partial class EditField
    {
        [Parameter] public Guid Id{ get; set; }
        protected PageToolbar Toolbar { get; } = new();
        protected UpdateFieldInput EditingEntity;
        protected Validations EditValidationsRef;

        public EditField()
        {
            ObjectMapperContext = typeof(CmsAdminBlazorModule);
            LocalizationResource = typeof(CmsResource);
        }
        protected override async Task OnInitializedAsync()
        {
            var fieldDto = await AppService.GetAsync(Id);
            EditingEntity = ObjectMapper.Map<FieldDto, UpdateFieldInput>(fieldDto);
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
                UpdateEntityAsync,
                IconName.Save);
            await InvokeAsync(StateHasChanged);
        }

        protected virtual async Task UpdateEntityAsync()
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
                    await AppService.UpdateAsync(Id,EditingEntity);
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
