using System.Collections.Generic;
using System.Threading.Tasks;
using Dignite.Abp.Settings.DynamicForms;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.SettingManagement;
using Volo.Abp.Settings;

namespace Dignite.Abp.SettingManagement;

[Authorize(SettingManagementPermissions.Tenant)]
public class TenantSettingsAppService : SettingsAppServiceBase, ITenantSettingsAppService
{
    public TenantSettingsAppService(
        ISettingDefinitionFormManager settingDefinitionManager,
        ISettingManager settingManager)
        : base(settingDefinitionManager, settingManager)
    {
    }

    public async Task UpdateAsync(UpdateTenantSettingsInput input)
    {
        await base.UpdateAsync(input);
    }

    protected override async Task UpdateAsync(string name, string value)
    {
        await SettingManager.SetForCurrentTenantAsync(name, value);
    }

    protected override async Task<List<SettingValue>> GetSettingValues()
    {
        return await SettingManager.GetAllForCurrentTenantAsync();
    }
}