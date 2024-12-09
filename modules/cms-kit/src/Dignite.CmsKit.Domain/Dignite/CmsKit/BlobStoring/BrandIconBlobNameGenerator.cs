using System.Threading.Tasks;
using Dignite.Abp.BlobStoring;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;

namespace Dignite.CmsKit.BlobStoring;

public class BrandIconBlobNameGenerator : IBlobNameGenerator, ITransientDependency
{
    private readonly ICurrentTenant _currentTenant;

    public BrandIconBlobNameGenerator(ICurrentTenant currentTenant)
    {
        _currentTenant = currentTenant;
    }

    public async Task<string> Create()
    {
        if (_currentTenant.IsAvailable)
        {
            return $"{_currentTenant.Name}-icon";
        }
        else
        {
            return "icon";
        }
    }
}
