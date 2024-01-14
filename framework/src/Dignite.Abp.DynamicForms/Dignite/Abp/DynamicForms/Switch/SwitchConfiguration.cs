namespace Dignite.Abp.DynamicForms.Switch;

public class SwitchConfiguration : FormConfigurationBase
{
    public bool Default{
        get => ConfigurationDictionary.GetConfigurationOrDefault(SwitchConfigurationNames.Default, false);
        set => ConfigurationDictionary.SetConfiguration(SwitchConfigurationNames.Default, value);
    }


    public SwitchConfiguration() : base()
    {
    }

    public SwitchConfiguration(FormConfigurationDictionary fieldConfiguration)
        : base(fieldConfiguration)
    {
    }
}