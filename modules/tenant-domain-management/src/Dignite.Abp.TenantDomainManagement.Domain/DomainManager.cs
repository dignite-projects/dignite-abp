using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.Domain.Services;
using Volo.Abp.MultiTenancy;

namespace Dignite.Abp.TenantDomainManagement;


public class DomainManager(IDomainRepository domainRepository, IDomainNormalizer domainNormalizer) : DomainService
{
    protected IDataFilter DataFilter => LazyServiceProvider.LazyGetRequiredService<IDataFilter>();

    protected IDomainRepository DomainRepository { get; } = domainRepository;

    protected IDomainNormalizer DomainNormalizer { get; } = domainNormalizer;

    public virtual async Task<Domain> CreateAsync(string name, string business)
    {
        Check.NotNull(name, nameof(name));
        Check.NotNull(business, nameof(business));
        var normalizedName = DomainNormalizer.NormalizeName(name)!;
        await ValidateNameAsync(normalizedName);
        return new Domain(GuidGenerator.Create(), normalizedName, business, CurrentTenant.Id);
    }

    protected virtual async Task ValidateNameAsync(string normalizeName, Guid? expectedId = null)
    {
        using (DataFilter.Disable<IMultiTenant>())
        {
            var tenantDomain = await DomainRepository.FindByNameAsync(normalizeName);
            if (tenantDomain != null && tenantDomain.Id != expectedId)
            {
                throw new BusinessException("Volo.Abp.TenantDomainManagement:DuplicateDomainName").WithData("Name", normalizeName);
            }
        }
    }
}
