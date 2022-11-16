using System;
using Microsoft.AspNetCore.Components;
using Volo.Abp.AspNetCore.Components;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.DynamicForms.Components;

public abstract class FormComponentBase<TForm, TFormConfiguration> : AbpComponentBase, IFormComponent, ITransientDependency
    where TForm : IForm
    where TFormConfiguration : FormConfigurationBase, new()
{
    public Type FormType { get; private set; }

    public TFormConfiguration FormConfiguration { get; private set; }

    [Parameter]
    public IHasCustomFields CustomizableObject { get; set; }

    [Parameter]
    public ICustomizeFieldInfo Field { get; set; }

    [Parameter]
    public bool IsChild { get; set; }

    protected FormComponentBase()
    {
        LocalizationResource = typeof(AbpDynamicFormsModule);
        IsChild = false;
        FormType = typeof(TForm);
        FormConfiguration = new TFormConfiguration();
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        FormConfiguration.ConfigurationDictionary = Field.FormConfiguration;
    }
}