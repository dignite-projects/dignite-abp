using Dignite.Publisher.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Dignite.Publisher.Public;

public abstract class PublisherPublicController : AbpControllerBase
{
    protected PublisherPublicController()
    {
        LocalizationResource = typeof(PublisherResource);
    }
}
