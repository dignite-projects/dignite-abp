using System.Threading.Tasks;
using Dignite.Abp.BlobStoring;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;

namespace Dignite.CmsKit.BlobStoring;

public class BrandLogoReverseBlobNameGenerator : IBlobNameGenerator, ITransientDependency
{
    private readonly ICurrentTenant _currentTenant;

    public BrandLogoReverseBlobNameGenerator(ICurrentTenant currentTenant)
    {
        _currentTenant = currentTenant;
    }

    public async Task<string> Create()
    {
        if (_currentTenant.IsAvailable)
        {
            return $"{_currentTenant.Name}-logo-reverse";
        }
        else
        {
            return "logo-reverse";
        }
    }
}
