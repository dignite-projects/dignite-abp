namespace Dignite.Abp.DynamicForms;

/// <summary>
/// Object with custom fields
/// </summary>
public interface IHasCustomFields
{
    /// <summary>
    /// The data of the dynamic form is stored in the dictionary
    /// </summary>
    CustomFieldDictionary CustomFields { get; }
}