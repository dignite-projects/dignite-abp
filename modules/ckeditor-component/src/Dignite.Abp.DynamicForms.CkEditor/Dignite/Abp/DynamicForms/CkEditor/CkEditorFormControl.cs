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
        if (args.Field.Required && args.Field.Value == null)
        {
            args.ValidationErrors.Add(
                new System.ComponentModel.DataAnnotations.ValidationResult(
                    L["Validate:Required", args.Field.DisplayName],
                    new[] { args.Field.Name }
                    ));
        }
    }

    public override FormConfigurationBase GetConfiguration(FormConfigurationDictionary fieldConfiguration)
    {
        return new CkEditorConfiguration(fieldConfiguration);
    }
}