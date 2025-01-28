using Blazorise;
using Dignite.Abp.DynamicForms;
using Dignite.Abp.DynamicForms.Components;
using Dignite.Abp.DynamicForms.Matrix;
using Dignite.Abp.DynamicForms.TextEdit;
using Dignite.Cms.Localization;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace Dignite.Cms.Admin.Blazor.Components.DynamicForms.Matrix
{
    public partial class MatrixFormConfigurationComponent
    {
        protected List<MatrixBlockType> MatrixBlockTypes = new();
        protected ImmutableList<IFormControl> AvailableFormControls;

        protected Modal CreateBlockTypeModal;
        protected Validations CreateBlockTypeValidationsRef;
        protected MatrixBlockType NewBlockType = new();

        protected Modal EditBlockTypeModal;
        protected Validations EditBlockTypeValidationsRef;
        protected MatrixBlockType EditingBlockType = new();

        protected MatrixBlockType SelectedBlockType = null;

        protected Validations FieldValidationsRef;
        protected FormField SelectedField = null;
        protected Type FieldFormConfigurationComponentType;
        protected Dictionary<string, object> FieldFormConfigurationComponentParameters = new();

        private string EditingBlockTypeName;



        public MatrixFormConfigurationComponent()
        {
            LocalizationResource = typeof(CmsResource);
        }


        void ValidateAll(ValidatorEventArgs e)
        {
            FormConfiguration.MatrixBlockTypes = MatrixBlockTypes;
        }


        protected override Task OnInitializedAsync()
        {
            AvailableFormControls = AllFormControls.Where(p => p.Name != MatrixFormControl.ControlName).ToImmutableList();
            return base.OnInitializedAsync();
        }

        protected override async void OnParametersSet()
        {
            base.OnParametersSet();
            MatrixBlockTypes = FormConfiguration.MatrixBlockTypes == null ? new() : FormConfiguration.MatrixBlockTypes;
            if (MatrixBlockTypes.Any())
            {
                await SelectBlockTypeAsync(MatrixBlockTypes.First());
            }
        }

        #region create block type methods
        private Task ClosingCreateBlockTypeModal(ModalClosingEventArgs eventArgs)
        {
            // cancel close if clicked outside of modal area
            eventArgs.Cancel = eventArgs.CloseReason == CloseReason.FocusLostClosing;

            return Task.CompletedTask;
        }

        private Task CloseCreateBlockTypeModalAsync()
        {
            InvokeAsync(CreateBlockTypeModal.Hide);
            return Task.CompletedTask;
        }

        private void CreateMatrixBlockTypeNameExistsValidator(ValidatorEventArgs e)
        {
            var name = Convert.ToString(e.Value);
            if (!name.IsNullOrEmpty())
            {
                if (FormConfiguration.MatrixBlockTypes!=null
                    &&FormConfiguration.MatrixBlockTypes.Any(mbt => mbt.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                    )
                {
                    e.Status = ValidationStatus.Error;
                    e.ErrorText = L["MatrixBlockTypeName{0}AlreadyExist", name];
                }
                else
                {
                    e.Status = ValidationStatus.Success;
                }
            }
            else
            {
                e.Status = ValidationStatus.Error;
            }
        }

        private async Task OpenCreateBlockTypeModal()
        {
            EditingBlockTypeName = null;
            NewBlockType = new MatrixBlockType();

            await InvokeAsync(async () =>
            {
                StateHasChanged();
                await CreateBlockTypeModal.Show();
            });
        }

        private async Task CreateBlockType()
        {
            if (await CreateBlockTypeValidationsRef.ValidateAll())
            {
                MatrixBlockTypes.Add(NewBlockType);
                FormConfiguration.MatrixBlockTypes = MatrixBlockTypes;
                await SelectBlockTypeAsync(NewBlockType);
                await CreateBlockTypeModal.Hide();
            }
        }
        #endregion


        #region update block type methods
        private Task ClosingEditBlockTypeModal(ModalClosingEventArgs eventArgs)
        {
            // cancel close if clicked outside of modal area
            eventArgs.Cancel = eventArgs.CloseReason == CloseReason.FocusLostClosing;

            return Task.CompletedTask;
        }

        private Task CloseEditBlockTypeModalAsync()
        {
            EditingBlockType.Name = EditingBlockTypeName;
            InvokeAsync(EditBlockTypeModal.Hide);
            return Task.CompletedTask;
        }

        private void EditingMatrixBlockTypeNameExistsValidator(ValidatorEventArgs e)
        {
            var name = Convert.ToString(e.Value);
            if (!name.IsNullOrEmpty())
            {
                if (!EditingBlockTypeName.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                {
                    if (FormConfiguration.MatrixBlockTypes.Any(mbt => mbt.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        e.Status = ValidationStatus.Error;
                        e.ErrorText = L["MatrixBlockTypeName{0}AlreadyExist", name];
                    }
                    else
                    {
                        e.Status = ValidationStatus.Success;
                    }
                }
            }
            else
            {
                e.Status = ValidationStatus.Error;
            }
        }

        private async Task OpenEditBlockTypeModal(MatrixBlockType blockType)
        {
            EditingBlockTypeName = blockType.Name;
            EditingBlockType = blockType;

            await InvokeAsync(async () =>
            {
                StateHasChanged();
                await EditBlockTypeModal.Show();
            });
        }

        private async Task UpdateBlockType()
        {
            if (await EditBlockTypeValidationsRef.ValidateAll())
            {
                FormConfiguration.MatrixBlockTypes = MatrixBlockTypes;
                await EditBlockTypeModal.Hide();
            }
        }
        #endregion


        private Task DeleteBlockTypeAsync(MatrixBlockType blockType)
        {
            MatrixBlockTypes.RemoveAll(mbt => mbt.Name == blockType.Name);
            return Task.CompletedTask;


            //TODO:
            //After testing, blazorise's ListGroup does not select any "BlockType" when the last entry is removed, even if a "BlockType" is specified.
            //This is an unresolved issue
        }

        private async Task SelectBlockTypeAsync(MatrixBlockType blockType)
        {
            if (SelectedField != null)
            {
                if (await FieldValidationsRef.ValidateAll())
                {
                    SelectedBlockType = blockType;
                }
                else
                {
                    return;
                }
            }
            else
            {
                SelectedBlockType = blockType;
            }

            if (SelectedBlockType.Fields.Any())
            {
                await SelectFieldAsync(SelectedBlockType.Fields.First());
            }
            else
            {
                SelectedField = null;
            }
        }

        private Task DeleteFieldAsync(FormField field)
        {
            SelectedBlockType.Fields.RemoveAll(f => f.Name == field.Name);
            return Task.CompletedTask;

            //TODO:
            //After testing, blazorise's ListGroup does not select any field when the last entry is removed, even if a field is specified.
            //This is an unresolved issue
        }

        private async Task SelectFieldAsync(FormField field)
        {
            if (SelectedField == null || await FieldValidationsRef.ValidateAll())
            {
                SelectedField = field;
                await SetFieldFormConfigurationComponentAsync();
            }
        }

        private async Task AddFieldAsync()
        {
            if (SelectedField == null || await FieldValidationsRef.ValidateAll())
            {
                SelectedField = new FormField("", "", "", TextEditFormControl.ControlName, new FormConfigurationDictionary(), false, null);
                SelectedBlockType.Fields.Add(SelectedField);
                await SetFieldFormConfigurationComponentAsync();
            }
        }

        private async Task FormTypeChangedAsync(string formName)
        {
            SelectedField.FormControlName = formName;
            await SetFieldFormConfigurationComponentAsync();
        }

        private async Task SetFieldFormConfigurationComponentAsync()
        {
            var configurationComponent = ConfigurationComponentSelector.Get(SelectedField.FormControlName);
            FieldFormConfigurationComponentType = configurationComponent.GetType();
            FieldFormConfigurationComponentParameters = new Dictionary<string, object>
            {
                { nameof(IFormConfigurationComponent.ConfigurationDictionary), SelectedField.FormConfiguration }
            };

            await Task.CompletedTask;
        }

        private void FieldNameExistsValidator(ValidatorEventArgs e)
        {
            var fieldName = Convert.ToString(e.Value);
            if (!fieldName.IsNullOrEmpty())
            {
                if (
                    SelectedBlockType.Fields.Count(f => f.Name.Equals(fieldName, StringComparison.InvariantCultureIgnoreCase)) == 2
                )
                {
                    e.Status = ValidationStatus.Error;
                    e.ErrorText = L["FieldName{0}AlreadyExist", fieldName];
                }
                else
                {
                    e.Status = ValidationStatus.Success;
                }
            }
            else
            {
                e.Status = ValidationStatus.Error;
            }
        }


        private void BlockTypeDisplayNameTextEditBlur(MatrixBlockType blockType)
        {
            if (!blockType.DisplayName.IsNullOrEmpty() && blockType.Name.IsNullOrEmpty())
            {
                blockType.Name = SlugNormalizer.Normalize(blockType.DisplayName);
            }
        }
        private void FieldDisplayNameTextEditBlur()
        {
            if (!SelectedField.DisplayName.IsNullOrEmpty() && SelectedField.Name.IsNullOrEmpty())
            {
                SelectedField.Name = SlugNormalizer.Normalize(SelectedField.DisplayName);
            }
        }
    }
}
