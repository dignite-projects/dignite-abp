using JetBrains.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Dignite.Abp.FieldCustomizing.Fields.Table
{
    /// <summary>
    /// 定义表格的列
    /// </summary>
    public class TableColumn
    {
        protected TableColumn()
        {
        }

        public TableColumn(
        [NotNull] BasicCustomizeFieldDefinition fieldDefinition)
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
        public BasicCustomizeFieldDefinition FieldDefinition { get;  set; }
    }
}
