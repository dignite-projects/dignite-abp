using System;
using Dignite.Abp.FieldCustomizing.Forms;
using Microsoft.AspNetCore.Components;
using Volo.Abp.AspNetCore.Components;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.FieldCustomizing.Components;

public abstract class FieldFormComponentBase<TFieldProvider, TConfiguration> : AbpComponentBase, IFieldFormComponent, ITransientDependency
    where TFieldProvider : IFormProvider
    where TConfiguration : FormConfigurationBase, new()
{
    protected FieldFormComponentBase()
    {
        LocalizationResource = typeof(AbpFieldCustomizingModule);
        HideFieldLable = false;
        FormProviderType = typeof(TFieldProvider);
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