using Dignite.Abp.DynamicForms;

namespace Dignite.Abp.DynamicForms.Table;

public class TableFormProvider : FormProviderBase
{
    public const string ProviderName = "Table";

    public override string Name => ProviderName;

    public override string DisplayName => L["TableControl"];

    public override FormType FormType => FormType.Complex;

    public override void Validate(FormValidateArgs args)
    {
    }

    public override FormConfigurationBase GetConfiguration(FormConfigurationDictionary fieldConfiguration)
    {
        return new TableConfiguration(fieldConfiguration);
    }
}