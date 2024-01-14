using System;

namespace Dignite.Abp.DynamicForms.Textbox;

public class TextEditFormControl : FormControlBase
{
    public const string ControlName = "TextEdit";

    public override string Name => ControlName;

    public override string DisplayName => L["FormControl:TextEdit"];

    public override void Validate(FormControlValidateArgs args)
    {
        var configuration = new TextEditConfiguration(args.Field.FormConfiguration);

        if (args.Field.Value != null && !args.Field.Value.ToString().IsNullOrWhiteSpace())
        {
            if (configuration.CharLimit < args.Field.Value.ToString().Length)
            {
                args.ValidationErrors.Add(
                    new System.ComponentModel.DataAnnotations.ValidationResult(
                        L["Validate:CharacterCountExceedsLimit", args.Field.DisplayName, configuration.CharLimit],
                        new[] { args.Field.Name }
                        ));
            }
        }
    }

    public override FormConfigurationBase GetConfiguration(FormConfigurationDictionary fieldConfiguration)
    {
        return new TextEditConfiguration(fieldConfiguration);
    }
}