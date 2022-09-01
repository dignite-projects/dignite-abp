using Dignite.Abp.FileManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Dignite.Abp.FileManagement;

public abstract class FileManagementController : AbpController
{
    protected FileManagementController()
    {
        LocalizationResource = typeof(FileManagementResource);
    }
}
