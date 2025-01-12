using System;
using Dignite.Abp.DynamicForms.Localization;
using Microsoft.AspNetCore.Components;
using Volo.Abp.AspNetCore.Components;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.DynamicForms.Components;

public abstract class FormConfigurationComponentBase<TFormControl, TFormConfiguration> : AbpComponentBase, IFormConfigurationComponent, ITransientDependency
    where TFormControl : IFormControl
    where TFormConfiguration : FormConfigurationBase, new()
{
    public Type FormControlType { get; private set; }

    public TFormConfiguration FormConfiguration { get; private set; }

    [Parameter]
    public FormConfigurationDictionary ConfigurationDictionary { get; set; }

    protected FormConfigurationComponentBase()
    {
        LocalizationResource = typeof(AbpDynamicFormsResource);
        FormControlType = typeof(TFormControl);
        FormConfiguration = new TFormConfiguration();
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        FormConfiguration.ConfigurationDictionary = ConfigurationDictionary;
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        FormConfiguration.ConfigurationDictionary = ConfigurationDictionary;
    }
}