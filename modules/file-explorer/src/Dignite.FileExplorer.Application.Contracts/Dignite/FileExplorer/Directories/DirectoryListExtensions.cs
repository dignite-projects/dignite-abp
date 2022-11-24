using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dignite.FileExplorer.Directories;

public static class DirectoryListExtensions
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="source"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public static DirectoryDescriptorInfoDto FindById([NotNull] this IEnumerable<DirectoryDescriptorInfoDto> source, Guid id)
    {
        DirectoryDescriptorInfoDto result = source.FirstOrDefault(ou => ou.Id == id);

        if (result == null)
        {
            foreach (var item in source)
            {
                if (item.Children != null && item.Children.Any())
                {
                    result = FindById(item.Children, id);
                    if (result != null)
                        return result;
                }
            }
        }

        return result;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static IReadOnlyList<DirectoryDescriptorInfoDto> ToLevelList([NotNull] this IReadOnlyList<DirectoryDescriptorInfoDto> source)
    {
        var result = new List<DirectoryDescriptorInfoDto>();
        foreach (var ou in source)
        {
            result.Add(ou);
            FindChildren(result, ou);
        }
        return result;
    }

    public static IReadOnlyList<DirectoryDescriptorInfoDto> BuildTree([NotNull] this IReadOnlyList<DirectoryDescriptorInfoDto> source)
    {
        if (source.Any())
        {
            var parentId = source.First().ParentId;
            var tree = new List<DirectoryDescriptorInfoDto>();
            tree.AddRange(source.Where(p => p.ParentId == parentId).ToList());
            foreach (var ou in tree)
            {
                AddChildren(ou, source);
            }
            return tree;
        }
        return source;
    }

    private static void FindChildren(List<DirectoryDescriptorInfoDto> list, DirectoryDescriptorInfoDto ou)
    {
        if (ou.Children != null && ou.Children.Any())
        {
            foreach (var c in ou.Children)
            {
                list.Add(c);
                FindChildren(list, c);
            }
        }
    }

    private static void AddChildren(DirectoryDescriptorInfoDto parent, IReadOnlyList<DirectoryDescriptorInfoDto> list)
    {
        var children = list.Where(p => p.ParentId == parent.Id).ToList();
        if (children.Any())
        {
            foreach (var ou in children)
            {
                parent.AddChild(ou);
                AddChildren(ou, list);
            }
        }
    }
}