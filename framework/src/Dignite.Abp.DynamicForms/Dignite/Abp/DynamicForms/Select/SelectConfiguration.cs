using System.Collections.Generic;
using Dignite.Abp.DynamicForms;

namespace Dignite.Abp.DynamicForms.Select;

public class SelectConfiguration : FormConfigurationBase
{
    public string NullText {
        get => ConfigurationDictionary.GetConfiguration<string>(SelectConfigurationNames.NullText, null);
        set => ConfigurationDictionary.SetConfiguration(SelectConfigurationNames.NullText, value);
    }

    public List<SelectListItem> Options {
        get => ConfigurationDictionary.GetConfiguration<List<SelectListItem>>(SelectConfigurationNames.Options, new List<SelectListItem>());
        set => ConfigurationDictionary.SetConfiguration(SelectConfigurationNames.Options, value);
    }

    public bool Multiple {
        get => ConfigurationDictionary.GetConfiguration(SelectConfigurationNames.Multiple, false);
        set => ConfigurationDictionary.SetConfiguration(SelectConfigurationNames.Multiple, value);
    }

    public int? Size {
        get => ConfigurationDictionary.GetConfiguration<int?>(SelectConfigurationNames.Size);
        set => ConfigurationDictionary.SetConfiguration(SelectConfigurationNames.Size, value);
    }

    public SelectConfiguration(FormConfigurationDictionary fieldConfiguration)
        : base(fieldConfiguration)
    {
    }

    public SelectConfiguration() : base()
    {
    }
}