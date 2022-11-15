using System;
using Microsoft.AspNetCore.Components;
using Volo.Abp.AspNetCore.Components;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.DynamicForms.Components;

public abstract class FormComponentBase<TFieldProvider, TFormConfiguration> : AbpComponentBase, IFormComponent, ITransientDependency
    where TFieldProvider : IFormProvider
    where TFormConfiguration : FormConfigurationBase, new()
{
    public Type FormProviderType { get; private set; }

    public TFormConfiguration FormConfiguration { get; private set; }

    [Parameter]
    public IHasCustomFields CustomizableObject { get; set; }

    [Parameter]
    public ICustomizeField Field { get; set; }

    [Parameter]
    public bool IsChild { get; set; }

    protected FormComponentBase()
    {
        LocalizationResource = typeof(AbpDynamicFormsModule);
        IsChild = false;
        FormProviderType = typeof(TFieldProvider);
        FormConfiguration = new TFormConfiguration();
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        FormConfiguration.ConfigurationDictionary = Field.FormConfiguration;
    }
}