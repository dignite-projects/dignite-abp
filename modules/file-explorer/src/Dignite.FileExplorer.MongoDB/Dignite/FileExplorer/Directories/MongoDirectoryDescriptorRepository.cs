using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Dignite.FileExplorer.MongoDB;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;

namespace Dignite.FileExplorer.Directories;

public class MongoDirectoryDescriptorRepository : MongoDbRepository<IFileExplorerMongoDbContext, DirectoryDescriptor, Guid>, IDirectoryDescriptorRepository
{
    public MongoDirectoryDescriptorRepository(IMongoDbContextProvider<IFileExplorerMongoDbContext> dbContextProvider) : base(
        dbContextProvider)
    {
    }


    public async Task<List<DirectoryDescriptor>> GetListAsync(Guid creatorId, string containerName, Guid? parentId, CancellationToken cancellationToken = default)
    {
        var token = GetCancellationToken(cancellationToken);

        var query = await GetListQueryAsync(
            creatorId, containerName, parentId, token);

        return await query
                  .As<IMongoQueryable<DirectoryDescriptor>>()
                  .ToListAsync(token);
    }

    public async Task<bool> NameExistsAsync(Guid creatorId, string containerName, string name, Guid? parentId, CancellationToken cancellationToken = default)
    {
        var token = GetCancellationToken(cancellationToken);
        return await (await GetMongoQueryableAsync(token))
            .As<IMongoQueryable<DirectoryDescriptor>>()
            .AnyAsync(x => x.ContainerName == containerName && x.CreatorId == creatorId && x.ParentId == parentId && x.Name == name);
    }

    public async Task<int> GetMaxOrderAsync(Guid creatorId, string containerName, Guid? parentId, CancellationToken cancellationToken = default)
    {
        var token = GetCancellationToken(cancellationToken);

        var query = await GetListQueryAsync(
            creatorId, containerName, parentId, token);

        return await query.OrderBy(dd => dd.Order)
                  .As<IMongoQueryable<DirectoryDescriptor>>()
                  .DefaultIfEmpty()
                  .As<IMongoQueryable<DirectoryDescriptor>>()
                  .MaxAsync(d=> (int?)d.Order) ?? 0;
    }

    public async Task<List<DirectoryDescriptor>> GetAllByUserAsync(Guid creatorId, string containerName, CancellationToken cancellationToken = default(CancellationToken))
    {
        var token = GetCancellationToken(cancellationToken);
        return await (await GetMongoQueryableAsync(token))
            .Where(dd => dd.ContainerName == containerName && dd.CreatorId == creatorId)
            .OrderBy(dd=>dd.ParentId)
            .ThenBy(dd => dd.Order)
            .ToListAsync();
    }

    protected virtual async Task<IQueryable<DirectoryDescriptor>> GetListQueryAsync(
        Guid creatorId,
        string containerName,
        Guid? parentId,
        CancellationToken cancellationToken = default)
    {
        return (await GetMongoQueryableAsync(cancellationToken))
            .Where(dd => dd.ContainerName == containerName && dd.CreatorId == creatorId && dd.ParentId == parentId);
    }
}