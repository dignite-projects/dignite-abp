using Dignite.Abp.FieldCustomizing.Fields;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.SettingManagement;
using Volo.Abp.Settings;
using ISettingDefinitionManager = Dignite.Abp.Settings.IDigniteSettingDefinitionManager;

namespace Dignite.Abp.SettingManagement
{
    [Authorize]
    public class UserSettingsAppService : SettingsAppServiceBase, IUserSettingsAppService
    {
        public UserSettingsAppService(
            ISettingDefinitionManager settingDefinitionManager,
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
}
