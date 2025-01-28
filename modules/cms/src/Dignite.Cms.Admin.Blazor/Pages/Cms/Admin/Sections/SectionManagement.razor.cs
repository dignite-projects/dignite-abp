using Blazorise;
using Dignite.Cms.Admin.Sections;
using Dignite.Cms.Localization;
using Dignite.Cms.Permissions;
using Dignite.Cms.Sections;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Components.Web.Extensibility.EntityActions;
using Volo.Abp.AspNetCore.Components.Web.Extensibility.TableColumns;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;

namespace Dignite.Cms.Admin.Blazor.Pages.Cms.Admin.Sections
{
    public partial class SectionManagement
    {
        protected PageToolbar Toolbar { get; } = new();
        protected List<TableColumn> SectionManagementTableColumns => TableColumns.Get<SectionManagement>();

        public SectionManagement()
        {
            ObjectMapperContext = typeof(CmsAdminBlazorModule);
            LocalizationResource = typeof(CmsResource);

            CreatePolicyName = CmsAdminPermissions.Section.Create;
            UpdatePolicyName = CmsAdminPermissions.Section.Update;
            DeletePolicyName = CmsAdminPermissions.Section.Delete;
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }



        protected override ValueTask SetToolbarItemsAsync()
        {
            Toolbar.AddButton(L["New"],
                OpenCreateModalAsync,
                IconName.Add,
                requiredPolicyName: CreatePolicyName);

            return base.SetToolbarItemsAsync();
        }

        protected override ValueTask SetEntityActionsAsync()
        {
            EntityActions
                .Get<SectionManagement>()
                .AddRange(new EntityAction[]
                {
                    new EntityAction
                    {
                        Text = L["Edit"],
                        Visible = (data) => HasUpdatePermission,
                        Clicked = async (data) => { await OpenEditModalAsync(data.As<SectionDto>()); }
                    },
                    new EntityAction
                    {
                        Text = L["Delete"],
                        Visible = (data) => HasDeletePermission,
                        Clicked = async (data) => await DeleteEntityAsync(data.As<SectionDto>()),
                        ConfirmationMessage = (data) => GetDeleteConfirmationMessage(data.As<SectionDto>())
                    }
                });

            return base.SetEntityActionsAsync();
        }

        protected override ValueTask SetTableColumnsAsync()
        {
            SectionManagementTableColumns
                .AddRange(new TableColumn[]
                {
                    new TableColumn
                    {
                        Title = L["DisplayName"],
                        Data = nameof(SectionDto.DisplayName)
                    },
                    new TableColumn
                    {
                        Title = L["Name"],
                        Data = nameof(SectionDto.Name)
                    },
                    new TableColumn
                    {
                        Title = L["SectionType"],
                        Sortable = true,
                        ValueConverter = (data)=> L[((SectionDto)data).Type.ToLocalizationKey()],
                        Data = nameof(SectionDto.Type)
                    },
                    new TableColumn
                    {
                        Title = L["IsDefault"],
                        Data = nameof(SectionDto.IsDefault)
                    },
                    new TableColumn
                    {
                        Title = L["IsActive"],
                        Sortable = true,
                        Data = nameof(SectionDto.IsActive)
                    },
                    new TableColumn
                    {
                        Title = L["EntryType"],
                        Data = nameof(SectionDto.EntryTypes),
                        Component = typeof(EntryTypeComponent)
                    },
                    new TableColumn
                    {
                        Title = L["Actions"],
                        Actions = EntityActions.Get<SectionManagement>()
                    },
                });

            return base.SetTableColumnsAsync();
        }
    }
}
