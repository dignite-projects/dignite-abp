using Dignite.CmsKit.Localization;
using Volo.Abp.Application.Services;

namespace Dignite.CmsKit.Admin;

public abstract class CmsKitAdminAppServiceBase : ApplicationService
{
    protected CmsKitAdminAppServiceBase()
    {
        LocalizationResource = typeof(DigniteCmsKitResource);
        ObjectMapperContext = typeof(DigniteCmsKitAdminApplicationModule);
    }
}
