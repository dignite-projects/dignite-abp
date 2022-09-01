

using System.Collections.Generic;

namespace Dignite.Abp.SettingManagement
{
    public class SettingNavigationDto
    {

        public SettingNavigationDto(string name, string displayName, IReadOnlyList<SettingDto> settings)
        {
            Name = name;
            DisplayName = displayName;
            Settings = settings;
        }

        public string Name { get; }

        public string DisplayName { get;  }

        public IReadOnlyList<SettingDto> Settings { get;  }
    }
}
