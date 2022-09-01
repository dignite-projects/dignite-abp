using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dignite.Abp.FieldCustomizing.Fields.Table
{
    public class TableConfiguration:FieldConfigurationBase
    {
        [Required]
        public List<TableColumn> TableColumns
        {
            get => ConfigurationDictionary.GetConfigurationOrDefault(TableConfigurationNames.TableColumns, new List<TableColumn>());
            set => ConfigurationDictionary.SetConfiguration(TableConfigurationNames.TableColumns, value);
        }


        public TableConfiguration(FieldConfigurationDictionary fieldConfiguration)
            :base(fieldConfiguration)
        {
        }
        public TableConfiguration() : base()
        {
        }
    }
}
