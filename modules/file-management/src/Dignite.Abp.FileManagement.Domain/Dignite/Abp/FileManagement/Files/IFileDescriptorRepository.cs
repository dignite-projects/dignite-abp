using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Dignite.Abp.FileManagement.Files;

public interface IFileDescriptorRepository : IBasicRepository<FileDescriptor, Guid>
{
    Task<bool> ExistsAsync(string containerName, string blobName, CancellationToken cancellationToken = default);
    Task<bool> HashExistsAsync(string containerName, string hash, CancellationToken cancellationToken = default);
    Task<bool> ReferenceExistsAsync(string containerName, string blobName, CancellationToken cancellationToken = default);
    Task<FileDescriptor> FindAsync(string containerName, string blobName, CancellationToken cancellationToken = default);
    Task<FileDescriptor> FindByBlobHashAsync(string containerName, string hash, CancellationToken cancellationToken = default);

    Task<List<FileDescriptor>> GetListAsync(
        string containerName, 
        string filter=null , 
        string entityType=null, 
        string entityId=null, 
        string sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0, 
        CancellationToken cancellationToken = default);
    Task<int> GetCountAsync(string containerName, string filter = null, string entityType = null, string entityId = null, CancellationToken cancellationToken = default);
}
