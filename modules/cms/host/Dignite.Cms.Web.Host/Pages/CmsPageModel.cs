using Dignite.Cms.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Dignite.Cms.Pages;

public abstract class CmsPageModel : AbpPageModel
{
    protected CmsPageModel()
    {
        LocalizationResourceType = typeof(CmsResource);
    }
}
