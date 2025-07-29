using Dignite.Publisher.Localization;
using Volo.Abp.Application.Services;

namespace Dignite.Publisher;

public abstract class PublisherAppServiceBase : ApplicationService
{
    protected PublisherAppServiceBase()
    {
        LocalizationResource = typeof(PublisherResource);
        ObjectMapperContext = typeof(PublisherCommonApplicationModule);
    }
}
