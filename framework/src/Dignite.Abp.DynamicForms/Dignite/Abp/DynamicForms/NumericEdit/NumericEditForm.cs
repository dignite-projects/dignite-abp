using Dignite.Abp.DynamicForms;

namespace Dignite.Abp.DynamicForms.NumericEdit;

/// <summary>
///
/// </summary>
public class NumericEditForm : FormBase
{
    public const string NumericEditFormName = "NumericEdit";

    public override string Name => NumericEditFormName;

    public override string DisplayName => L["NumericEditControl"];

    public override FormType FormType => FormType.Simple;

    public override void Validate(FormValidateArgs args)
    {
        var configuration = new NumericEditConfiguration(args.Field.FormConfiguration);
        decimal value = decimal.MinValue;

        if (configuration.Required && (args.Value == null || args.Value.ToString().Length == 0))
        {
            args.ValidationErrors.Add(
                new System.ComponentModel.DataAnnotations.ValidationResult(
                    L["ValidateValue:Required", args.Field.DisplayName],
                    new[] { args.Field.Name }
                    ));
        }
        else
        {
            if (decimal.TryParse(args.Value.ToString(), out value))
            {
                args.ValidationErrors.Add(
                    new System.ComponentModel.DataAnnotations.ValidationResult(
                        L["ValidateValue:NotNumericType", args.Field.DisplayName],
                        new[] { args.Field.Name }
                        ));
            }
        }

        if (value != decimal.MinValue && configuration.Max.HasValue && configuration.Max.Value < value)
        {
            args.ValidationErrors.Add(
                new System.ComponentModel.DataAnnotations.ValidationResult(
                    L["ValidateValue:CannotBeGreaterThan", args.Field.DisplayName, configuration.Max.Value],
                    new[] { args.Field.Name }
                    ));
        }

        if (value != decimal.MinValue && configuration.Min.HasValue && configuration.Min.Value > value)
        {
            args.ValidationErrors.Add(
                new System.ComponentModel.DataAnnotations.ValidationResult(
                    L["ValidateValue:CannotBeLessThan", args.Field.DisplayName, configuration.Min.Value],
                    new[] { args.Field.Name }
                    ));
        }
    }

    public override FormConfigurationBase GetConfiguration(FormConfigurationDictionary fieldConfiguration)
    {
        return new NumericEditConfiguration(fieldConfiguration);
    }
}