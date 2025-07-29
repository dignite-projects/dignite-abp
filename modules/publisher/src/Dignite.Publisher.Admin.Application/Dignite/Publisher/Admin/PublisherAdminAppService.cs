using Dignite.Publisher.Localization;
using Volo.Abp.Application.Services;

namespace Dignite.Publisher.Admin;

public abstract class PublisherAdminAppService : ApplicationService
{
    protected PublisherAdminAppService()
    {
        LocalizationResource = typeof(PublisherResource);
        ObjectMapperContext = typeof(PublisherAdminApplicationModule);
    }
}
