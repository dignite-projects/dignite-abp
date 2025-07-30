using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dignite.Publisher.Posts;
using Volo.Abp.Domain.Services;

namespace Dignite.Publisher.Categories;
public class CategoryManager : DomainService
{
    protected ICategoryRepository CategoryRepository { get; }

    protected IPostRepository PostRepository { get; }
    public CategoryManager(ICategoryRepository categoryRepository, IPostRepository postRepository)
    {
        CategoryRepository = categoryRepository;
        PostRepository = postRepository;
    }

    public virtual async Task<Category> CreateAsync(
        string? local, Guid? parentId, string displayName, string name, string? description, bool isActive, List<string> postTypes, int order
        )
    {
        await CheckNameExistenceAsync(parentId, name);

        // Check if the target parent exists
        if (parentId.HasValue && await CategoryRepository.FindAsync(parentId.Value, false) == null)
        {
            throw new CategoryNotFoundException(parentId.Value);
        }

        var category = new Category(GuidGenerator.Create(), local, parentId, displayName, name, description, isActive, postTypes, order, CurrentTenant.Id);
        return category;
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        var category = await CategoryRepository.GetAsync(id, false);
        var allCategories = await GetTreeListAsync(category.Local);
        var children = BuildTree(allCategories, id);
        foreach (var child in children)
        {
            // Delete all child categories recursively
            await CategoryRepository.DeleteAsync(child.Id);
        }

        // Delete the category itself
        await CategoryRepository.DeleteAsync(id);

        // TODO: How should posts under this category be handled?
    }

    /// <summary>
    /// Retrieves a list of categories in a tree structure based on the specified local identifier.
    /// </summary>
    /// <param name="local"></param>
    /// <returns></returns>
    public virtual async Task<List<Category>> GetTreeListAsync(string? local)
    {
        var allCategories = await CategoryRepository.GetListAsync(local);

        return BuildTree(allCategories);
    }

    /// <summary>
    /// Moves a category to a new parent category or changes its order within the same parent category.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="targetParentId"></param>
    /// <param name="insertPosition"></param>
    /// <returns></returns>
    /// <exception cref="CategoryNotFoundException"></exception>
    /// <exception cref="CategoryLocalMismatchException"></exception>
    /// <exception cref="CategoryCannotMoveToChildException"></exception>
    public virtual async Task MoveAsync(Category source, Guid? targetParentId, int insertPosition)
    {
        if (source.ParentId == targetParentId && source.Order == insertPosition)
        {
            return;
        }

        // Check if the target parent exists
        if(targetParentId.HasValue)
        {
            var targetParent = await CategoryRepository.FindAsync(targetParentId.Value, false);
            if (targetParent == null)
            {
                throw new CategoryNotFoundException(targetParentId.Value);
            }
            else
            {
                if (source.Local != targetParent.Local)
                {
                    throw new CategoryLocalMismatchException();
                }
            }
        }
        // Check if the name already exists in the target parent
        await CheckNameExistenceAsync(targetParentId, source.Name);

        var allCategories = await CategoryRepository.GetListAsync(source.Local);
        var sourceChildrenTree = BuildTree(allCategories, source.Id);

        //
        if (sourceChildrenTree.Any())
        {
            var sourceChildren = FlattenTree(sourceChildrenTree);
            // Check if the target parent is a child of the source category
            if (sourceChildren.Any(c => c.Id == targetParentId))
            {
                throw new CategoryCannotMoveToChildException();
            }
        }

        var sourceParents = GetAllParents(allCategories, source.Id);
        var targetAndParents = GetAllParents(allCategories, targetParentId ?? Guid.Empty);
        if (targetParentId.HasValue)
        {
            targetAndParents.Add(allCategories.First(c => c.Id == targetParentId.Value));
        }
        var posts = await PostRepository.GetListByCategoryIdAsync(source.Id);
        // If the source category has posts, we need to update the post categories
        if (posts.Any())
        {
            var sourceOnlyParentCategoryIds = sourceParents.Select(c => c.Id).Except(targetAndParents.Select(c => c.Id));
            var targetOnlyParentCategoryIds = targetAndParents.Select(c => c.Id).Except(sourceParents.Select(c => c.Id));
            foreach (var post in posts)
            {
                var postCategoryIds = post.PostCategories.Select(pc => pc.CategoryId).ToList();

                // Remove the source category from the post categories
                postCategoryIds.RemoveAll(x => sourceOnlyParentCategoryIds.Contains(x));

                // Add the target parent category to the post categories
                postCategoryIds.AddRange(targetOnlyParentCategoryIds);

                // 
                post.SetCategoies(postCategoryIds);
                await PostRepository.UpdateAsync(post);
            }
        }


        // Adjust the sort values of the child nodes under the target parent node.
        foreach (var item in allCategories.Where(c => c.ParentId == targetParentId))
        {
            if (item.Order >= insertPosition)
            {
                // If the current item's order is greater than or equal to the insert position, increment its order
                item.Order++;
                await CategoryRepository.UpdateAsync(item);
            }
        }

        // Update the category's parent and order
        source.ParentId = targetParentId;
        source.Order = insertPosition;
    }

    public virtual async Task CheckNameExistenceAsync(Guid? parentId, string name)
    {
        if (await CategoryRepository.NameExistsAsync(parentId, name))
        {
            throw new CategoryNameAlreadyExistException(parentId, name);
        }
    }

    public virtual async Task CheckExistenceAsync(string? local, IEnumerable<Guid> ids)
    {
        var allCategories = await CategoryRepository.GetListAsync(local);
        foreach (var id in ids)
        {
            if (!allCategories.Any(c => c.Id == id))
            {
                throw new CategoryNotFoundException(id);
            }
        }
    }

    /// <summary>
    /// Builds a tree structure from a flat list of categories, where each category can have children based on its ParentId.
    /// </summary>
    /// <param name="all"></param>
    /// <param name="parentId"></param>
    /// <returns></returns>
    protected virtual List<Category> BuildTree(List<Category> all, Guid? parentId = null)
    {
        return all.Where(c => c.ParentId == parentId)
                  .Select(c => {
                      c.Children = BuildTree(all, c.Id);
                      return c;
                  }).ToList();
    }

    /// <summary>
    /// Flattens a tree structure into a flat list of categories.
    /// </summary>
    /// <param name="tree"></param>
    /// <returns></returns>
    protected virtual List<Category> FlattenTree(List<Category> tree)
    {
        var list = new List<Category>();

        foreach (var node in tree)
        {
            list.Add(node); // 添加当前节点

            if (node.Children != null && node.Children.Any())
            {
                list.AddRange(FlattenTree(node.Children)); // 递归添加子节点
            }
        }

        return list;
    }

    /// <summary>
    /// Retrieves all parent categories for a given category ID from a flat list of categories.
    /// </summary>
    /// <param name="all"></param>
    /// <param name="categoryId"></param>
    /// <returns></returns>
    protected virtual List<Category> GetAllParents(List<Category> all, Guid categoryId)
    {
        var parents = new List<Category>();
        var current = all.FirstOrDefault(c => c.Id == categoryId);

        while (current != null && current.ParentId.HasValue)
        {
            var parent = all.FirstOrDefault(c => c.Id == current.ParentId.Value);
            if (parent != null)
            {
                parents.Add(parent);
                current = parent;
            }
            else
            {
                break;
            }
        }

        return parents;
    }
}
