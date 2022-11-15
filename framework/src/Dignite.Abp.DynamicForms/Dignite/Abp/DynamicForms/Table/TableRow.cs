namespace Dignite.Abp.DynamicForms.Table;

/// <summary>
///
/// </summary>
public class TableRow : IHasCustomFields
{
    public TableRow()
    {
        this.CustomFields = new CustomFieldDictionary();
    }

    public CustomFieldDictionary CustomFields { get; set; }
}