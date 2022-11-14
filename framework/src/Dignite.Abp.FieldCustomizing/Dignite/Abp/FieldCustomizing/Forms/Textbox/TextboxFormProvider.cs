namespace Dignite.Abp.FieldCustomizing.Forms.Textbox;

public class TextboxFormProvider : FormProviderBase
{
    public const string ProviderName = "Textbox";

    public override string Name => ProviderName;

    public override string DisplayName => L["TextboxControl"];

    public override FormType FormType => FormType.Simple;

    public override void Validate(FormValidateArgs args)
    {
        var configuration = new TextboxConfiguration(args.FieldDefinition.Configuration);

        if (configuration.Required && (args.Value == null || args.Value.ToString().Length == 0))
        {
            args.ValidationErrors.Add(
                new System.ComponentModel.DataAnnotations.ValidationResult(
                    L["ValidateValue:Required"],
                    new[] { args.FieldDefinition.Name }
                    ));
        }

        if (args.Value != null && configuration.CharLimit < args.Value.ToString().Length)
        {
            args.ValidationErrors.Add(
                new System.ComponentModel.DataAnnotations.ValidationResult(
                    L["CharacterCountExceedsLimit", args.FieldDefinition.DisplayName, configuration.CharLimit],
                    new[] { args.FieldDefinition.Name }
                    ));
        }
    }

    public override FormConfigurationBase GetConfiguration(FormConfigurationDictionary fieldConfiguration)
    {
        return new TextboxConfiguration(fieldConfiguration);
    }
}