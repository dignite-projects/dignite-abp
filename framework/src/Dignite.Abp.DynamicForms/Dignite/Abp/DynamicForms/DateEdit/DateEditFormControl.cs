using System;

namespace Dignite.Abp.DynamicForms.DateEdit;

/// <summary>
///
/// </summary>
public class DateEditFormControl : FormControlBase
{
    public const string ControlName = "DateEdit";

    public override string Name => ControlName;

    public override string DisplayName => L["FormControl:DateEdit"];

    public override void Validate(FormControlValidateArgs args)
    {
        var configuration = new DateEditConfiguration(args.Field.FormConfiguration);

        if (args.Field.Value != null && !args.Field.Value.ToString().IsNullOrWhiteSpace())
        {
            DateTime value;
            if (!DateTime.TryParse(args.Field.Value.ToString(), out value))
            {
                args.ValidationErrors.Add(
                    new System.ComponentModel.DataAnnotations.ValidationResult(
                        L["Validate:NotDateType", args.Field.DisplayName],
                        new[] { args.Field.Name }
                        ));
            }

            if (configuration.Max.HasValue && configuration.Max.Value < value)
            {
                args.ValidationErrors.Add(
                    new System.ComponentModel.DataAnnotations.ValidationResult(
                        L["Validate:CannotBeGreaterThan", args.Field.DisplayName, configuration.Max.Value],
                        new[] { args.Field.Name }
                        ));
            }

            if (configuration.Min.HasValue && configuration.Min.Value > value)
            {
                args.ValidationErrors.Add(
                    new System.ComponentModel.DataAnnotations.ValidationResult(
                        L["Validate:CannotBeLessThan", args.Field.DisplayName, configuration.Min.Value],
                        new[] { args.Field.Name }
                        ));
            }
        }
    }

    public override FormConfigurationBase GetConfiguration(FormConfigurationDictionary fieldConfiguration)
    {
        return new DateEditConfiguration(fieldConfiguration);
    }
}