using System;
using System.ComponentModel.DataAnnotations;

namespace Dignite.Abp.DynamicForms.DateEdit;

public class DateEditConfiguration : FormConfigurationBase
{
    [Required]
    public DateInputMode InputMode {
        get => ConfigurationDictionary.GetConfigurationOrDefault(DateEditConfigurationNames.InputMode, DateInputMode.Date);
        set => ConfigurationDictionary.SetConfiguration(DateEditConfigurationNames.InputMode, value);
    }

    public DateTimeOffset? Max {
        get => ConfigurationDictionary.GetConfigurationOrDefault<DateTimeOffset?>(DateEditConfigurationNames.Max);
        set => ConfigurationDictionary.SetConfiguration(DateEditConfigurationNames.Max, value);
    }

    public DateTimeOffset? Min {
        get => ConfigurationDictionary.GetConfigurationOrDefault<DateTimeOffset?>(DateEditConfigurationNames.Min);
        set => ConfigurationDictionary.SetConfiguration(DateEditConfigurationNames.Min, value);
    }

    public DateEditConfiguration(FormConfigurationDictionary fieldConfiguration)
        : base(fieldConfiguration)
    {
    }

    public DateEditConfiguration()
    {
    }
}