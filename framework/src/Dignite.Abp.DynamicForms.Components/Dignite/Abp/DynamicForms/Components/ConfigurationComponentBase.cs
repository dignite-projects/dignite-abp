using System;
using Dignite.Abp.DynamicForms.Localization;
using Microsoft.AspNetCore.Components;
using Volo.Abp.AspNetCore.Components;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.DynamicForms.Components;

public abstract class ConfigurationComponentBase<TFormProvider, TFormConfiguration> : AbpComponentBase, IConfigurationComponent, ITransientDependency
    where TFormProvider : IFormProvider
    where TFormConfiguration : FormConfigurationBase, new()
{
    public Type FormProviderType { get; private set; }

    public TFormConfiguration FormConfiguration { get; private set; }

    [Parameter]
    public ICustomizeField Field { get; set; }

    protected ConfigurationComponentBase()
    {
        LocalizationResource = typeof(AbpDynamicFormsResource);
        FormProviderType = typeof(TFormProvider);
        FormConfiguration = new TFormConfiguration();
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        FormConfiguration.ConfigurationDictionary = Field.FormConfiguration;
    }
}