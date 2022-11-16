namespace Dignite.Abp.DynamicForms.Textbox;

public class TextboxForm : FormBase
{
    public const string TextboxFormName = "Textbox";

    public override string Name => TextboxFormName;

    public override string DisplayName => L["TextboxControl"];

    public override FormType FormType => FormType.Simple;

    public override void Validate(FormValidateArgs args)
    {
        var configuration = new TextboxConfiguration(args.Field.FormConfiguration);

        if (configuration.Required && (args.Value == null || args.Value.ToString().Length == 0))
        {
            args.ValidationErrors.Add(
                new System.ComponentModel.DataAnnotations.ValidationResult(
                    L["ValidateValue:Required"],
                    new[] { args.Field.Name }
                    ));
        }

        if (args.Value != null && configuration.CharLimit < args.Value.ToString().Length)
        {
            args.ValidationErrors.Add(
                new System.ComponentModel.DataAnnotations.ValidationResult(
                    L["CharacterCountExceedsLimit", args.Field.DisplayName, configuration.CharLimit],
                    new[] { args.Field.Name }
                    ));
        }
    }

    public override FormConfigurationBase GetConfiguration(FormConfigurationDictionary fieldConfiguration)
    {
        return new TextboxConfiguration(fieldConfiguration);
    }
}