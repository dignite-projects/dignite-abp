using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dignite.Abp.SettingManagement.Blazor.Pages.SettingManagement
{
    internal class UpdateGlobalSettingsInputForClientProxy : UpdateGlobalSettingsInput
    {
        IReadOnlyList<SettingDto> _settings;

        public UpdateGlobalSettingsInputForClientProxy(IReadOnlyList<SettingDto> settings)
        {
            _settings = settings;
        }

        public override IReadOnlyList<SettingDto> GetSettings(ValidationContext validationContext)
        {
            return _settings;
        }
    }
}
