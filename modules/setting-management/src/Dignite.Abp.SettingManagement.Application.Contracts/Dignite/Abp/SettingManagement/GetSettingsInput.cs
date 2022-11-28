using System.ComponentModel.DataAnnotations;

namespace Dignite.Abp.SettingManagement;
public class GetSettingsInput
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
}
