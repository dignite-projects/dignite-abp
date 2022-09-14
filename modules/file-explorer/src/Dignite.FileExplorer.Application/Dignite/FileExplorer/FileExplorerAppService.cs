using Dignite.FileExplorer.Localization;
using Volo.Abp.Application.Services;

namespace Dignite.FileExplorer;

public abstract class FileExplorerAppService : ApplicationService
{
    protected FileExplorerAppService()
    {
        LocalizationResource = typeof(FileExplorerResource);
        ObjectMapperContext = typeof(FileExplorerApplicationModule);
    }
}