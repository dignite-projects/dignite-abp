using System;
using Microsoft.AspNetCore.Components;
using Volo.Abp.AspNetCore.Components;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.DynamicForms.Components;

public abstract class FormViewComponentBase<TForm, TFormConfiguration> : AbpComponentBase, IFormViewComponent, ITransientDependency
    where TForm : IFormControl
    where TFormConfiguration : FormConfigurationBase, new()
{
    public Type FormControlType { get; private set; }

    public TFormConfiguration FormConfiguration { get; private set; }

    /// <summary>
    /// Dynamic form field object
    /// </summary>
    [Parameter]
    public FormField Field { get; set; }

    protected FormViewComponentBase()
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
}