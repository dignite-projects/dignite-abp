using Dignite.Abp.Data;
using Volo.Abp.Data;

namespace Dignite.Abp.DynamicForms.Table;

/// <summary>
///
/// </summary>
public class TableRow : IHasCustomFields
{
    public TableRow()
    {
        this.ExtraProperties = new ExtraPropertyDictionary();
    }

    public ExtraPropertyDictionary ExtraProperties { get; set; }
}