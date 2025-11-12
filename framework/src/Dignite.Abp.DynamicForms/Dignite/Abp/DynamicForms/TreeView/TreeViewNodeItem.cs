using System;
using System.Collections.Generic;

namespace Dignite.Abp.DynamicForms.TreeView;

[Serializable]
public class TreeViewNodeItem
{
    public TreeViewNodeItem()
    {
    }

    public TreeViewNodeItem(string text, string value, bool selected)
    {
        Text = text;
        Value = value;
        Selected = selected;
    }

    public string Text { get; set; }

    public string Value { get; set; }

    public bool Selected { get; set; }

    public IList<TreeViewNodeItem> Children { get; set; } = new List<TreeViewNodeItem>();
}