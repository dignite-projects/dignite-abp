using Dignite.FileExplorer.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Dignite.FileExplorer;

public abstract class FileExplorerController : AbpControllerBase
{
    protected FileExplorerController()
    {
        LocalizationResource = typeof(FileExplorerResource);
    }
}