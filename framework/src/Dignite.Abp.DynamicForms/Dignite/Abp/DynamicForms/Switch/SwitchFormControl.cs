namespace Dignite.Abp.DynamicForms.Switch;

public class SwitchFormControl : FormControlBase
{
    public const string ControlName = "Switch";

    public override string Name => ControlName;

    public override string DisplayName => L["FormControl:Switch"];

    public override void Validate(FormControlValidateArgs args)
    {
    }

    public override FormConfigurationBase GetConfiguration(FormConfigurationDictionary fieldConfiguration)
    {
        return new SwitchConfiguration(fieldConfiguration);
    }
}