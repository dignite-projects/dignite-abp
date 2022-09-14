using System.Collections.Generic;

namespace Dignite.Abp.FieldCustomizing.Fields.Select;

public class SelectConfiguration : FieldConfigurationBase
{
    public string NullText {
        get => ConfigurationDictionary.GetConfigurationOrDefault<string>(SelectConfigurationNames.NullText, null);
        set => ConfigurationDictionary.SetConfiguration(SelectConfigurationNames.NullText, value);
    }

    public List<SelectListItem> Options {
        get => ConfigurationDictionary.GetConfigurationOrDefault(SelectConfigurationNames.Options, new List<SelectListItem>());
        set => ConfigurationDictionary.SetConfiguration(SelectConfigurationNames.Options, value);
    }

    public bool Multiple {
        get => ConfigurationDictionary.GetConfigurationOrDefault(SelectConfigurationNames.Multiple, false);
        set => ConfigurationDictionary.SetConfiguration(SelectConfigurationNames.Multiple, value);
    }

    public int? Size {
        get => ConfigurationDictionary.GetConfigurationOrDefault<int?>(SelectConfigurationNames.Size);
        set => ConfigurationDictionary.SetConfiguration(SelectConfigurationNames.Size, value);
    }

    public SelectConfiguration(FieldConfigurationDictionary fieldConfiguration)
        : base(fieldConfiguration)
    {
    }

    public SelectConfiguration() : base()
    {
    }
}