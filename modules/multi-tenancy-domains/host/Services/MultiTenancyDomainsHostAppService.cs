using Dignite.Abp.MultiTenancyDomains.Host.Localization;
using Volo.Abp.Application.Services;

namespace Dignite.Abp.MultiTenancyDomains.Host.Services;

/* Inherit your application services from this class. */
public abstract class MultiTenancyDomainsHostAppService : ApplicationService
{
    protected MultiTenancyDomainsHostAppService()
    {
        LocalizationResource = typeof(MultiTenancyDomainsHostResource);
    }
}