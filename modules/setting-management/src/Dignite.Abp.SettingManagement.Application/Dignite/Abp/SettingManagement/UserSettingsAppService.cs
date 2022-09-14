using System.Collections.Generic;
using System.Threading.Tasks;
using Dignite.Abp.FieldCustomizing.Fields;
using Dignite.Abp.SettingsGrouping;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.SettingManagement;
using Volo.Abp.Settings;

namespace Dignite.Abp.SettingManagement;

[Authorize]
public class UserSettingsAppService : SettingsAppServiceBase, IUserSettingsAppService
{
    public UserSettingsAppService(
        ISettingDefinitionGroupManager settingDefinitionManager,
        ISettingManager settingManager,
        IEnumerable<IFieldProvider> controlProviders)
        : base(settingDefinitionManager, settingManager, controlProviders)
    {
    }

    public async Task UpdateAsync(UpdateUserSettingsInput input)
    {
        await base.UpdateAsync(input);
    }

    protected override async Task UpdateAsync(string name, string value)
    {
        await SettingManager.SetForCurrentUserAsync(name, value);
    }

    protected override async Task<List<SettingValue>> GetSettingValues()
    {
        return await SettingManager.GetAllForCurrentUserAsync();
    }
}