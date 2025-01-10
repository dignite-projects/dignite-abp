using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace Dignite.Abp.RegionalizationManagement;

[Area(RegionalizationManagementRemoteServiceConsts.ModuleName)]
[RemoteService(Name = RegionalizationManagementRemoteServiceConsts.RemoteServiceName)]
[Route("api/regionalization-management/regionalization")]
public class RegionalizationController : RegionalizationManagementController, IRegionalizationAppService
{
    private readonly IRegionalizationAppService _regionalizationAppService;

    public RegionalizationController(IRegionalizationAppService regionalizationAppService)
    {
        _regionalizationAppService = regionalizationAppService;
    }

    [HttpGet]
    public async Task<RegionalizationDto> GetAsync()
    {
        return await _regionalizationAppService.GetAsync();
    }

    [HttpPost]
    public async Task<RegionalizationDto> UpdateAsync(UpdateRegionalizationInput input)
    {
        return await _regionalizationAppService.UpdateAsync(input);
    }
}
