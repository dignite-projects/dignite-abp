using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Dignite.Publisher.Categories;
public class CategoryManager : DomainService
{
    protected ICategoryRepository CategoryRepository { get; }
    public CategoryManager(ICategoryRepository categoryRepository)
    {
        CategoryRepository = categoryRepository;
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

    public virtual async Task<List<Category>> GetTreeListAsync(string? local)
    {
        var allCategories = await CategoryRepository.GetListAsync(local);

        return BuildTree(allCategories);
    }
    public virtual async Task MoveAsync(Category source, Guid? targetParentId, int insertPosition)
    {
        if (source.ParentId == targetParentId && source.Order == insertPosition)
        {
            return;
        }
        // Check if the target parent exists
        if (targetParentId.HasValue && await CategoryRepository.FindAsync(targetParentId.Value, false) == null)
        {
            throw new CategoryNotFoundException(targetParentId.Value);
        }
        // Check if the name already exists in the target parent
        await CheckNameExistenceAsync(targetParentId, source.Name);

        var allCategories = await CategoryRepository.GetListAsync(source.Local);
        var targetChildren = allCategories.Where(c => c.ParentId == targetParentId).ToList();
        foreach (var item in targetChildren)
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

    protected virtual List<Category> BuildTree(List<Category> all, Guid? parentId = null)
    {
        return all.Where(c => c.ParentId == parentId)
                  .Select(c => {
                      c.Children = BuildTree(all, c.Id);
                      return c;
                  }).ToList();
    }
}
