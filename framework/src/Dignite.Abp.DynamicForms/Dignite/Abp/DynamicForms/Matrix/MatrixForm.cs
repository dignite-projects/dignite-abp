namespace Dignite.Abp.DynamicForms.Matrix;

/// <summary>
///
/// </summary>
public class MatrixForm : FormBase
{
    public const string MatrixFormName = "Matrix";

    public override string Name => MatrixFormName;

    public override string DisplayName => L["MatrixControl"];

    public override FormType FormType => FormType.Complex;

    public override void Validate(FormValidateArgs args)
    {
    }

    public override FormConfigurationBase GetConfiguration(FormConfigurationDictionary fieldConfiguration)
    {
        return new MatrixConfiguration(fieldConfiguration);
    }
}