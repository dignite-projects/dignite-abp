using Dignite.Cms.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Dignite.Abp.DynamicForms.Entry;

/// <summary>
///
/// </summary>
public class EntryFormControl : FormControlBase
{
    public const string ControlName = "Entry";

    public override string Name => ControlName;

    public override string DisplayName => L["FormControl:Entry"];

    public EntryFormControl()
    {
        LocalizationResource = typeof(CmsResource);
    }

    public override void Validate(FormControlValidateArgs args)
    {
        var configuration = new EntryConfiguration(args.Field.FormConfiguration);
        var value = args.Field.Value == null ?
            new List<Guid>()
            : args.Field.Value.GetType() == typeof(JsonElement)
                        ? JsonSerializer.Deserialize<List<Guid>>(args.Field.Value.ToString(), new JsonSerializerOptions(JsonSerializerDefaults.Web))  //This is the way to get data in webassembly mode.
                        : (List<Guid>)args.Field.Value;                                                                                               //This is the way to get data in blazorserver mode.


        if ((value == null || !value.Any()) && args.Field.Required)
        {
            args.ValidationErrors.Add(
                new System.ComponentModel.DataAnnotations.ValidationResult(
                    L["Validate:Required", args.Field.DisplayName],
                    new[] { args.Field.Name }
                    ));
        }
    }

    public override FormConfigurationBase GetConfiguration(FormConfigurationDictionary fieldConfiguration)
    {
        return new EntryConfiguration(fieldConfiguration);
    }
}