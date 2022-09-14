namespace Dignite.Abp.FieldCustomizing.Fields.RichTextEditor;

public class RichTextEditorFieldProvider : FieldProviderBase
{
    public const string ProviderName = "RichTextEditor";

    public override string Name => ProviderName;

    public override string DisplayName => L["RichTextEditorControl"];

    public override FieldType ControlType => FieldType.Simple;

    public override void Validate(FieldValidateArgs args)
    {
        var configuration = new RichTextEditorConfiguration(args.FieldDefinition.Configuration);

        if (configuration.Required && (args.Value == null || args.Value.ToString().Length == 0))
        {
            args.ValidationErrors.Add(
                new System.ComponentModel.DataAnnotations.ValidationResult(
                    L["ValidateValue:Required"],
                    new[] { args.FieldDefinition.Name }
                    ));
        }
    }

    public override FieldConfigurationBase GetConfiguration(FieldConfigurationDictionary fieldConfiguration)
    {
        return new RichTextEditorConfiguration(fieldConfiguration);
    }
}