using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dignite.Abp.DynamicForms.Table;

public class TableConfiguration : FormConfigurationBase
{
    [Required]
    public List<TableColumn> TableColumns {
        get => ConfigurationDictionary.GetConfigurationOrDefault(TableConfigurationNames.TableColumns, new List<TableColumn>());
        set => ConfigurationDictionary.SetConfiguration(TableConfigurationNames.TableColumns, value);
    }

    public TableConfiguration(FormConfigurationDictionary fieldConfiguration)
        : base(fieldConfiguration)
    {
    }

    public TableConfiguration() : base()
    {
    }
}