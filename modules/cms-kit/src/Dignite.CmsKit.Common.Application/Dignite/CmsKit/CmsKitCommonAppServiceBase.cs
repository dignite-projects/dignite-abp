using Dignite.CmsKit.Localization;
using Volo.Abp.Application.Services;

namespace Dignite.CmsKit;

public abstract class CmsKitCommonAppServiceBase : ApplicationService
{
    protected CmsKitCommonAppServiceBase()
    {
        LocalizationResource = typeof(DigniteCmsKitResource);
        ObjectMapperContext = typeof(DigniteCmsKitCommonApplicationModule);
    }
}
