

namespace Dignite.Abp.FieldCustomizing.Fields.Switch
{
    public class SwitchFieldProvider : FieldProviderBase
    {

        public const string ProviderName = "Switch";

        public override string Name => ProviderName;

        public override string DisplayName => L["SwitchControl"];

        public override FieldType ControlType => FieldType.Simple;

        public override void Validate(FieldValidateArgs args)
        {
        }

        public override FieldConfigurationBase GetConfiguration(FieldConfigurationDictionary fieldConfiguration)
        {
            return new SwitchConfiguration(fieldConfiguration);
        }
    }
}
