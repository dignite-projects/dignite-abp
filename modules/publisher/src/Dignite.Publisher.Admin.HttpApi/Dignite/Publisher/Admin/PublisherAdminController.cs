using Dignite.Publisher.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Dignite.Publisher.Admin;

public abstract class PublisherAdminController : AbpControllerBase
{
    protected PublisherAdminController()
    {
        LocalizationResource = typeof(PublisherResource);
    }
}
