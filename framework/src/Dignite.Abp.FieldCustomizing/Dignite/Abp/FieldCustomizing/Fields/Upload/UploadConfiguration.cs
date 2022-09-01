using System.ComponentModel.DataAnnotations;

namespace Dignite.Abp.FieldCustomizing.Fields.Upload
{
    public class UploadConfiguration:FieldConfigurationBase
    {
        [StringLength(256)]
        public string Placeholder
        {
            get => ConfigurationDictionary.GetConfigurationOrDefault<string>(UploadConfigurationNames.Placeholder, null);
            set => ConfigurationDictionary.SetConfiguration(UploadConfigurationNames.Placeholder, value);
        }

        [Required]
        public bool Multiple
        {
            get => ConfigurationDictionary.GetConfigurationOrDefault(UploadConfigurationNames.Multiple, false);
            set => ConfigurationDictionary.SetConfiguration(UploadConfigurationNames.Multiple, value);
        }


        [Required]
        public string Filter
        {
            get => ConfigurationDictionary.GetConfigurationOrDefault<string>(UploadConfigurationNames.Filter);
            set => ConfigurationDictionary.SetConfiguration(UploadConfigurationNames.Filter, value);
        }

        public UploadConfiguration(FieldConfigurationDictionary fieldConfiguration)
            :base(fieldConfiguration)
        {
        }

        public UploadConfiguration():base()
        {
        }
    }
}
