using System;
using Dignite.Abp.DynamicForms.Localization;
using Microsoft.AspNetCore.Components;
using Volo.Abp.AspNetCore.Components;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.DynamicForms.Components;

public abstract class ConfigurationComponentBase<TForm, TFormConfiguration> : AbpComponentBase, IConfigurationComponent, ITransientDependency
    where TForm : IForm
    where TFormConfiguration : FormConfigurationBase, new()
{
    public Type FormType { get; private set; }

    public TFormConfiguration FormConfiguration { get; private set; }

    [Parameter]
    public ICustomizeFieldInfo Field { get; set; }

    protected ConfigurationComponentBase()
    {
        LocalizationResource = typeof(AbpDynamicFormsResource);
        FormType = typeof(TForm);
        FormConfiguration = new TFormConfiguration();
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        FormConfiguration.ConfigurationDictionary = Field.FormConfiguration;
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        FormConfiguration.ConfigurationDictionary = Field.FormConfiguration;
    }
}