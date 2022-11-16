namespace Dignite.Abp.DynamicForms.Table;

public class TableForm : FormBase
{
    public const string TableFormName = "Table";

    public override string Name => TableFormName;

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