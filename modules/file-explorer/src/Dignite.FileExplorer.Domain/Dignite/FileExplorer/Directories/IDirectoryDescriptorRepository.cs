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
    /// <param name="creatorId"></param>
    /// <param name="containerName"></param>
    /// <param name="name"></param>
    /// <param name="parentId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> NameExistsAsync(Guid creatorId, string containerName, string name, Guid? parentId, CancellationToken cancellationToken = default(CancellationToken));

    /// <summary>
    ///
    /// </summary>
    /// <param name="creatorId"></param>
    /// <param name="containerName"></param>
    /// <param name="parentId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<DirectoryDescriptor>> GetListAsync(Guid creatorId, string containerName, Guid? parentId, CancellationToken cancellationToken = default(CancellationToken));


    /// <summary>
    /// 
    /// </summary>
    /// <param name="creatorId"></param>
    /// <param name="containerName"></param>
    /// <param name="parentId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<int> GetMaxOrderAsync(Guid creatorId, string containerName, Guid? parentId, CancellationToken cancellationToken = default(CancellationToken));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="creatorId"></param>
    /// <param name="containerName"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<DirectoryDescriptor>> GetAllByUserAsync(Guid creatorId, string containerName, CancellationToken cancellationToken = default(CancellationToken));
}