using Dignite.Cms.Localization;

namespace Dignite.Cms.Public.Web.Views
{
    public abstract class CmsEntryRazorPage<TModel> : AbpRazorPage<TModel>
    {
        protected CmsEntryRazorPage()
        {
            LocalizationResourceType = typeof(CmsResource);
            ObjectMapperContext = typeof(CmsPublicWebModule);
        }
    }
}
