using Dignite.Abp.DynamicForms;

namespace Dignite.Abp.DynamicForms.RichTextEditor;

public class RichTextEditorFormProvider : FormProviderBase
{
    public const string ProviderName = "RichTextEditor";

    public override string Name => ProviderName;

    public override string DisplayName => L["RichTextEditorControl"];

    public override FormType FormType => FormType.Simple;

    public override void Validate(FormValidateArgs args)
    {
        var configuration = new RichTextEditorConfiguration(args.Field.FormConfiguration);

        if (configuration.Required && (args.Value == null || args.Value.ToString().Length == 0))
        {
            args.ValidationErrors.Add(
                new System.ComponentModel.DataAnnotations.ValidationResult(
                    L["ValidateValue:Required"],
                    new[] { args.Field.Name }
                    ));
        }
    }

    public override FormConfigurationBase GetConfiguration(FormConfigurationDictionary fieldConfiguration)
    {
        return new RichTextEditorConfiguration(fieldConfiguration);
    }
}