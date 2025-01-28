using Blazorise;
using Dignite.Cms.Admin.DynamicForms;
using Dignite.Cms.Admin.Fields;
using Dignite.Cms.Localization;
using Dignite.Cms.Permissions;
using Dignite.FileExplorer.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Components.Web.Extensibility.EntityActions;
using Volo.Abp.AspNetCore.Components.Web.Extensibility.TableColumns;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;

namespace Dignite.Cms.Admin.Blazor.Pages.Cms.Admin.Fields
{
    public partial class FieldManagement
    {
        protected PageToolbar Toolbar { get; } = new();
        protected List<TableColumn> FieldManagementTableColumns => TableColumns.Get<FieldManagement>();
        protected IReadOnlyList<FormControlDto> AllFormControls { get; set; } = new List<FormControlDto>();

        public FieldManagement()
        {
            ObjectMapperContext = typeof(CmsAdminBlazorModule);
            LocalizationResource = typeof(CmsResource);

            CreatePolicyName = CmsAdminPermissions.Field.Create;
            UpdatePolicyName = CmsAdminPermissions.Field.Update;
            DeletePolicyName = CmsAdminPermissions.Field.Delete;
        }

        protected override async Task OnInitializedAsync()
        {
            AllFormControls = (await FormService.GetFormControlsAsync()).Items;
            await base.OnInitializedAsync();
        }

        protected override ValueTask SetToolbarItemsAsync()
        {
            Toolbar.AddButton(L["New"],
                async () => { 
                    Navigation.NavigateTo("cms/admin/fields/create"); 
                    await Task.CompletedTask; 
                },
                IconName.Add,
                requiredPolicyName: CreatePolicyName);

            return base.SetToolbarItemsAsync();
        }

        protected override ValueTask SetEntityActionsAsync()
        {
            EntityActions
                .Get<FieldManagement>()
                .AddRange(new EntityAction[]
                {
                    new EntityAction
                    {
                        Text = L["Edit"],
                        Visible = (data) => HasUpdatePermission,
                        Clicked = async (data) => {
                            Navigation.NavigateTo($"cms/admin/fields/{data.As<FieldDto>().Id}/edit");
                            await Task.CompletedTask;
                        }
                    },
                    new EntityAction
                    {
                        Text = L["Delete"],
                        Visible = (data) => HasDeletePermission,
                        Clicked = async (data) => await DeleteEntityAsync(data.As<FieldDto>()),
                        ConfirmationMessage = (data) => GetDeleteConfirmationMessage(data.As<FieldDto>())
                    }
                });

            return base.SetEntityActionsAsync();
        }

        protected override ValueTask SetTableColumnsAsync()
        {
            FieldManagementTableColumns
                .AddRange(new TableColumn[]
                {
                    new TableColumn
                    {
                        Title = L["DisplayName"],
                        Sortable = true,
                        Data = nameof(FieldDto.DisplayName)
                    },
                    new TableColumn
                    {
                        Title = L["Name"],
                        Sortable = true,
                        Data = nameof(FieldDto.Name)
                    },
                    new TableColumn
                    {
                        Title = L["FormControlName"],
                        Sortable = true,
                        ValueConverter = (data)=> AllFormControls.FirstOrDefault(fc=>fc.Name == data.As<FieldDto>().FormControlName)?.DisplayName,
                        Data = nameof(FieldDto.FormControlName)
                    },
                    new TableColumn
                    {
                        Title = L["Group"],
                        Sortable = true,
                        ValueConverter = (data)=> data.As<FieldDto>().GroupName,
                        Data = nameof(FieldDto.GroupId)
                    },
                    new TableColumn
                    {
                        Title = L["CreationTime"],
                        Sortable = true,
                        Data = nameof(FieldDto.CreationTime)
                    },
                    new TableColumn
                    {
                        Title = L["Actions"],
                        Actions = EntityActions.Get<FieldManagement>()
                    },
                });

            return base.SetTableColumnsAsync();
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        protected async Task OnGroupChanged(FieldGroupDto group)
        {
            GetListInput.Filter = null; 
            GetListInput.GroupId = group==null?null: group.Id;
            await SearchEntitiesAsync();
        }
    }
}
