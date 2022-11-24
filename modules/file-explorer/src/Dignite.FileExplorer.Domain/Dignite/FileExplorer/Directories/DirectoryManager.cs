using System;
using System.Threading.Tasks;
using Dignite.Abp.BlobStoring;
using Volo.Abp.Domain.Services;

namespace Dignite.FileExplorer.Directories;

public class DirectoryManager : DomainService, IDirectoryManager, IDomainService
{
    protected ContainerNameValidator ContainerNameValidator { get; }
    protected IDirectoryDescriptorRepository DirectoryDescriptorRepository { get; }

    public DirectoryManager(IDirectoryDescriptorRepository directoryDescriptorRepository, ContainerNameValidator containerNameValidator)
    {
        DirectoryDescriptorRepository = directoryDescriptorRepository;
        ContainerNameValidator = containerNameValidator;
    }

    public async Task<DirectoryDescriptor> CreateAsync(Guid userId, string containerName, string name, Guid? parentId = null)
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
        await DirectoryDescriptorRepository.InsertAsync(directory);
        return directory;
    }

    public async Task MoveAsync(DirectoryDescriptor directory, Guid? parentId)
    {
        var containerName = directory.ContainerName;
        if (parentId.HasValue)
        {
            var parent = await DirectoryDescriptorRepository.GetAsync(parentId.Value);
            if (!parent.ContainerName.Equals(directory.ContainerName, StringComparison.CurrentCultureIgnoreCase))
            {
                throw new DirectoryInvalidMoveException();
            }
            containerName = parent.ContainerName;
        }

        //
        if (await DirectoryDescriptorRepository.NameExistsAsync(directory.CreatorId.Value, containerName, directory.Name, parentId, directory.Id))
        {
            throw new DirectoryAlreadyExistException(directory.Name);
        }

        directory.ParentId = parentId;
        await DirectoryDescriptorRepository.UpdateAsync(directory);
    }

    public async Task RenameAsync(DirectoryDescriptor directory, string newName)
    {
        //
        if (await DirectoryDescriptorRepository.NameExistsAsync(directory.CreatorId.Value, directory.ContainerName, newName, directory.ParentId, directory.Id))
        {
            throw new DirectoryAlreadyExistException(newName);
        }

        directory.Name = newName;
        await DirectoryDescriptorRepository.UpdateAsync(directory);
    }
}