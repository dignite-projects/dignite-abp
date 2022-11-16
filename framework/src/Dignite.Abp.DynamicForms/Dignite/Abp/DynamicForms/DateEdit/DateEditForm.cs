using System;

namespace Dignite.Abp.DynamicForms.DateEdit;

/// <summary>
///
/// </summary>
public class DateEditForm : FormBase
{
    public const string DateEditFormName = "DateEdit";

    public override string Name => DateEditFormName;

    public override string DisplayName => L["DateEditControl"];

    public override FormType FormType => FormType.Simple;

    public override void Validate(FormValidateArgs args)
    {
        var configuration = new DateEditConfiguration(args.Field.FormConfiguration);
        DateTime value = DateTime.MinValue;

        if (configuration.Required && (args.Value == null || args.Value.ToString().Length == 0))
        {
            args.ValidationErrors.Add(
                new System.ComponentModel.DataAnnotations.ValidationResult(
                    L["ValidateValue:Required"],
                    new[] { args.Field.Name }
                    ));
        }
        else
        {
            if (DateTime.TryParse(args.Value.ToString(), out value))
            {
                args.ValidationErrors.Add(
                    new System.ComponentModel.DataAnnotations.ValidationResult(
                        L["ValidateValue:NotDateType"],
                        new[] { args.Field.Name }
                        ));
            }
        }

        if (value != DateTime.MinValue && configuration.Max.HasValue && configuration.Max.Value < value)
        {
            args.ValidationErrors.Add(
                new System.ComponentModel.DataAnnotations.ValidationResult(
                    L["ValueCannotBeGreaterThan", args.Field.DisplayName, configuration.Max.Value],
                    new[] { args.Field.Name }
                    ));
        }

        if (value != DateTime.MinValue && configuration.Min.HasValue && configuration.Min.Value > value)
        {
            args.ValidationErrors.Add(
                new System.ComponentModel.DataAnnotations.ValidationResult(
                    L["ValueCannotBeLessThan", args.Field.DisplayName, configuration.Min.Value],
                    new[] { args.Field.Name }
                    ));
        }
    }

    public override FormConfigurationBase GetConfiguration(FormConfigurationDictionary fieldConfiguration)
    {
        return new DateEditConfiguration(fieldConfiguration);
    }
}