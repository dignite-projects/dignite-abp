using Dignite.Abp.DynamicForms.FileExplorer.Localization;

namespace Dignite.Abp.DynamicForms.Components.FileExplorer.Components.FileExplorer;
public partial class FileExplorerConfigurationComponent
{
    public FileExplorerConfigurationComponent()
    {
        LocalizationResource = typeof(AbpDynamicFormsFileExplorerResource);
    }
}
