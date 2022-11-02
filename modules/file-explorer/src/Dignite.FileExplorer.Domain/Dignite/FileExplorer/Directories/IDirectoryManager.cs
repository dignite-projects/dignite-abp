using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Dignite.FileExplorer.Directories;

public interface IDirectoryManager : IDomainService
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="containerName"></param>
    /// <param name="name"></param>
    /// <param name="parentId"></param>
    /// <returns></returns>
    Task<DirectoryDescriptor> CreateAsync(Guid userId, string containerName, string name, Guid? parentId = null);

    /// <summary>
    ///
    /// </summary>
    /// <param name="directory"></param>
    /// <param name="newParentId"></param>
    /// <param name="order"></param>
    /// <returns></returns>
    Task MoveAsync(DirectoryDescriptor directory, Guid? newParentId, int order);

    /// <summary>
    ///
    /// </summary>
    /// <param name="directory"></param>
    /// <param name="newName"></param>
    /// <returns></returns>
    Task RenameAsync(DirectoryDescriptor directory, string newName);
}