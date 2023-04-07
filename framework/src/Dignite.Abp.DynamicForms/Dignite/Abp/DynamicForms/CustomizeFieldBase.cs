using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;
using Volo.Abp.Validation;

namespace Dignite.Abp.DynamicForms;
public abstract class CustomizeFieldBase: ICustomizeFieldInfo
{
    public CustomizeFieldBase()
    {
        this.FormConfiguration = new FormConfigurationDictionary();
    }

    public CustomizeFieldBase(
        [NotNull] string name,
        [NotNull] string displayName,
        [NotNull] string fieldName,
        [NotNull] string defaultValue,
        [NotNull] FormConfigurationDictionary configuration
        )
    {
        Name = name;
        DisplayName = displayName;
        FormName = fieldName;
        DefaultValue = defaultValue;
        FormConfiguration = configuration;
    }

    [NotNull]
    [Required]
    [DynamicMaxLength(typeof(CustomizeFieldInfoConsts), nameof(CustomizeFieldInfoConsts.MaxNameLength))]
    [RegularExpression(CustomizeFieldInfoConsts.NameRegularExpression)]
    public string Name { get; set; }

    [NotNull]
    [Required]
    [DynamicMaxLength(typeof(CustomizeFieldInfoConsts), nameof(CustomizeFieldInfoConsts.MaxDisplayNameLength))]
    public string DisplayName { get; set; }

    /// <summary>
    /// The form to be used to <see cref="IForm.Name"/>
    /// </summary>
    [NotNull]
    [Required]
    [DynamicMaxLength(typeof(CustomizeFieldInfoConsts), nameof(CustomizeFieldInfoConsts.MaxFormNameLength))]
    public string FormName { get; set; }

    /// <summary>
    /// Default value of the field.
    /// </summary>
    [CanBeNull]
    public string DefaultValue { get; set; }

    [NotNull]
    [Required]
    public FormConfigurationDictionary FormConfiguration { get; set; }
}
