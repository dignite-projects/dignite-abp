using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Dignite.FileExplorer.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.FileExplorer.Files;

public class EfCoreFileDescriptorRepository : EfCoreRepository<IFileExplorerDbContext, FileDescriptor, Guid>, IFileDescriptorRepository
{
    public EfCoreFileDescriptorRepository(
        IDbContextProvider<IFileExplorerDbContext> dbContextProvider
        )
        : base(dbContextProvider)
    {
    }

    public async Task<bool> BlobNameExistsAsync(string containerName, string blobName, CancellationToken cancellationToken = default)
    {
        return await (await GetDbSetAsync())
                   .AnyAsync(b => b.ContainerName == containerName && b.BlobName == blobName, GetCancellationToken(cancellationToken));
    }

    public async Task<bool> Md5ExistsAsync(string containerName, string md5, CancellationToken cancellationToken = default)
    {
        return await (await GetDbSetAsync())
                   .AnyAsync(b => b.ContainerName == containerName && b.Md5 != null && b.Md5 == md5, GetCancellationToken(cancellationToken));
    }

    public async Task<bool> ReferencingAnyAsync(string containerName, string blobName, CancellationToken cancellationToken = default)
    {
        return await (await GetDbSetAsync())
                   .AnyAsync(b => b.ContainerName == containerName && b.ReferBlobName != null && b.ReferBlobName == blobName, GetCancellationToken(cancellationToken));
    }

    public async Task<FileDescriptor> FindByBlobNameAsync(string containerName, string blobName, CancellationToken cancellationToken = default)
    {
        return await (await GetDbSetAsync())
                   .FirstOrDefaultAsync(b => b.ContainerName == containerName && b.BlobName == blobName, GetCancellationToken(cancellationToken));
    }

    public async Task<FileDescriptor> FindByMd5Async(string containerName, string md5, CancellationToken cancellationToken = default)
    {
        return await (await GetDbSetAsync())
                   .FirstOrDefaultAsync(b => b.ContainerName == containerName && b.Md5 != null && b.Md5 == md5, GetCancellationToken(cancellationToken));
    }

    public async Task<int> GetCountAsync(string containerName,
        Guid? creatorId, Guid? directoryId, string filter = null, string entityId = null, CancellationToken cancellationToken = default)
    {
        cancellationToken = GetCancellationToken(cancellationToken);

        var query = await GetListQueryAsync(
            containerName, creatorId, directoryId, filter, entityId
        );

        return await query.CountAsync(cancellationToken);
    }

    public async Task<List<FileDescriptor>> GetListAsync(
        string containerName,
        Guid? creatorId,
        Guid? directoryId,
        string filter = null,
        string entityId = null,
        string sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default)
    {
        cancellationToken = GetCancellationToken(cancellationToken);

        var query = await GetListQueryAsync(
            containerName, creatorId, directoryId, filter, entityId
        );

        return await query.OrderBy(sorting.IsNullOrWhiteSpace() ? $"{nameof(FileDescriptor.CreationTime)} desc" : sorting)
            .PageBy(skipCount, maxResultCount)
            .ToListAsync(cancellationToken);
    }

    protected virtual async Task<IQueryable<FileDescriptor>> GetListQueryAsync(
        string containerName,
        Guid? creatorId,
        Guid? directoryId,
        string filter = null,
        string entityId = null)
    {
        return (await GetDbSetAsync()).AsNoTracking()
            .Where(fd => fd.ContainerName == containerName)
            .WhereIf(directoryId.HasValue, fd => fd.DirectoryId == directoryId.Value)
            .WhereIf(creatorId.HasValue, fd => fd.CreatorId == creatorId.Value)
            .WhereIf(!filter.IsNullOrWhiteSpace(), fd => fd.Name.Contains(filter))
            .WhereIf(!entityId.IsNullOrWhiteSpace(), fd => fd.EntityId == entityId);
    }
}