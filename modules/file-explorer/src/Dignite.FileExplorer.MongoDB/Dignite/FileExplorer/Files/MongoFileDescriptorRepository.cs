using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Dignite.FileExplorer.MongoDB;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;

namespace Dignite.FileExplorer.Files;

public class MongoFileDescriptorRepository : MongoDbRepository<IFileExplorerMongoDbContext, FileDescriptor, Guid>, IFileDescriptorRepository
{
    public MongoFileDescriptorRepository(IMongoDbContextProvider<IFileExplorerMongoDbContext> dbContextProvider) : base(
        dbContextProvider)
    {
    }

    public async Task<bool> BlobNameExistsAsync(string containerName, string blobName, CancellationToken cancellationToken = default)
    {
        var token = GetCancellationToken(cancellationToken);
        return await (await GetMongoQueryableAsync(token))
            .AnyAsync(x => x.ContainerName == containerName && x.BlobName == blobName && x.ReferBlobName == null, token);
    }

    public async Task<bool> Md5ExistsAsync(string containerName, string md5, CancellationToken cancellationToken = default)
    {
        var token = GetCancellationToken(cancellationToken);
        return await (await GetMongoQueryableAsync(token))
                   .AnyAsync(b => b.ContainerName == containerName && b.Md5 != null && b.Md5 == md5, token);
    }

    public async Task<bool> ReferencingAnyAsync(string containerName, string blobName, CancellationToken cancellationToken = default)
    {
        var token = GetCancellationToken(cancellationToken);
        return await (await GetMongoQueryableAsync(token))
                   .AnyAsync(b => b.ContainerName == containerName && b.ReferBlobName != null && b.ReferBlobName == blobName, token);
    }

    public async Task<FileDescriptor> FindByBlobNameAsync(string containerName, string blobName, CancellationToken cancellationToken = default)
    {
        var token = GetCancellationToken(cancellationToken);
        return await FindAsync(x => x.ContainerName == containerName && x.BlobName == blobName && x.ReferBlobName == null, false, token);
    }

    public async Task<FileDescriptor> FindByMd5Async(string containerName, string md5, CancellationToken cancellationToken = default)
    {
        var token = GetCancellationToken(cancellationToken);
        return await FindAsync(b => b.ContainerName == containerName && b.Md5 != null && b.Md5 == md5, false, token);
    }

    public async Task<int> GetCountAsync(string containerName,
        Guid? creatorId,
        Guid? directoryId, string filter = null, string entityId = null, CancellationToken cancellationToken = default)
    {
        var token = GetCancellationToken(cancellationToken);

        var query = await GetListQueryAsync(
            containerName,
            creatorId,
            directoryId,
            filter,
            entityId,
            token);

        return await query.As<IMongoQueryable<FileDescriptor>>().CountAsync(token);
    }

    public async Task<List<FileDescriptor>> GetListAsync(string containerName,
        Guid? creatorId,
        Guid? directoryId, string filter = null, string entityId = null, string sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
    {
        var token = GetCancellationToken(cancellationToken);

        var query = await GetListQueryAsync(
            containerName,
            creatorId,
            directoryId,
            filter,
            entityId,
            token);

        return await query.OrderBy(sorting.IsNullOrWhiteSpace() ? $"{nameof(FileDescriptor.CreationTime)} asc" : sorting)
                  .As<IMongoQueryable<FileDescriptor>>()
                  .PageBy<FileDescriptor, IMongoQueryable<FileDescriptor>>(skipCount, maxResultCount)
                  .ToListAsync(token);
    }

    protected virtual async Task<IQueryable<FileDescriptor>> GetListQueryAsync(
        string containerName,
        Guid? creatorId,
        Guid? directoryId,
        string filter = null,
        string entityId = null,
        CancellationToken cancellationToken = default)
    {
        return (await GetMongoQueryableAsync(cancellationToken))
            .Where(fd => fd.ContainerName == containerName)
            .WhereIf(directoryId.HasValue, fd => fd.DirectoryId == directoryId.Value)
            .WhereIf(creatorId.HasValue, fd => fd.CreatorId == creatorId.Value)
            .WhereIf(!filter.IsNullOrWhiteSpace(), fd => fd.Name.Contains(filter))
            .WhereIf(!entityId.IsNullOrWhiteSpace(), fd => fd.EntityId == entityId);
    }
}