using System.Collections.Generic;

namespace Dignite.Abp.DynamicForms.TreeView;

public class TreeViewConfiguration : FormConfigurationBase
{
    public bool Multiple {
        get => ConfigurationDictionary.GetConfiguration(TreeViewConfigurationNames.Multiple, false);
        set => ConfigurationDictionary.SetConfiguration(TreeViewConfigurationNames.Multiple, value);
    }
    public List<TreeViewNodeItem> Nodes {
        get => ConfigurationDictionary.GetConfiguration(TreeViewConfigurationNames.Nodes, new List<TreeViewNodeItem>());
        set => ConfigurationDictionary.SetConfiguration(TreeViewConfigurationNames.Nodes, value);
    }

    public TreeViewConfiguration(FormConfigurationDictionary fieldConfiguration)
        : base(fieldConfiguration)
    {
    }

    public TreeViewConfiguration() : base()
    {
    }
}