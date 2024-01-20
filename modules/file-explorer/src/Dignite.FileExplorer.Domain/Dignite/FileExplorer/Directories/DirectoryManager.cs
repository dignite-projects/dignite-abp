using System;
using System.Linq;
using System.Threading.Tasks;
using Dignite.Abp.BlobStoring;
using Volo.Abp.Domain.Services;

namespace Dignite.FileExplorer.Directories;

public class DirectoryManager : DomainService
{
    protected ContainerNameValidator ContainerNameValidator { get; }
    protected IDirectoryDescriptorRepository DirectoryDescriptorRepository { get; }

    public DirectoryManager(IDirectoryDescriptorRepository directoryDescriptorRepository, ContainerNameValidator containerNameValidator)
    {
        DirectoryDescriptorRepository = directoryDescriptorRepository;
        ContainerNameValidator = containerNameValidator;
    }

    public virtual async Task<DirectoryDescriptor> CreateAsync(Guid userId, string containerName, string name, Guid? parentId = null)
    {
        ContainerNameValidator.Validate(containerName);

        //
        if (await DirectoryDescriptorRepository.NameExistsAsync(userId, containerName, name, parentId))
        {
            throw new DirectoryAlreadyExistException(name);
        }

        //
        var order = await DirectoryDescriptorRepository.GetMaxOrderAsync(userId, containerName, parentId);
        var directory = new DirectoryDescriptor(
            GuidGenerator.Create(),
            containerName, name, parentId,
            order + 1,
            CurrentTenant.Id
            );
        return await DirectoryDescriptorRepository.InsertAsync(directory);
    }

    public virtual async Task<DirectoryDescriptor> MoveAsync(DirectoryDescriptor directory, Guid? parentId,int order)
    {
        if (parentId.HasValue)
        {
            var parent = await DirectoryDescriptorRepository.GetAsync(parentId.Value);
            if (!parent.ContainerName.Equals(directory.ContainerName, StringComparison.CurrentCultureIgnoreCase))
            {
                throw new DirectoryInvalidMoveException();
            }
        }

        var children = await DirectoryDescriptorRepository.GetListAsync(directory.CreatorId.Value, directory.ContainerName, parentId);
        foreach (var item in children.Where(d=>d.Order>=order && d.Id!=directory.Id))
        {
            item.Order=item.Order+1;
            await DirectoryDescriptorRepository.UpdateAsync(item);
        }

        directory.ParentId = parentId;
        directory.Order = order;
        return await DirectoryDescriptorRepository.UpdateAsync(directory);
    }

    public virtual async Task<DirectoryDescriptor> RenameAsync(DirectoryDescriptor directory, string newName)
    {
        //
        if (!directory.Name.Equals(newName, StringComparison.CurrentCultureIgnoreCase))
        {
            if (await DirectoryDescriptorRepository.NameExistsAsync(directory.CreatorId.Value, directory.ContainerName, newName, directory.ParentId))
            {
                throw new DirectoryAlreadyExistException(newName);
            }
        }

        directory.Name = newName;
        return await DirectoryDescriptorRepository.UpdateAsync(directory);
    }
}