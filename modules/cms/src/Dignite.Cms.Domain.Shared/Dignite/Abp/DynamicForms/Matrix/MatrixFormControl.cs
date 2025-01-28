using Dignite.Cms.Localization;

namespace Dignite.Abp.DynamicForms.Matrix;

/// <summary>
///
/// </summary>
public class MatrixFormControl : FormControlBase
{
    public const string ControlName = "Matrix";

    public override string Name => ControlName;

    public override string DisplayName => L["FormControl:Matrix"];

    public MatrixFormControl()
    {
        LocalizationResource = typeof(CmsResource);
    }

    public override void Validate(FormControlValidateArgs args)
    {
    }

    public override FormConfigurationBase GetConfiguration(FormConfigurationDictionary fieldConfiguration)
    {
        return new MatrixConfiguration(fieldConfiguration);
    }
}