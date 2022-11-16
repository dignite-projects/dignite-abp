namespace Dignite.Abp.DynamicForms.Switch;

public class SwitchForm : FormBase
{
    public const string SwitchFormName = "Switch";

    public override string Name => SwitchFormName;

    public override string DisplayName => L["SwitchControl"];

    public override FormType FormType => FormType.Simple;

    public override void Validate(FormValidateArgs args)
    {
    }

    public override FormConfigurationBase GetConfiguration(FormConfigurationDictionary fieldConfiguration)
    {
        return new SwitchConfiguration(fieldConfiguration);
    }
}