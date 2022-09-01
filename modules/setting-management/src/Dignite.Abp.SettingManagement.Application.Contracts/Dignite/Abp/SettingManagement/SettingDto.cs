using Dignite.Abp.FieldCustomizing.Fields;

namespace Dignite.Abp.SettingManagement
{
    public class SettingDto
    {
        public SettingDto(
            string group, 
            string name, 
            string displayName, 
            string description, 
            string value,
            string fieldProviderName,
            FieldConfigurationDictionary fieldConfiguration)
        {
            Group = group;
            Name = name;
            DisplayName = displayName;
            Description = description;
            Value = value;
            FieldProviderName = fieldProviderName;
            FieldConfiguration = fieldConfiguration;
        }

        public string Group { get;  }

        public string Name { get; }
        public string DisplayName { get; }
        public string Description { get; }

        public string Value { get;  }

        public string FieldProviderName { get; }

        public FieldConfigurationDictionary FieldConfiguration { get; }
    }
}
