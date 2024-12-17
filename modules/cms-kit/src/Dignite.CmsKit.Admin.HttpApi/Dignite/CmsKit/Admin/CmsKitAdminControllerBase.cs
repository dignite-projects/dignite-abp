using Dignite.CmsKit.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Dignite.CmsKit.Admin;

public abstract class CmsKitAdminControllerBase : AbpControllerBase
{
    protected CmsKitAdminControllerBase()
    {
        LocalizationResource = typeof(DigniteCmsKitResource);
    }
}
