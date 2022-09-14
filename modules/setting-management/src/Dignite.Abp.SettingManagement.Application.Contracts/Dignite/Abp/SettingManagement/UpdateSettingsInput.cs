using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Dignite.Abp.FieldCustomizing;

namespace Dignite.Abp.SettingManagement;

public abstract class UpdateSettingsInput : CustomizableObject<SettingDto>
{
    [Required]
    public string GroupName { get; set; }

    public override IReadOnlyList<SettingDto> GetFieldDefinitions(ValidationContext validationContext)
    {
        return GetSettings(validationContext);
    }

    public abstract IReadOnlyList<SettingDto> GetSettings(ValidationContext validationContext);
}