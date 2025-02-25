using Blazorise;
using Blazorise.Extensions;
using Dignite.Cms.Admin.Fields;
using Dignite.Cms.Admin.Sections;
using Dignite.Cms.Localization;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Dignite.Cms.Admin.Blazor.Pages.Cms.Admin.Sections
{
    public partial class CreateOrUpdateEntryTypeComponent
    {
        [Parameter] public CreateOrUpdateEntryTypeInputBase Entity { get; set; }
        [Parameter] public Guid SectionId{ get; set; }

        protected Modal CreateModal;
        protected Modal EditModal;
        protected Modal EditFieldModal;

        protected Validations CreateValidationsRef;
        protected Validations EditValidationsRef;
        protected Validations EditFieldValidationsRef;

        /// <summary>
        /// 
        /// </summary>
        protected Guid? DraggingFieldId;

        protected IReadOnlyList<FieldGroupDto> FieldGroups { get; set; } = new List<FieldGroupDto>();
        protected IReadOnlyList<FieldDto> AllFields { get; set; }=new List<FieldDto>();

        private string SelectedFieldTabName;

        private EntryFieldTabInput NewFieldTab = new();
        private EntryFieldTabInput EditingFieldTab = new();
        private EntryFieldInput EditingField = new();

        //Will not change again after assignment, used to verify that the site name already exists
        private string entryTypeNameForValidation;

        public CreateOrUpdateEntryTypeComponent()
        {
            LocalizationResource = typeof(CmsResource);
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            entryTypeNameForValidation = Entity.Name;
            SelectedFieldTabName = Entity.FieldTabs.First().Name;
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            FieldGroups = (await FieldGroupAppService.GetListAsync(new GetFieldGroupsInput())).Items;
            AllFields = (await FieldAppService.GetListAsync(new GetFieldsInput
            {
                MaxResultCount = 1000
            })).Items;
        }

        private async Task NameExistsValidatorAsync(ValidatorEventArgs e, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var name = Convert.ToString(e.Value);
            if (!name.IsNullOrEmpty())
            {
                if (!name.Equals(entryTypeNameForValidation, StringComparison.InvariantCultureIgnoreCase))
                {
                    e.Status = await EntryTypeAdminAppService.NameExistsAsync(new EntryTypeNameExistsInput(SectionId, name))
                        ? ValidationStatus.Error
                        : ValidationStatus.Success;

                    e.ErrorText = L["EntryTypeName{0}AlreadyExist", name];
                }
            }
            else
            {
                e.Status = ValidationStatus.Error;
            }
        }


        private void DisplayNameTextEditBlur()
        {
            if (!Entity.DisplayName.IsNullOrEmpty() && Entity.Name.IsNullOrEmpty())
            {
                Entity.Name = SlugNormalizer.Normalize(Entity.DisplayName);
            }
        }

        private async Task SectionFieldDropped(string fieldTabName)
        {
            var field = AllFields.First(f => f.Id == DraggingFieldId);

            //Check if from FieldTabs, if true then remove
            foreach (var tab in Entity.FieldTabs)
            {
                if (tab.Fields.Any(f => f.FieldId == DraggingFieldId))
                {
                    tab.Fields.RemoveAll(f => f.FieldId == DraggingFieldId);
                }
            }

            //Add to FieldTabs
            Entity.FieldTabs.First(ft => ft.Name == fieldTabName)
                .Fields.Add(
                new EntryFieldInput()
                {
                    FieldId = DraggingFieldId.Value,
                    DisplayName = field.DisplayName
                });

            await InvokeAsync(StateHasChanged);
        }
        private async Task FieldDropped()
        {
            foreach (var ft in Entity.FieldTabs)
            {
                ft.Fields.RemoveAll(ft => ft.FieldId == DraggingFieldId);
            }
            await InvokeAsync(StateHasChanged);
        }


        /****** Field Tab **************************/

        private void OnTabClicked(string name)
        {
            SelectedFieldTabName = name;
        }

        void RemoveFieldTab(EntryFieldTabInput tab)
        {
            if (Entity.FieldTabs.Count > 1)
            {
                var index = Entity.FieldTabs.IndexOf(tab);
                Entity.FieldTabs.Remove(tab);

                index = index > 0 ? index - 1 : index;
                OnTabClicked(Entity.FieldTabs[index].Name);
            }
        }

        private void OnSelectedTabChanged(string name)
        {
            if (!Entity.FieldTabs.Any(ft => ft.Name == name))
            {
                var tabCount=Entity.FieldTabs.Count;
                OnTabClicked(Entity.FieldTabs[tabCount-1].Name);
            }
        }


        /****** Create Field Tab **************************/
        private async Task OpenCreateModalAsync()
        {
            if (CreateValidationsRef != null)
            {
                await CreateValidationsRef.ClearAll();
            }
            await InvokeAsync(async () =>
            {
                StateHasChanged();
                if (CreateModal != null)
                {
                    await CreateModal.Show();
                }

            });
        }

        private Task CloseCreateModalAsync()
        {
            NewFieldTab.Name = "";
            return InvokeAsync(CreateModal!.Hide);
        }

        private Task ClosingCreateModal(ModalClosingEventArgs eventArgs)
        {
            // cancel close if clicked outside of modal area
            eventArgs.Cancel = eventArgs.CloseReason == CloseReason.FocusLostClosing;

            return Task.CompletedTask;
        }

        private void NewFieldTabNameValidator(ValidatorEventArgs e)
        {
            var name = Convert.ToString(e.Value);
            if (!name.IsNullOrEmpty())
            {
                e.Status = Entity.FieldTabs.Any(ft => ft.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                    ? ValidationStatus.Error
                    : ValidationStatus.Success;

                e.ErrorText = L["FieldTabName{0}AlreadyExist", name];
            }
            else
            {
                e.Status = ValidationStatus.Error;
            }
        }
        private async Task CreateFieldTabAsync()
        {
            var validate = true;
            if (CreateValidationsRef != null)
            {
                validate = await CreateValidationsRef.ValidateAll();
            }
            if (validate)
            {
                Entity.FieldTabs.Add(new EntryFieldTabInput(NewFieldTab.Name));
                OnTabClicked(NewFieldTab.Name);

                NewFieldTab = new("");
                await InvokeAsync(CreateModal!.Hide);
            }
        }

        /****** Edit Field Tab **************************/
        private async Task OpenEditModalAsync()
        {
            if (EditValidationsRef != null)
            {
                await EditValidationsRef.ClearAll();
            }
            EditingFieldTab = new EntryFieldTabInput(SelectedFieldTabName);
            await InvokeAsync(async () =>
            {
                StateHasChanged();
                if (EditModal != null)
                {
                    await EditModal.Show();
                }
            });
        }
        private Task CloseEditModalAsync()
        {
            InvokeAsync(EditModal!.Hide);
            return Task.CompletedTask;
        }
        private Task ClosingEditModal(ModalClosingEventArgs eventArgs)
        {
            // cancel close if clicked outside of modal area
            eventArgs.Cancel = eventArgs.CloseReason == CloseReason.FocusLostClosing;

            return Task.CompletedTask;
        }
        private void EditingFieldTabNameValidator(ValidatorEventArgs e)
        {
            var name = Convert.ToString(e.Value);
            if (!name.IsNullOrEmpty())
            {
                if (!name.Equals(SelectedFieldTabName, StringComparison.InvariantCultureIgnoreCase))
                {
                    e.Status = Entity.FieldTabs.Any(ft=>ft.Name.Equals(name,StringComparison.InvariantCultureIgnoreCase))
                        ? ValidationStatus.Error
                        : ValidationStatus.Success;

                    e.ErrorText = L["FieldTabName{0}AlreadyExist", name];
                }
            }
            else
            {
                e.Status = ValidationStatus.Error;
            }
        }


        private async Task UpdateEntityAsync()
        {
            var validate = true;
            if (EditValidationsRef != null)
            {
                validate = await EditValidationsRef.ValidateAll();
            }
            if (validate)
            {
                Entity.FieldTabs.Single(t => t.Name == SelectedFieldTabName).Name = EditingFieldTab.Name;
                OnTabClicked(EditingFieldTab.Name);

                await InvokeAsync(EditModal!.Hide);
            }
        }      


        /****** Edit Field **************************/
        private async Task OpenEditFieldModalAsync(EntryFieldInput field)
        {
            if (EditFieldValidationsRef != null)
            {
                await EditFieldValidationsRef.ClearAll();
            }
            EditingField = new EntryFieldInput(field.FieldId, field.DisplayName, field.Required, field.ShowInList, field.EnableSearch);
            await InvokeAsync(async () =>
            {
                StateHasChanged();
                if (EditFieldModal != null)
                {
                    await EditFieldModal.Show();
                }
            });
        }
        private Task CloseEditFieldModalAsync()
        {
            InvokeAsync(EditFieldModal!.Hide);
            return Task.CompletedTask;
        }
        private Task ClosingEditFieldModal(ModalClosingEventArgs eventArgs)
        {
            // cancel close if clicked outside of modal area
            eventArgs.Cancel = eventArgs.CloseReason == CloseReason.FocusLostClosing;

            return Task.CompletedTask;
        }

        private async Task UpdateFieldAsync()
        {
            var validate = true;
            if (EditFieldValidationsRef != null)
            {
                validate = await EditFieldValidationsRef.ValidateAll();
            }
            if (validate)
            {
                var field = Entity.FieldTabs.SelectMany(ft => ft.Fields).Single(f => f.FieldId == EditingField.FieldId);
                field.DisplayName = EditingField.DisplayName;
                field.Required = EditingField.Required;
                field.ShowInList = EditingField.ShowInList;
                await InvokeAsync(EditFieldModal!.Hide);
            }
        }
    }
}
