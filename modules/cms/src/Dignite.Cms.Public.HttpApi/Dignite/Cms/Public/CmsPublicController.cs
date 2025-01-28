using Dignite.Cms.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Dignite.Cms.Public;

public abstract class CmsPublicController : AbpControllerBase
{
    protected CmsPublicController()
    {
        LocalizationResource = typeof(CmsResource);
    }
}
