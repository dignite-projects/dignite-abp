using System.Collections.Generic;
using System.Threading.Tasks;
using Dignite.Abp.FieldCustomizing.Fields;
using Dignite.Abp.SettingsGrouping;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.SettingManagement;
using Volo.Abp.Settings;

namespace Dignite.Abp.SettingManagement;

[Authorize(SettingManagementPermissions.Global)]
public class GlobalSettingsAppService : SettingsAppServiceBase, IGlobalSettingsAppService
{
    public GlobalSettingsAppService(
        ISettingDefinitionGroupManager settingDefinitionManager,
        ISettingManager settingManager,
        IEnumerable<IFieldProvider> controlProviders)
        : base(settingDefinitionManager, settingManager, controlProviders)
    {
    }

    protected override async Task UpdateAsync(string name, string value)
    {
        await SettingManager.SetGlobalAsync(name, value);
    }

    protected override async Task<List<SettingValue>> GetSettingValues()
    {
        return await SettingManager.GetAllGlobalAsync();
    }

    public async Task UpdateAsync(UpdateGlobalSettingsInput input)
    {
        await base.UpdateAsync(input);
    }
}