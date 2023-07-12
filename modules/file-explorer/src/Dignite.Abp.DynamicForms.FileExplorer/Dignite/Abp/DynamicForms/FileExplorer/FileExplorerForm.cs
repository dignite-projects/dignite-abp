using Dignite.Abp.DynamicForms.FileExplorer.Localization;

namespace Dignite.Abp.DynamicForms.FileExplorer;

public class FileExplorerForm : FormBase
{
    public const string FileExplorerFormName = "FileExplorer";

    public FileExplorerForm()
    {
        LocalizationResource = typeof(AbpDynamicFormsFileExplorerResource);
    }

    public override string Name => FileExplorerFormName;

    public override string DisplayName => L["FileExplorerFormDisplayName"];

    public override FormType FormType => FormType.Simple;

    public override void Validate(FormValidateArgs args)
    {
    }

    public override FormConfigurationBase GetConfiguration(FormConfigurationDictionary fieldConfiguration)
    {
        return new FileExplorerConfiguration(fieldConfiguration);
    }
}