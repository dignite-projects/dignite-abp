using System.Threading.Tasks;
using Dignite.Abp.MultiTenancyDomains.TestBase;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;

namespace Dignite.Abp.MultiTenancyDomains;

public class MultiTenancyDomainsDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IGuidGenerator _guidGenerator;
    private readonly ICurrentTenant _currentTenant;
    private readonly ITenantDomainRepository _tenantDomainRepository;
    private readonly MultiTenancyDomainsTestData _tenantDomainTestData;

    public MultiTenancyDomainsDataSeedContributor(
        IGuidGenerator guidGenerator, ICurrentTenant currentTenant, ITenantDomainRepository tenantDomainRepository, MultiTenancyDomainsTestData tenantDomainTestData)
    {
        _guidGenerator = guidGenerator;
        _currentTenant = currentTenant;
        _tenantDomainRepository = tenantDomainRepository;
        _tenantDomainTestData = tenantDomainTestData;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        /* Instead of returning the Task.CompletedTask, you can insert your test data
         * at this point!
         */

        using (_currentTenant.Change(context?.TenantId))
        {
            await SeedTenantDomainsAsync();
        }
    }

    private async Task SeedTenantDomainsAsync()
    {
        await _tenantDomainRepository.InsertAsync(
            new TenantDomain(
                _tenantDomainTestData.TenantDomain1Id,
                _tenantDomainTestData.DomainName1,
                null
            ));
    }
}
