using System.Threading.Tasks;
using Dignite.Abp.RegionalizationManagement.Permissions;
using Microsoft.AspNetCore.Authorization;
using Dignite.Abp.Regionalization;
using System.Collections.Generic;
using Volo.Abp.SettingManagement;

namespace Dignite.Abp.RegionalizationManagement;

[Authorize]
public class RegionalizationAppService : RegionalizationManagementAppService, IRegionalizationAppService
{
    private readonly ISettingManager _settingManager;

    public RegionalizationAppService(ISettingManager settingManager)
    {
        _settingManager = settingManager;
    }

    public async Task<RegionalizationDto> GetAsync()
    {
        var regionalization = new RegionalizationDto();
        regionalization.DefaultCultureName = await SettingProvider.GetOrNullAsync(RegionalizationSettingNames.DefaultCultureName);
        regionalization.AvailableCultureNames = (await SettingProvider.GetOrNullAsync(RegionalizationSettingNames.AvailableCultureNames)).Split(',');

        return regionalization;
    }

    [Authorize(RegionalizationManagementPermissions.ManageRegions)]
    public async Task<RegionalizationDto> UpdateAsync(UpdateRegionalizationInput input)
    {
        await _settingManager.SetForTenantOrGlobalAsync(CurrentTenant.Id, RegionalizationSettingNames.DefaultCultureName, input.DefaultCultureName);
        await _settingManager.SetForTenantOrGlobalAsync(CurrentTenant.Id, RegionalizationSettingNames.AvailableCultureNames, input.AvailableCultureNames.JoinAsString(","));

        return new RegionalizationDto(input.DefaultCultureName,input.AvailableCultureNames);
    }
}
