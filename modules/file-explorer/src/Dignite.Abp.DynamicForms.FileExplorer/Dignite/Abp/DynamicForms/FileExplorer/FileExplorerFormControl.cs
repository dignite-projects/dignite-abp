using Dignite.Abp.DynamicForms.FileExplorer.Localization;

namespace Dignite.Abp.DynamicForms.FileExplorer;

public class FileExplorerFormControl : FormControlBase
{
    public const string ControlName = "FileExplorer";

    public FileExplorerFormControl()
    {
        LocalizationResource = typeof(DigniteAbpDynamicFormsFileExplorerResource);
    }

    public override string Name => ControlName;

    public override string DisplayName => L["FormControl:FileExplorer"];

    public override void Validate(FormControlValidateArgs args)
    {
    }

    public override FormConfigurationBase GetConfiguration(FormConfigurationDictionary fieldConfiguration)
    {
        return new FileExplorerConfiguration(fieldConfiguration);
    }
}