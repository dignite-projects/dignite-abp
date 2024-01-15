using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Volo.Abp.AspNetCore.Components;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.DynamicForms.Components;

public abstract class FormControlComponentBase<TFormControl, TFormConfiguration, TFormControlValueType> : AbpComponentBase, IFormControlComponent, ITransientDependency
    where TFormControl : IFormControl
    where TFormConfiguration : FormConfigurationBase, new()
{
    public Type FormControlType { get; private set; }

    public TFormConfiguration FormConfiguration { get; private set; }

    [Parameter]
    public FormField Field { get; set; }

    [Parameter]
    public EventCallback<FormField> OnChangedValueAsync { get; set; }

    protected FormControlComponentBase()
    {
        LocalizationResource = typeof(DigniteAbpDynamicFormsModule);
        FormControlType = typeof(TFormControl);
        FormConfiguration = new TFormConfiguration();
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        FormConfiguration.ConfigurationDictionary = Field.FormConfiguration;
    }

    protected virtual async Task ChangeValueAsync(TFormControlValueType value)
    {
        Field.Value = value;

        if (OnChangedValueAsync.HasDelegate)
            await OnChangedValueAsync.InvokeAsync(Field);
    }
}