namespace Dignite.Abp.DynamicForms;

public abstract class FormConfigurationBase
{
    public bool Required {
        get => ConfigurationDictionary.GetConfigurationOrDefault(FormBasicConfigurationNames.Required, false);
        set => ConfigurationDictionary.SetConfiguration(FormBasicConfigurationNames.Required, value);
    }

    public string Description {
        get => ConfigurationDictionary.GetConfigurationOrDefault<string>(FormBasicConfigurationNames.Description, null);
        set => ConfigurationDictionary.SetConfiguration(FormBasicConfigurationNames.Description, value);
    }

    public FormConfigurationDictionary ConfigurationDictionary { get; set; }

    public FormConfigurationBase(
        FormConfigurationDictionary fieldConfiguration)
    {
        ConfigurationDictionary = fieldConfiguration;
    }

    public FormConfigurationBase()
    {
        ConfigurationDictionary = new FormConfigurationDictionary();
    }
}