using Dignite.CmsKit.Localization;
using Volo.Abp.Application.Services;

namespace Dignite.CmsKit.Public;

public abstract class CmsKitPublicAppServiceBase : ApplicationService
{
    protected CmsKitPublicAppServiceBase()
    {
        LocalizationResource = typeof(DigniteCmsKitResource);
        ObjectMapperContext = typeof(DigniteCmsKitPublicApplicationModule);
    }
}
