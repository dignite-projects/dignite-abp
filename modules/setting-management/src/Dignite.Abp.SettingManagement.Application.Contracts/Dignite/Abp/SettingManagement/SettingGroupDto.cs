using System.Collections.Generic;

namespace Dignite.Abp.SettingManagement;

public class SettingGroupDto
{
    public SettingGroupDto(string name, string displayName, string description, string icon)
    {
        Name = name;
        DisplayName = displayName;
        Description = description;
        SubGroups = new List<SettingGroupDto>();
        Settings = new List<SettingDto>();
        Icon = icon;
    }

    public string Name { get;  set; }

    public string DisplayName { get;  set; }

    public string Description { get;  set; }

    public string Icon { get; set; }

    public IList<SettingGroupDto> SubGroups { get;  set; }

    public IReadOnlyList<SettingDto> Settings { get;  set; }
}