namespace Dignite.Abp.TenantDomainManagement;
public interface IDomainNormalizer
{
    string? NormalizeName(string? name);
}
