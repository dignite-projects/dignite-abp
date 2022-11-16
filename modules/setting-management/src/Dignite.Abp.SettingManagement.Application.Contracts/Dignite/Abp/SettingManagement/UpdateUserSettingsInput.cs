using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Dignite.Abp.SettingManagement;

public class UpdateUserSettingsInput : UpdateSettingsInput
{
    public override IReadOnlyList<SettingDto> GetSettings(ValidationContext validationContext)
    {
        var settingsAppService = validationContext.GetRequiredService<IUserSettingsAppService>();
        var settingNavigations = settingsAppService.GetAllAsync().Result;
        return settingNavigations.Items.Single(i => i.Name == ProviderName).Settings;
    }
}