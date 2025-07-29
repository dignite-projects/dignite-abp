using Dignite.Publisher.Localization;
using Volo.Abp.Application.Services;

namespace Dignite.Publisher.Public;

public abstract class PublisherPublicAppService : ApplicationService
{
    protected PublisherPublicAppService()
    {
        LocalizationResource = typeof(PublisherResource);
        ObjectMapperContext = typeof(PublisherPublicApplicationModule);
    }
}
