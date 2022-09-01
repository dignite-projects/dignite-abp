using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Dignite.Abp.SettingManagement
{
    public class UpdateGlobalSettingsInput: UpdateSettingsInput
    {
        /// <summary>
        /// 获取全局的设置项
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public override IReadOnlyList<SettingDto> GetSettings(ValidationContext validationContext)
        {
            var settingsAppService = validationContext.GetRequiredService<IGlobalSettingsAppService>();
            var settingNavigations = settingsAppService.GetAllAsync().Result;
            return settingNavigations.Items.Single(i => i.Name == NavigationName).Settings;
        }
    }
}
