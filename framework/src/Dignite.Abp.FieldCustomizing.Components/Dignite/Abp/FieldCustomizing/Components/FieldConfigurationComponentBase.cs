using System;
using Dignite.Abp.FieldCustomizing.Forms;
using Dignite.Abp.FieldCustomizing.Localization;
using Microsoft.AspNetCore.Components;
using Volo.Abp.AspNetCore.Components;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.FieldCustomizing.Components;

public abstract class FieldConfigurationComponentBase<TFieldProvider, TConfiguration> : AbpComponentBase, IFieldConfigurationComponent, ITransientDependency
    where TFieldProvider : IFormProvider
    where TConfiguration : FormConfigurationBase, new()
{
    protected FieldConfigurationComponentBase()
    {
        LocalizationResource = typeof(AbpFieldCustomizingResource);
        FormProviderType = typeof(TFieldProvider);
    }

    public Type FormProviderType { get; private set; }

    public TConfiguration Configuration { get; private set; }

    [Parameter]
    public ICustomizeFieldDefinition FieldDefinition { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        Configuration = new TConfiguration();
        Configuration.ConfigurationDictionary = FieldDefinition.Configuration;
    }
}