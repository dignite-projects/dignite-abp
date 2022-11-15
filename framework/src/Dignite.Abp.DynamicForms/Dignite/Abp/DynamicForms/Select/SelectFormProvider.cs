using Dignite.Abp.DynamicForms;

namespace Dignite.Abp.DynamicForms.Select;

public class SelectFormProvider : FormProviderBase
{
    public const string ProviderName = "Select";

    public override string Name => ProviderName;

    public override string DisplayName => L["SelectControl"];

    public override FormType FormType => FormType.Simple;

    public override void Validate(FormValidateArgs args)
    {
        var configuration = new SelectConfiguration(args.Field.FormConfiguration);

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
        return new SelectConfiguration(fieldConfiguration);
    }
}