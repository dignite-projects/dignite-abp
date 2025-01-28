using System;
using System.ComponentModel.DataAnnotations;

namespace Dignite.Abp.DynamicForms.Entry;

public class EntryConfiguration : FormConfigurationBase
{
    [Required]
    public Guid SectionId {
        get => ConfigurationDictionary.GetConfiguration<Guid>(EntryConfigurationNames.SectionId);
        set => ConfigurationDictionary.SetConfiguration(EntryConfigurationNames.SectionId, value);
    }
    public bool Multiple
    {
        get => ConfigurationDictionary.GetConfiguration(EntryConfigurationNames.Multiple, false);
        set => ConfigurationDictionary.SetConfiguration(EntryConfigurationNames.Multiple, value);
    }
    public string Placeholder
    {
        get => ConfigurationDictionary.GetConfiguration<string>(EntryConfigurationNames.Placeholder, null);
        set => ConfigurationDictionary.SetConfiguration(EntryConfigurationNames.Placeholder, value);
    }

    public EntryConfiguration(FormConfigurationDictionary fieldConfiguration)
        : base(fieldConfiguration)
    {
    }

    public EntryConfiguration() : base()
    {
    }
}