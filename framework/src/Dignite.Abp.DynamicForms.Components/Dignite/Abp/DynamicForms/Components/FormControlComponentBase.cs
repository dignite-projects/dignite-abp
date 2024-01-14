using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Volo.Abp.AspNetCore.Components;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.DynamicForms.Components;

public abstract class FormControlComponentBase<TForm, TFormConfiguration,TValueType> : AbpComponentBase, IFormControlComponent, ITransientDependency
    where TForm : IFormControl
    where TFormConfiguration : FormConfigurationBase, new()
{
    public Type FormControlType { get; private set; }

    public TFormConfiguration FormConfiguration { get; private set; }

    [Parameter]
    public FormField Field { get; set; }

    [Parameter]
    public EventCallback<FormField> OnFormControlValueChanged { get; set; }

    protected FormControlComponentBase()
    {
        LocalizationResource = typeof(DigniteAbpDynamicFormsModule);
        FormControlType = typeof(TForm);
        FormConfiguration = new TFormConfiguration();
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        FormConfiguration.ConfigurationDictionary = Field.FormConfiguration;
    }

    protected virtual async Task OnValueChanged(TValueType value)
    {
        Field.Value = value;

        if (OnFormControlValueChanged.HasDelegate)
            await OnFormControlValueChanged.InvokeAsync(Field);
    }
}