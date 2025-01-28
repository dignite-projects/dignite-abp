using Dignite.Cms.Localization;

namespace Dignite.Abp.DynamicForms.Table;

public class TableFormControl : FormControlBase
{
    public const string ControlName = "Table";

    public override string Name => ControlName;

    public override string DisplayName => L["FormControl:Table"];

    public TableFormControl()
    {
        LocalizationResource = typeof(CmsResource);
    }

    public override void Validate(FormControlValidateArgs args)
    {
    }

    public override FormConfigurationBase GetConfiguration(FormConfigurationDictionary fieldConfiguration)
    {
        return new TableConfiguration(fieldConfiguration);
    }
}