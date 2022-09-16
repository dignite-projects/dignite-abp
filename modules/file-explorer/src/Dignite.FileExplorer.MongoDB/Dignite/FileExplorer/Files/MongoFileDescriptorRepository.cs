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

    public async Task<bool> BlobNameExistsAsync(string containerName, string blobName, Guid? ignoredId = null, CancellationToken cancellationToken = default)
    {
        var token = GetCancellationToken(cancellationToken);
        return await (await GetMongoQueryableAsync(token))
            .WhereIf(ignoredId != null, ct => ct.Id != ignoredId)
            .As<IMongoQueryable<FileDescriptor>>()
            .AnyAsync(x => x.ContainerName == containerName && x.BlobName==blobName);
    }

    public async Task<FileDescriptor> FindAsync(string containerName, string blobName, CancellationToken cancellationToken = default)
    {
        return await GetAsync(x => x.ContainerName == containerName && x.BlobName == blobName, cancellationToken: cancellationToken);
    }

    public async Task<int> GetCountAsync(string containerName, string filter = null, string entityType = null, string entityId = null, CancellationToken cancellationToken = default)
    {
        var token = GetCancellationToken(cancellationToken);

        var query = await GetListQueryAsync(
            containerName, filter, entityType, entityId,
            token);

        return await query.As<IMongoQueryable<FileDescriptor>>().CountAsync(token);
    }

    public async Task<List<FileDescriptor>> GetListAsync(string containerName, string filter = null, string entityTypeFullName = null, string entityId = null, string sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
    {
        var token = GetCancellationToken(cancellationToken);

        var query = await GetListQueryAsync(
            containerName, filter, entityTypeFullName, entityId,
            token);

        return await query.OrderBy(sorting.IsNullOrWhiteSpace() ? $"{nameof(FileDescriptor.CreationTime)} desc" : sorting)
                  .As<IMongoQueryable<FileDescriptor>>()
                  .PageBy<FileDescriptor, IMongoQueryable<FileDescriptor>>(skipCount, maxResultCount)
                  .ToListAsync(token);
    }



    protected virtual async Task<IQueryable<FileDescriptor>> GetListQueryAsync(
        string containerName,
        string filter = null,
        string entityTypeFullName = null,
        string entityId = null,
        CancellationToken cancellationToken = default)
    {
        return (await GetMongoQueryableAsync(cancellationToken))
            .WhereIf(!containerName.IsNullOrWhiteSpace(), fd => fd.ContainerName == containerName)
            .WhereIf(!filter.IsNullOrWhiteSpace(), fd => fd.Name.Contains(filter))
            .WhereIf(!entityTypeFullName.IsNullOrWhiteSpace(), fd => fd.EntityTypeFullName == entityTypeFullName)
            .WhereIf(!entityId.IsNullOrWhiteSpace(), fd => fd.EntityId == entityId);
    }
}
