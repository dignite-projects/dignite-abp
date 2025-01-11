using Dignite.Abp.TenantDomains.Host.Localization;
using Volo.Abp.Application.Services;

namespace Dignite.Abp.TenantDomains.Host.Services;

/* Inherit your application services from this class. */
public abstract class TenantDomainsHostAppService : ApplicationService
{
    protected TenantDomainsHostAppService()
    {
        LocalizationResource = typeof(TenantDomainsHostResource);
    }
}