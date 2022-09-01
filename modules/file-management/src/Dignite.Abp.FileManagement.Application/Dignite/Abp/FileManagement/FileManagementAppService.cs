using Dignite.Abp.FileManagement.Localization;
using Volo.Abp.Application.Services;

namespace Dignite.Abp.FileManagement;

public abstract class FileManagementAppService : ApplicationService
{
    protected FileManagementAppService()
    {
        LocalizationResource = typeof(FileManagementResource);
        ObjectMapperContext = typeof(FileManagementApplicationModule);
    }
}
