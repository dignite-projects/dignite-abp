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
    /// <param name="ignoredId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> NameExistsAsync(Guid creatorId, string containerName, string name, Guid? parentId, Guid? ignoredId = null, CancellationToken cancellationToken = default(CancellationToken));

    /// <summary>
    ///
    /// </summary>
    /// <param name="creatorId"></param>
    /// <param name="containerName"></param>
    /// <param name="parentId"></param>
    /// <param name="name"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<DirectoryDescriptor> FindByNameAsync(Guid creatorId, string containerName, Guid? parentId, string name, CancellationToken cancellationToken = default(CancellationToken));

    /// <summary>
    ///
    /// </summary>
    /// <param name="creatorId"></param>
    /// <param name="containerName"></param>
    /// <param name="parentId"></param>
    /// <param name="skipCount"></param>
    /// <param name="maxResultCount"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<DirectoryDescriptor>> GetChildrenListAsync(Guid creatorId, string containerName, Guid? parentId, int skipCount = 0, int maxResultCount = int.MaxValue, CancellationToken cancellationToken = default(CancellationToken));

    /// <summary>
    ///
    /// </summary>
    /// <param name="creatorId"></param>
    /// <param name="containerName"></param>
    /// <param name="parentId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<int> GetChildrenCountAsync(Guid creatorId, string containerName, Guid? parentId, CancellationToken cancellationToken = default(CancellationToken));

    /// <summary>
    ///
    /// </summary>
    /// <param name="creatorId"></param>
    /// <param name="containerName"></param>
    /// <param name="parentId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> AnyChildrenAsync(Guid creatorId, string containerName, Guid? parentId, CancellationToken cancellationToken = default(CancellationToken));
}