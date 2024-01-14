namespace Dignite.Abp.DynamicForms;

public abstract class FormConfigurationBase
{
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