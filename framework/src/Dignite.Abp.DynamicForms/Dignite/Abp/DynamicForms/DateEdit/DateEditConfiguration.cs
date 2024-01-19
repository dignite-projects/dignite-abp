using System;
using System.ComponentModel.DataAnnotations;

namespace Dignite.Abp.DynamicForms.DateEdit;

public class DateEditConfiguration : FormConfigurationBase
{
    [Required]
    public DateInputMode InputMode {
        get => ConfigurationDictionary.GetConfiguration(DateEditConfigurationNames.InputMode, DateInputMode.Date);
        set => ConfigurationDictionary.SetConfiguration(DateEditConfigurationNames.InputMode, value);
    }

    public DateTime? Max {
        get => ConfigurationDictionary.GetConfiguration<DateTime?>(DateEditConfigurationNames.Max);
        set => ConfigurationDictionary.SetConfiguration(DateEditConfigurationNames.Max, value);
    }

    public DateTime? Min {
        get => ConfigurationDictionary.GetConfiguration<DateTime?>(DateEditConfigurationNames.Min);
        set => ConfigurationDictionary.SetConfiguration(DateEditConfigurationNames.Min, value);
    }

    public DateEditConfiguration(FormConfigurationDictionary fieldConfiguration)
        : base(fieldConfiguration)
    {
    }

    public DateEditConfiguration() : base()
    {
    }
}