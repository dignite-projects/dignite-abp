using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Dignite.Abp.FileExplorer.Directories;
public interface IDirectoryDescriptorRepository : IBasicRepository<DirectoryDescriptor, Guid>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="containerName"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<DirectoryDescriptor>> GetListAsync(string containerName, CancellationToken cancellationToken = default);
}
