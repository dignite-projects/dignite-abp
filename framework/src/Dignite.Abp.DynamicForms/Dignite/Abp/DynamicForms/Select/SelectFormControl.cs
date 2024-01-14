using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Dignite.Abp.DynamicForms.Select;

public class SelectFormControl : FormControlBase
{
    public const string ControlName = "Select";

    public override string Name => ControlName;

    public override string DisplayName => L["FormControl:Select"];

    public override void Validate(FormControlValidateArgs args)
    {
        var configuration = new SelectConfiguration(args.Field.FormConfiguration);

        if (args.Field.Value != null && !args.Field.Value.ToString().IsNullOrWhiteSpace())
        {
            var value = JsonSerializer.Deserialize<List<string>>(args.Field.Value.ToString());
            if (value.Except(configuration.Options.Select(x => x.Value)).Any())
            {
                args.ValidationErrors.Add(
                    new System.ComponentModel.DataAnnotations.ValidationResult(
                        L["Validate:InvalidSelection", args.Field.DisplayName],
                        new[] { args.Field.Name }
                        ));
            }
        }
    }

    public override FormConfigurationBase GetConfiguration(FormConfigurationDictionary fieldConfiguration)
    {
        return new SelectConfiguration(fieldConfiguration);
    }
}