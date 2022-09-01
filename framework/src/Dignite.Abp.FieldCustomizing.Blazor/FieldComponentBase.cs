using Dignite.Abp.FieldCustomizing.Fields;
using Microsoft.AspNetCore.Components;
using System;
using Volo.Abp.AspNetCore.Components;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.FieldCustomizing.Blazor
{
    public abstract class FieldComponentBase<TFieldProvider, TConfiguration> : AbpComponentBase, IFieldComponent, ITransientDependency
        where TFieldProvider : IFieldProvider
        where TConfiguration : FieldConfigurationBase, new()
    {
        protected FieldComponentBase()
        {
            LocalizationResource = typeof(DigniteAbpFieldCustomizingModule);
            HideFieldLable = false;
            FieldProviderType = typeof(TFieldProvider);
        }


        public Type FieldProviderType { get; private set; }

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
}
