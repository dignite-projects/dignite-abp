using System;

namespace Dignite.Abp.FieldCustomizing.Forms.DateEdit;

/// <summary>
///
/// </summary>
public class DateEditFormProvider : FormProviderBase
{
    public const string ProviderName = "DateEdit";

    public override string Name => ProviderName;

    public override string DisplayName => L["DateEditControl"];

    public override FormType FormType => FormType.Simple;

    public override void Validate(FormValidateArgs args)
    {
        var configuration = new DateEditConfiguration(args.FieldDefinition.Configuration);
        DateTime value = DateTime.MinValue;

        if (configuration.Required && (args.Value == null || args.Value.ToString().Length == 0))
        {
            args.ValidationErrors.Add(
                new System.ComponentModel.DataAnnotations.ValidationResult(
                    L["ValidateValue:Required"],
                    new[] { args.FieldDefinition.Name }
                    ));
        }
        else
        {
            if (DateTime.TryParse(args.Value.ToString(), out value))
            {
                args.ValidationErrors.Add(
                    new System.ComponentModel.DataAnnotations.ValidationResult(
                        L["ValidateValue:NotDateType"],
                        new[] { args.FieldDefinition.Name }
                        ));
            }
        }

        if (value != DateTime.MinValue && configuration.Max.HasValue && configuration.Max.Value < value)
        {
            args.ValidationErrors.Add(
                new System.ComponentModel.DataAnnotations.ValidationResult(
                    L["ValueCannotBeGreaterThan", args.FieldDefinition.DisplayName, configuration.Max.Value],
                    new[] { args.FieldDefinition.Name }
                    ));
        }

        if (value != DateTime.MinValue && configuration.Min.HasValue && configuration.Min.Value > value)
        {
            args.ValidationErrors.Add(
                new System.ComponentModel.DataAnnotations.ValidationResult(
                    L["ValueCannotBeLessThan", args.FieldDefinition.DisplayName, configuration.Min.Value],
                    new[] { args.FieldDefinition.Name }
                    ));
        }
    }

    public override FormConfigurationBase GetConfiguration(FormConfigurationDictionary fieldConfiguration)
    {
        return new DateEditConfiguration(fieldConfiguration);
    }
}