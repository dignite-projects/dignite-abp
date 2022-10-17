using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Dignite.FileExplorer.Directories;
public interface IDirectoryDescriptorRepository : IBasicRepository<DirectoryDescriptor, Guid>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="containerName"></param>
    /// <param name="name"></param>
    /// <param name="parentId"></param>
    /// <param name="ignoredId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> NameExistsAsync(string containerName, string name, Guid? parentId, Guid? ignoredId = null, CancellationToken cancellationToken = default(CancellationToken));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="containerName"></param>
    /// <param name="parentId"></param>
    /// <param name="name"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<DirectoryDescriptor> FindByNameAsync(string containerName, string name, Guid? parentId, CancellationToken cancellationToken = default(CancellationToken));

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// As a developer, you can use this method only when you can predict that the number of directories will not be large
    /// </remarks>
    /// <param name="containerName"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<DirectoryDescriptor>> GetAllChildrenAsync(string containerName,  CancellationToken cancellationToken = default(CancellationToken));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="containerName"></param>
    /// <param name="parentId"></param>
    /// <param name="skipCount"></param>
    /// <param name="maxResultCount"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<DirectoryDescriptor>> GetListAsync(string containerName, Guid? parentId, int skipCount=0, int maxResultCount=int.MaxValue, CancellationToken cancellationToken = default(CancellationToken));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="containerName"></param>
    /// <param name="parentId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<int> GetCountAsync(string containerName, Guid? parentId, CancellationToken cancellationToken = default(CancellationToken));
}
