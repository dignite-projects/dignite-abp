using Dignite.Cms.Localization;
using Volo.Abp.Application.Services;

namespace Dignite.Cms.Admin
{
    public abstract class CmsAdminAppServiceBase : ApplicationService
    {
        protected CmsAdminAppServiceBase()
        {
            LocalizationResource = typeof(CmsResource);
            ObjectMapperContext = typeof(CmsAdminApplicationModule);
        }
    }
}
