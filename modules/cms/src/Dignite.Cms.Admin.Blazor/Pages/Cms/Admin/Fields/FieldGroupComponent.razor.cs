using Dignite.Cms.Admin.Fields;
using Dignite.Cms.Localization;
using Dignite.Cms.Permissions;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Dignite.Cms.Admin.Blazor.Pages.Cms.Admin.Fields
{
    public partial class FieldGroupComponent
    {
        [Parameter]
        public EventCallback<FieldGroupDto> OnClickCallback { get; set; }

        protected string SelectedGroupName = "";

        public FieldGroupComponent()
        {
            ObjectMapperContext = typeof(CmsAdminBlazorModule);
            LocalizationResource = typeof(CmsResource);

            CreatePolicyName = CmsAdminPermissions.Field.Create;
            UpdatePolicyName = CmsAdminPermissions.Field.Update;
            DeletePolicyName = CmsAdminPermissions.Field.Delete;
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await SearchEntitiesAsync();
        }

        public void OnItemClick(FieldGroupDto group)
        {
            OnClickCallback.InvokeAsync(group);
        }
    }
}
