using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Dignite.Abp.RegionalizationManagement;

public interface IRegionalizationAppService : IApplicationService
{
    Task<RegionalizationDto> GetAsync();

    Task<RegionalizationDto> UpdateAsync(UpdateRegionalizationInput input);
}
