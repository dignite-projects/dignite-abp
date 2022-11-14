using System;
using Dignite.Abp.FieldCustomizing.Forms;
using Microsoft.AspNetCore.Components;
using Volo.Abp.AspNetCore.Components;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.FieldCustomizing.Components;

public abstract class FieldComponentBase<TFormProvider, TConfiguration> : AbpComponentBase, IFieldComponent, ITransientDependency
    where TFormProvider : IFormProvider
    where TConfiguration : FormConfigurationBase, new()
{
    protected FieldComponentBase()
    {
        LocalizationResource = typeof(AbpFieldCustomizingModule);
        HideFieldLable = false;
        FormProviderType = typeof(TFormProvider);
    }

    public Type FormProviderType { get; private set; }

    public TConfiguration Configuration { get; private set; }

    [Parameter]
    public CustomizeField CustomizeField { get; set; }

    [Parameter]
    public bool HideFieldLable { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        Configuration = new TConfiguration();
        Configuration.ConfigurationDictionary = CustomizeField.Definition.Configuration;
    }
}