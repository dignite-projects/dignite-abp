using System;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.TenantDomainManagement.TestBase;
public class TenantDomainManagementTestData: ISingletonDependency
{
    public string TenantName { get; } = "abc";
    public string DomainName { get; } = "abc.dignite.com";
    public Guid TenantDomain1Id { get; } = Guid.NewGuid();
    public Guid Tenant1Id { get; } = Guid.NewGuid();
    public Guid TenantDomain2Id { get; } = Guid.NewGuid();
    public Guid Tenant2Id { get; } = Guid.NewGuid();
    public Guid Tenant3Id { get; } = Guid.NewGuid();

    public Guid App1Id { get; set; } = Guid.NewGuid();
    public string App1ClientId { get; set; } = "Client1";
    public Guid App2Id { get; set; } = Guid.NewGuid();
    public string App2ClientId { get; set; } = "Client2";

    public Guid Scope1Id { get; set; } = Guid.NewGuid();
    public string Scope1Name { get; set; } = "Scope1";
    public Guid Scope2Id { get; set; } = Guid.NewGuid();
    public string Scope2Name { get; set; } = "Scope2";

    public Guid Token1Id { get; set; } = Guid.NewGuid();

    public Guid Token2Id { get; set; } = Guid.NewGuid();

    public Guid Authorization1Id { get; set; } = Guid.NewGuid();

    public Guid Authorization2Id { get; set; } = Guid.NewGuid();
}
