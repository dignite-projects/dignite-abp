﻿using System;
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

    public async Task<DirectoryDescriptor> CreateAsync(string containerName, string name, Guid? parentId = null)
    {
        //
        DirectoryNameValidator.CheckDirectoryName(name);
        ContainerNameValidator.CheckDirectoryName(containerName);

        //
        if (await DirectoryDescriptorRepository.NameExistsAsync(containerName, name, parentId))
        {
            throw new DirectoryAlreadyExistException(name);
        }



        //
        var order = await DirectoryDescriptorRepository.GetChildrenCountAsync(containerName, parentId);
        var directory = new DirectoryDescriptor(
            GuidGenerator.Create(),
            containerName, name, parentId,
            order+1,
            CurrentTenant.Id
            );
        await DirectoryDescriptorRepository.InsertAsync(directory);
        return directory;
    }

    public async Task DeleteAsync(Guid id)
    {
        var directory = await DirectoryDescriptorRepository.GetAsync(id);
        await DirectoryDescriptorRepository.DeleteAsync(directory);
    }

    public async Task MoveAsync(DirectoryDescriptor directory, Guid? newParentId, int order)
    {
        if (newParentId.HasValue)
        {
            var newParent = await DirectoryDescriptorRepository.GetAsync(newParentId.Value);
            if (!newParent.ContainerName.Equals(directory.ContainerName, StringComparison.CurrentCultureIgnoreCase))
            {
                throw new InvalidMoveException();
            }
        }

        directory.ParentId = newParentId;
        directory.Order = order;
        await DirectoryDescriptorRepository.UpdateAsync(directory);
    }

    public async Task RenameAsync(DirectoryDescriptor directory, string newName)
    {
        //
        DirectoryNameValidator.CheckDirectoryName(newName);

        //
        if (await DirectoryDescriptorRepository.NameExistsAsync(directory.ContainerName, newName, directory.ParentId,directory.Id))
        {
            throw new DirectoryAlreadyExistException(newName);
        }

        directory.Name = newName;
        await DirectoryDescriptorRepository.UpdateAsync(directory);
    }
}
