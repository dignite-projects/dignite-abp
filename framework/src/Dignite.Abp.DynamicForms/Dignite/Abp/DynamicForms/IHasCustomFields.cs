namespace Dignite.Abp.DynamicForms;

/// <summary>
/// used to persist dynamic form data
/// </summary>
public interface IHasCustomFields
{
    /// <summary>
    /// The data of the dynamic form is stored in the dictionary
    /// </summary>
    CustomFieldDictionary CustomFields { get; }
}