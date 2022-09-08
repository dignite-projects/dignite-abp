

namespace Dignite.Abp.FieldCustomizing.Fields.Table;

public class TableFieldProvider : FieldProviderBase
{

    public const string ProviderName = "Table";

    public override string Name => ProviderName;

    public override string DisplayName => L["TableControl"];

    public override FieldType ControlType => FieldType.Complex;

    public override void Validate(FieldValidateArgs args)
    {
    }

    public override FieldConfigurationBase GetConfiguration(FieldConfigurationDictionary fieldConfiguration)
    {
        return new TableConfiguration(fieldConfiguration);
    }
}
