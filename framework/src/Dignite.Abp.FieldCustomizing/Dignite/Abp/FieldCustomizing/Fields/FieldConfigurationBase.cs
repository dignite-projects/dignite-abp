

using System.ComponentModel.DataAnnotations;

namespace Dignite.Abp.FieldCustomizing.Fields
{
    public abstract class FieldConfigurationBase
    {
        public bool Required
        {
            get => ConfigurationDictionary.GetConfigurationOrDefault(FieldConfigurationNames.Required, false);
            set => ConfigurationDictionary.SetConfiguration(FieldConfigurationNames.Required, value);
        }

        [StringLength(256)]
        public string Description
        {
            get => ConfigurationDictionary.GetConfigurationOrDefault<string>(FieldConfigurationNames.Description, null);
            set => ConfigurationDictionary.SetConfiguration(FieldConfigurationNames.Description, value);
        }

        public FieldConfigurationDictionary ConfigurationDictionary { get; set; }

        public FieldConfigurationBase(
            FieldConfigurationDictionary fieldConfiguration)
        {
            ConfigurationDictionary = fieldConfiguration;
        }

        public FieldConfigurationBase()
        {
        }
    }
}
