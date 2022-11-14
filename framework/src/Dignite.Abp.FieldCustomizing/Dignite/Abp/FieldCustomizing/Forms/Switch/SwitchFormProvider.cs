namespace Dignite.Abp.FieldCustomizing.Forms.Switch;

public class SwitchFormProvider : FormProviderBase
{
    public const string ProviderName = "Switch";

    public override string Name => ProviderName;

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