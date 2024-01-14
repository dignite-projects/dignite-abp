using JetBrains.Annotations;

namespace Dignite.Abp.DynamicForms;

/// <summary>
/// Form fields contain field information and configuration information for the control.
/// </summary>
public class FormField
{
    protected FormField() { }

    public FormField(string name, string displayName, string description, string formControlName, FormConfigurationDictionary formConfiguration, bool? required=null, object value=null)
    {
        Name = name;
        DisplayName = displayName;
        Description = description;
        FormControlName = formControlName;
        FormConfiguration = formConfiguration;
        Required = required;
        Value = value;
    }

    /// <summary>
    /// Field name
    /// </summary>
    /// <remarks>
    /// To ensure that it is unique within the scope of the field set
    /// </remarks>
    [NotNull]
    public virtual string Name { get; set; }

    /// <summary>
    /// Field display name
    /// </summary>
    [NotNull]
    public virtual string DisplayName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public virtual string Description { get; set; }

    /// <summary>
    /// Form control used by field
    /// </summary>
    [NotNull]
    public virtual string FormControlName { get; set; }

    /// <summary>
    /// Form configuration of field
    /// </summary>
    [NotNull]
    public virtual FormConfigurationDictionary FormConfiguration { get; set; }

    public bool? Required { get; set; }

    /// <summary>
    /// Field value
    /// </summary>
    public object Value { get; set; }
}