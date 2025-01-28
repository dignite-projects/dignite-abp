using Dignite.Cms.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Dignite.Cms.Admin
{
    public abstract class CmsAdminController : AbpControllerBase
    {
        protected CmsAdminController()
        {
            LocalizationResource = typeof(CmsResource);
        }
    }
}
