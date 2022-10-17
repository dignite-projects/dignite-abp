using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Dignite.Abp.FileExplorer.Directories;
public interface IDirectoryManager:IDomainService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="containerName"></param>
    /// <param name="name"></param>
    /// <param name="parentId"></param>
    /// <returns></returns>
    Task<DirectoryDescriptor> CreateAsync(string containerName, string name, Guid? parentId = null);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteAsync(Guid id);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="directory"></param>
    /// <param name="newParentId"></param>
    /// <param name="order"></param>
    /// <returns></returns>
    Task MoveAsync(DirectoryDescriptor directory, Guid? newParentId,int order);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="directory"></param>
    /// <param name="newName"></param>
    /// <returns></returns>
    Task RenameAsync(DirectoryDescriptor directory, string newName);
}
