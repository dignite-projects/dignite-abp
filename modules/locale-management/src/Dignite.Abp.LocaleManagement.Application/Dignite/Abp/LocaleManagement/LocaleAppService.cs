using System.Threading.Tasks;
using Dignite.Abp.LocaleManagement.Permissions;
using Microsoft.AspNetCore.Authorization;
using Dignite.Abp.Locales;
using System.Collections.Generic;
using Volo.Abp.SettingManagement;

namespace Dignite.Abp.LocaleManagement;

[Authorize]
public class LocaleAppService : LocaleManagementAppService, ILocaleAppService
{
    private readonly ISettingManager _settingManager;

    public LocaleAppService(ISettingManager settingManager)
    {
        _settingManager = settingManager;
    }

    public async Task<LocaleDto> GetAsync()
    {
        var locale = new LocaleDto();
        locale.DefaultCultureName = await SettingProvider.GetOrNullAsync(LocaleSettingNames.DefaultCultureName);
        locale.AvailableCultureNames = (await SettingProvider.GetOrNullAsync(LocaleSettingNames.AvailableCultureNames)).Split(',');

        return locale;
    }

    [Authorize(LocaleManagementPermissions.ManageRegions)]
    public async Task<LocaleDto> UpdateAsync(UpdateLocaleInput input)
    {
        await _settingManager.SetForTenantOrGlobalAsync(CurrentTenant.Id, LocaleSettingNames.DefaultCultureName, input.DefaultCultureName);
        await _settingManager.SetForTenantOrGlobalAsync(CurrentTenant.Id, LocaleSettingNames.AvailableCultureNames, input.AvailableCultureNames.JoinAsString(","));

        return new LocaleDto(input.DefaultCultureName,input.AvailableCultureNames);
    }
}
