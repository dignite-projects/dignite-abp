using System.Collections.Generic;
using Dignite.Abp.DynamicForms;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Localization;

namespace Dignite.Abp.DataDictionary;

public class DataDictionaryDefinition
{
    /// <summary>
    /// Unique name of the data dictionary.
    /// </summary>
    [NotNull]
    public string Name { get; }

    /// <summary>
    /// Default value of the data dictionary.
    /// </summary>
    [NotNull]
    public FormConfigurationDictionary DefaultValue { get; set; }

    [NotNull]
    public ILocalizableString DisplayName {
        get => _displayName;
        set => _displayName = Check.NotNull(value, nameof(value));
    }
    private ILocalizableString _displayName = default!;

    public ILocalizableString? Description { get; set; }

    /// <summary>
    /// A list of allowed providers to get/set value of this dataDictionary.
    /// An empty list indicates that all providers are allowed.
    /// </summary>
    public List<string> Providers { get; }

    /// <summary>
    /// Is this dataDictionary inherited from parent scopes.
    /// Default: True.
    /// </summary>
    public bool IsInherited { get; set; }

    public DataDictionaryDefinition(
        string name,
        FormConfigurationDictionary defaultValue,
        ILocalizableString? displayName = null,
        ILocalizableString? description = null,
        bool isInherited = true)
    {
        Name = name;
        DefaultValue = defaultValue;
        DisplayName = displayName ?? new FixedLocalizableString(name);
        Description = description;
        IsInherited = isInherited;

        Providers = new List<string>();
    }

    /// <summary>
    /// Adds one or more providers to the <see cref="Providers"/> list.
    /// This is a shortcut for nested calls on this object.
    /// </summary>
    public virtual DataDictionaryDefinition WithProviders(params string[] providers)
    {
        if (!providers.IsNullOrEmpty())
        {
            Providers.AddIfNotContains(providers);
        }

        return this;
    }
}
