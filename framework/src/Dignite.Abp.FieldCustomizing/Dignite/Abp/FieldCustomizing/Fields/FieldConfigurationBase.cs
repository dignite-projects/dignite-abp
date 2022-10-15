namespace Dignite.Abp.FieldCustomizing.Fields;

public abstract class FieldConfigurationBase
{
    public bool Required {
        get => ConfigurationDictionary.GetConfigurationOrDefault(FieldBasicConfigurationNames.Required, false);
        set => ConfigurationDictionary.SetConfiguration(FieldBasicConfigurationNames.Required, value);
    }

    public string Description {
        get => ConfigurationDictionary.GetConfigurationOrDefault<string>(FieldBasicConfigurationNames.Description, null);
        set => ConfigurationDictionary.SetConfiguration(FieldBasicConfigurationNames.Description, value);
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