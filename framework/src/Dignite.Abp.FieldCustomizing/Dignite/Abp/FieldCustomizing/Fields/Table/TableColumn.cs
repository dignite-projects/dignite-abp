using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

namespace Dignite.Abp.FieldCustomizing.Fields.Table;

/// <summary>
/// 定义表格的列
/// </summary>
public class TableColumn
{
    protected TableColumn()
    {
    }

    public TableColumn(
        TableColumnFieldDefinition fieldDefinition
        )
    {
        FieldDefinition = fieldDefinition;
    }

    [NotNull]
    [Required]
    public string Name { get { return FieldDefinition.Name; } }

    [NotNull]
    [Required]
    public string DisplayName { get { return FieldDefinition.DisplayName; } }

    [NotNull]
    [Required]
    public TableColumnFieldDefinition FieldDefinition { get; set; }
}