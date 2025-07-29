using Dignite.Publisher.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Dignite.Publisher;

public abstract class PublisherControllerBase : AbpControllerBase
{
    protected PublisherControllerBase()
    {
        LocalizationResource = typeof(PublisherResource);
    }
}
