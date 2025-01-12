using System.Collections.Generic;
using Dignite.Abp.DynamicForms.FileExplorer.Localization;

namespace Dignite.Abp.DynamicForms.FileExplorer;

public class FileExplorerFormControl : FormControlBase
{
    public const string ControlName = "FileExplorer";

    public FileExplorerFormControl()
    {
        LocalizationResource = typeof(AbpDynamicFormsFileExplorerResource);
    }

    public override string Name => ControlName;

    public override string DisplayName => L["FormControl:FileExplorer"];

    public override void Validate(FormControlValidateArgs args)
    {
        if (args.Field.Required && args.Field.Value == null)
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
        return new FileExplorerConfiguration(fieldConfiguration);
    }
}