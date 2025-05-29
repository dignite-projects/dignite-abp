using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.TenantDomainManagement;
public class LowerInvariantDomainNormalizer : IDomainNormalizer, ITransientDependency
{
    public string? NormalizeName(string? name)
    {
        return name?.Normalize().ToLowerInvariant().Trim();
    }
}
