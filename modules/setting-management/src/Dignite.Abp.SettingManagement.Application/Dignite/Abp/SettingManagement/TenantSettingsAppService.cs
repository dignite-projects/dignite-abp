using Dignite.Abp.FieldCustomizing.Fields;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.SettingManagement;
using Volo.Abp.Settings;
using ISettingDefinitionManager = Dignite.Abp.Settings.IDigniteSettingDefinitionManager;

namespace Dignite.Abp.SettingManagement
{
    [Authorize(SettingManagementPermissions.Tenant)]
    public class TenantSettingsAppService : SettingsAppServiceBase, ITenantSettingsAppService
    {
        public TenantSettingsAppService(
            ISettingDefinitionManager settingDefinitionManager,
            ISettingManager settingManager,
            IEnumerable<IFieldProvider> controlProviders)
            : base(settingDefinitionManager, settingManager, controlProviders)
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
}
