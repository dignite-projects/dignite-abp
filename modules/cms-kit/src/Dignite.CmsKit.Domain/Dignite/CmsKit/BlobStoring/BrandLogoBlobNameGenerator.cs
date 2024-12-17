using System.Threading.Tasks;
using Dignite.Abp.BlobStoring;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;

namespace Dignite.CmsKit.BlobStoring;

public class BrandLogoBlobNameGenerator : IBlobNameGenerator, ITransientDependency
{
    private readonly ICurrentTenant _currentTenant;

    public BrandLogoBlobNameGenerator(ICurrentTenant currentTenant)
    {
        _currentTenant = currentTenant;
    }

    public async Task<string> Create()
    {
        if (_currentTenant.IsAvailable)
        {
            return $"{_currentTenant.Name}-logo";
        }
        else
        {
            return "logo";
        }
    }
}
