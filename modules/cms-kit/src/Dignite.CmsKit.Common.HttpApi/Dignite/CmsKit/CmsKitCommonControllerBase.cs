using Dignite.CmsKit.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Dignite.CmsKit;

public abstract class CmsKitCommonControllerBase : AbpControllerBase
{
    protected CmsKitCommonControllerBase()
    {
        LocalizationResource = typeof(DigniteCmsKitResource);
    }
}
