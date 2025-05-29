using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.TenantDomainManagement;

public abstract class DomainProvider(IDomainStore domainStore) : IDomainProvider, ITransientDependency
{
    public abstract string Name { get; }

    protected IDomainStore DomainStore { get; } = domainStore;


}
