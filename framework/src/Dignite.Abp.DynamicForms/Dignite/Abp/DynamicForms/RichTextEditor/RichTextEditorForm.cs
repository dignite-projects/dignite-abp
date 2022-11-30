namespace Dignite.Abp.DynamicForms.RichTextEditor;

public class RichTextEditorForm : FormBase
{
    public const string RichTextEditorFormName = "RichTextEditor";

    public override string Name => RichTextEditorFormName;

    public override string DisplayName => L["RichTextEditorControl"];

    public override FormType FormType => FormType.Simple;

    public override void Validate(FormValidateArgs args)
    {
        var configuration = new RichTextEditorConfiguration(args.Field.FormConfiguration);

        if (configuration.Required && (args.Value == null || args.Value.ToString().Length == 0))
        {
            args.ValidationErrors.Add(
                new System.ComponentModel.DataAnnotations.ValidationResult(
                    L["ValidateValue:Required", args.Field.DisplayName],
                    new[] { args.Field.Name }
                    ));
        }
    }

    public override FormConfigurationBase GetConfiguration(FormConfigurationDictionary fieldConfiguration)
    {
        return new RichTextEditorConfiguration(fieldConfiguration);
    }
}