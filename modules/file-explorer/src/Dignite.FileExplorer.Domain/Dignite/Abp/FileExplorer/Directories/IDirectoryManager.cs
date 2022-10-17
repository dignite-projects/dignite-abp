using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Dignite.Abp.FileExplorer.Directories;
public interface IDirectoryManager:IDomainService
{
    Task<DirectoryDescriptor> CreateAsync(string containerName, string name, Guid? parentId = null);
    Task DeleteAsync(Guid id);
    Task MoveAsync(DirectoryDescriptor directory, Guid? newParentId,int order);
    Task RenameAsync(DirectoryDescriptor directory, string newName);
}
