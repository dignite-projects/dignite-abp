using Dignite.Cms.Localization;
using Volo.Abp.Application.Services;

namespace Dignite.Cms.Public;

public abstract class CmsPublicAppService : ApplicationService
{
    protected CmsPublicAppService()
    {
        LocalizationResource = typeof(CmsResource);
        ObjectMapperContext = typeof(CmsPublicApplicationModule);
    }
}
