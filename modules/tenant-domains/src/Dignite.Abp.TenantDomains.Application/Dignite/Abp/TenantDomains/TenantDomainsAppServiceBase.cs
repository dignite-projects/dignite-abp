using Dignite.Abp.TenantDomains.Localization;
using Volo.Abp.Application.Services;

namespace Dignite.Abp.TenantDomains;

public abstract class TenantDomainsAppServiceBase : ApplicationService
{
    protected TenantDomainsAppServiceBase()
    {
        LocalizationResource = typeof(TenantDomainsResource);
        ObjectMapperContext = typeof(AbpTenantDomainsApplicationModule);
    }
}
