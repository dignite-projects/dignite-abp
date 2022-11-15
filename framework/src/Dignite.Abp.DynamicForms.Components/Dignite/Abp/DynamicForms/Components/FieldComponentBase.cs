using System;
using Microsoft.AspNetCore.Components;
using Volo.Abp.AspNetCore.Components;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.DynamicForms.Components;

public abstract class FieldComponentBase<TFormProvider, TFormConfiguration> : AbpComponentBase, IFieldComponent, ITransientDependency
    where TFormProvider : IFormProvider
    where TFormConfiguration : FormConfigurationBase, new()
{
    public Type FormProviderType { get; private set; }

    public TFormConfiguration FormConfiguration { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public IHasCustomFields CustomizableObject { get; set; }

    /// <summary>
    /// Custom field object
    /// </summary>
    [Parameter]
    public ICustomizeField Field { get; set; }

    /// <summary>
    /// Whether it is a child form component
    /// </summary>
    [Parameter]
    public bool IsChild { get; set; }

    protected FieldComponentBase()
    {
        LocalizationResource = typeof(AbpDynamicFormsModule);
        IsChild = false;
        FormProviderType = typeof(TFormProvider);
        FormConfiguration = new TFormConfiguration();
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        FormConfiguration.ConfigurationDictionary = Field.FormConfiguration;
    }
}