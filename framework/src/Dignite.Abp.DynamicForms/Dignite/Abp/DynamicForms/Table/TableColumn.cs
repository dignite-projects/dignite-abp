using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

namespace Dignite.Abp.DynamicForms.Table;

/// <summary>
/// 定义表格的列
/// </summary>
public class TableColumn
{
    protected TableColumn()
    {
    }

    public TableColumn(
        TableColumnCustomField fieldDefinition
        )
    {
        CustomField = fieldDefinition;
    }

    [NotNull]
    [Required]
    public string Name { get { return CustomField.Name; } }

    [NotNull]
    [Required]
    public string DisplayName { get { return CustomField.DisplayName; } }

    [NotNull]
    [Required]
    public TableColumnCustomField CustomField { get; set; }
}