using Dignite.Abp.DynamicForms.CkEditor.Localization;

namespace Dignite.Abp.DynamicForms.CkEditor;

public class CkEditorFormControl : FormControlBase
{
    public const string ControlName = "CkEditor";

    public override string Name => ControlName;

    public override string DisplayName => L["FormControl:CkEditor"];

    public CkEditorFormControl()
    {
        LocalizationResource = typeof(DigniteAbpDynamicFormsCkEditorResource);
    }

    public override void Validate(FormControlValidateArgs args)
    {
    }

    public override FormConfigurationBase GetConfiguration(FormConfigurationDictionary fieldConfiguration)
    {
        return new CkEditorConfiguration(fieldConfiguration);
    }
}