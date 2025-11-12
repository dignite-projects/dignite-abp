using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Dignite.Abp.DynamicForms.TreeView;

public class TreeViewFormControl : FormControlBase
{
    public const string ControlName = "TreeView";

    public override string Name => ControlName;

    public override string DisplayName => L["FormControl:TreeView"];

    public override void Validate(FormControlValidateArgs args)
    {
        var configuration = new TreeViewConfiguration(args.Field.FormConfiguration);
        var value = args.Field.Value == null ? 
            new List<string>() 
            : args.Field.Value.GetType() == typeof(JsonElement)
                        ? JsonSerializer.Deserialize<List<string>>(args.Field.Value.ToString(), new JsonSerializerOptions(JsonSerializerDefaults.Web))  //This is the way to get data in webassembly mode.
                        : (List<string>)args.Field.Value;                                                                                               //This is the way to get data in blazorserver mode.

        if (args.Field.Value != null)
        {
            foreach (var item in value)
            {
                if (!configuration.Nodes.ContainsValue(item))
                {
                    args.ValidationErrors.Add(
                        new System.ComponentModel.DataAnnotations.ValidationResult(
                            L["Validate:InvalidSelection", args.Field.DisplayName],
                            new[] { args.Field.Name }
                            ));
                    return;
                }
            }
        }
        else
        {
            if (!value.Any() && args.Field.Required)
            {
                args.ValidationErrors.Add(
                    new System.ComponentModel.DataAnnotations.ValidationResult(
                        L["Validate:Required", args.Field.DisplayName],
                        new[] { args.Field.Name }
                        ));
            }
        }
    }

    public override FormConfigurationBase GetConfiguration(FormConfigurationDictionary fieldConfiguration)
    {
        return new TreeViewConfiguration(fieldConfiguration);
    }
}