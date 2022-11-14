namespace Dignite.Abp.FieldCustomizing;

public interface IHasCustomFields
{
    CustomFieldDictionary CustomFields { get; }
}