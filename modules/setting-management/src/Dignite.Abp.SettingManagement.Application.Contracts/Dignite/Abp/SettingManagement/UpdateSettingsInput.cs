using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Dignite.Abp.DynamicForms;

namespace Dignite.Abp.SettingManagement;

public abstract class UpdateSettingsInput : CustomizableObject<SettingDto>
{
    /// <summary>
    /// 
    /// </summary>
    [Required]
    public string GroupName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string SubGroupName { get; set; }

    public override IReadOnlyList<SettingDto> GetFieldDefinitions(ValidationContext validationContext)
    {
        return GetSettings(validationContext);
    }

    public abstract IReadOnlyList<SettingDto> GetSettings(ValidationContext validationContext);
}