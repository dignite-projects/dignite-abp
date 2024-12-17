using Dignite.CmsKit.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Dignite.CmsKit.Public;

public abstract class CmsKitPublicControllerBase : AbpControllerBase
{
    protected CmsKitPublicControllerBase()
    {
        LocalizationResource = typeof(DigniteCmsKitResource);
    }
}
