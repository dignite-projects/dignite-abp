using Dignite.Abp.FieldCustomizing.Fields;
using Dignite.Abp.FieldCustomizing.Localization;
using Microsoft.AspNetCore.Components;
using System;
using Volo.Abp.AspNetCore.Components;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.FieldCustomizing.FieldComponents;

public abstract class FieldConfigurationComponentBase<TFieldProvider, TConfiguration> : AbpComponentBase, IFieldConfigurationComponent, ITransientDependency
    where TFieldProvider : IFieldProvider
    where TConfiguration : FieldConfigurationBase, new()
{
    protected FieldConfigurationComponentBase()
    {
        LocalizationResource = typeof(DigniteAbpFieldCustomizingResource);
        FieldProviderType = typeof(TFieldProvider);
    }


    public Type FieldProviderType { get; private set; }

    public TConfiguration Configuration { get; private set; }

    [Parameter]
    public ICustomizeFieldDefinition Definition { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        Configuration = new TConfiguration();
        Configuration.ConfigurationDictionary = Definition.Configuration;
    }
}
