using Dignite.Abp.DynamicForms.FileExplorer.Localization;

namespace Dignite.Abp.DynamicForms.Components.FileExplorer.Components.FileExplorer;
public partial class FileExplorerFormConfigurationComponent
{
    public FileExplorerFormConfigurationComponent()
    {
        LocalizationResource = typeof(AbpDynamicFormsFileExplorerResource);
    }
}
