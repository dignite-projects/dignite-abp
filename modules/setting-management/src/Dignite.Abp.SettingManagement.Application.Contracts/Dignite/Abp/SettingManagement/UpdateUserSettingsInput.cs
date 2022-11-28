using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;

namespace Dignite.Abp.SettingManagement;

public class UpdateUserSettingsInput : UpdateSettingsInput
{
    /// <summary>
    /// get global settings
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public override IReadOnlyList<SettingDto> GetSettings(ValidationContext validationContext)
    {
        var settingsAppService = validationContext.GetRequiredService<IUserSettingsAppService>();
        var settings = settingsAppService.GetListAsync(
            new GetSettingsInput
            {
                GroupName = GroupName,
                SubGroupName = SubGroupName,
            }
            ).Result;
        return settings.Items;
    }
}