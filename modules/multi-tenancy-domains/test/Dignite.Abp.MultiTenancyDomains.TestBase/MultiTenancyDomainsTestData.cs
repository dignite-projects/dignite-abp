using System;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.MultiTenancyDomains.TestBase;
public class MultiTenancyDomainsTestData: ISingletonDependency
{
    public Guid TenantDomain1Id { get; } = Guid.NewGuid();

    public string DomainName1 { get; } = "dignite.com";
}
