using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Dignite.Abp.DynamicForms.TreeView;

public static class TreeViewNodeItemExtensions
{
    /// <summary>
    /// 判断树形结构中是否包含指定的 Value。
    /// </summary>
    /// <param name="nodes">树节点集合（根节点或任意层级）。</param>
    /// <param name="value">要查找的值。</param>
    /// <returns>如果存在则返回 true，否则返回 false。</returns>
    public static bool ContainsValue(this IEnumerable<TreeViewNodeItem> nodes, string value)
    {
        if (nodes == null || string.IsNullOrEmpty(value))
            return false;

        foreach (var node in nodes)
        {
            if (node.Value == value)
                return true;

            if (node.Children != null && node.Children.Count > 0)
            {
                if (node.Children.ContainsValue(value))
                    return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 判断单个节点及其子节点是否包含指定的 Value。
    /// </summary>
    /// <param name="node">树节点。</param>
    /// <param name="value">要查找的值。</param>
    /// <returns>如果存在则返回 true，否则返回 false。</returns>
    public static bool ContainsValue(this TreeViewNodeItem node, string value)
    {
        if (node == null || string.IsNullOrEmpty(value))
            return false;

        if (node.Value == value)
            return true;

        return node.Children != null && node.Children.ContainsValue(value);
    }


    public static TreeViewNodeItem? Find(this IEnumerable<TreeViewNodeItem> nodes, string value)
    {
        foreach (var node in nodes)
        {
            if (node.Value == value)
            {
                return node;
            }

            var subItem = Find(node.Children, value);
            if (subItem != null)
            {
                return subItem;
            }
        }

        return null;
    }
}