using Dignite.Abp.TenantDomainManagement.Host.Localization;
using Volo.Abp.Application.Services;

namespace Dignite.Abp.TenantDomainManagement.Host.Services;

/* Inherit your application services from this class. */
public abstract class TenantDomainManagementHostAppService : ApplicationService
{
    protected TenantDomainManagementHostAppService()
    {
        LocalizationResource = typeof(TenantDomainManagementHostResource);
    }
}