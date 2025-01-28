using Blazorise;
using Dignite.Abp.DynamicForms.Components;
using Dignite.Cms.Admin.DynamicForms;
using Dignite.Cms.Admin.Fields;
using Dignite.Cms.Localization;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Dignite.Cms.Admin.Blazor.Pages.Cms.Admin.Fields
{
    public partial class CreateOrUpdateFieldComponent
    {
        [Parameter] public CreateOrUpdateFieldInputBase Entity { get; set; }
        protected IReadOnlyList<FormControlDto> AllFormControls { get; set; } = new List<FormControlDto>();
        protected IReadOnlyList<FieldGroupDto> AllGroups { get; set; } = new List<FieldGroupDto>();

        protected Type FormConfigurationComponentType;
        protected Dictionary<string, object> FormConfigurationComponentParameters = new();

        //Will not change again after assignment, used to verify that the field name already exists
        private string fieldNameForValidation;
        public CreateOrUpdateFieldComponent()
        {
            LocalizationResource = typeof(CmsResource);
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            fieldNameForValidation = Entity.Name;
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            AllFormControls = (await FormService.GetFormControlsAsync()).Items;
            AllGroups = (await FieldGroupService.GetListAsync(new GetFieldGroupsInput())).Items;
            await SetFormConfigurationComponentAsync();
        }

        protected async Task OnFormChangedAsync(string value)
        {
            Entity.FormControlName = value;
            await SetFormConfigurationComponentAsync();
        }

        private async Task SetFormConfigurationComponentAsync()
        {
            var configurationComponent = ConfigurationComponentSelector.Get(Entity.FormControlName);
            FormConfigurationComponentType = configurationComponent.GetType();
            FormConfigurationComponentParameters = new Dictionary<string, object>
            {
                { nameof(IFormConfigurationComponent.ConfigurationDictionary), Entity.FormConfiguration }
            };
            await Task.CompletedTask;
        }

        private async Task NameExistsValidatorAsync(ValidatorEventArgs e, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var name = Convert.ToString(e.Value);
            if (!name.IsNullOrEmpty())
            {
                if (!name.Equals(fieldNameForValidation, StringComparison.InvariantCultureIgnoreCase))
                {
                    e.Status = await FieldService.NameExistsAsync(name)
                        ? ValidationStatus.Error
                        : ValidationStatus.Success;

                    e.ErrorText = L["FieldName{0}AlreadyExist", name];
                }
            }
            else
            {
                e.Status = ValidationStatus.Error;
            }
        }
        void DisplayNameTextEditBlur()
        {
            if (!Entity.DisplayName.IsNullOrEmpty() && Entity.Name.IsNullOrEmpty())
            {
                Entity.Name = SlugNormalizer.Normalize(Entity.DisplayName);
            }
        }
    }
}
