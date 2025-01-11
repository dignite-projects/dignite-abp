using System;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.TenantDomains.TestBase;
public class TenantDomainTestData: ISingletonDependency
{
    public Guid TenantDomain1Id { get; } = Guid.NewGuid();

    public string DomainName1 { get; } = "dignite.com";
}
