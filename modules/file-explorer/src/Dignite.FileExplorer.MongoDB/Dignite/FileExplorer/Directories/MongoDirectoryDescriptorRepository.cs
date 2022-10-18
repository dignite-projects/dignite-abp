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

namespace Dignite.FileExplorer.Directories;
public class MongoDirectoryDescriptorRepository : MongoDbRepository<IFileExplorerMongoDbContext, DirectoryDescriptor, Guid>, IDirectoryDescriptorRepository
{
    public MongoDirectoryDescriptorRepository(IMongoDbContextProvider<IFileExplorerMongoDbContext> dbContextProvider) : base(
        dbContextProvider)
    {
    }

    public async Task<bool> AnyChildrenAsync(string containerName, Guid? parentId, CancellationToken cancellationToken = default)
    {
        var token = GetCancellationToken(cancellationToken);
        return await(await GetMongoQueryableAsync(token))
            .AnyAsync(x => x.ContainerName == containerName && x.ParentId == parentId);
    }

    public async Task<DirectoryDescriptor> FindByNameAsync(string containerName, string name, Guid? parentId, CancellationToken cancellationToken = default)
    {
        return await GetAsync(x => x.ContainerName == containerName && x.Name == name, cancellationToken: cancellationToken);
    }

    public async Task<List<DirectoryDescriptor>> GetAllChildrenAsync(string containerName, CancellationToken cancellationToken = default)
    {
        var token = GetCancellationToken(cancellationToken);
        return await (await GetMongoQueryableAsync(token))
            .Where(dd => dd.ContainerName == containerName)
            .ToListAsync();
    }

    public async Task<int> GetChildrenCountAsync(string containerName, Guid? parentId, CancellationToken cancellationToken = default)
    {
        var token = GetCancellationToken(cancellationToken);

        var query = await GetListQueryAsync(
            containerName, parentId,token);

        return await query.As<IMongoQueryable<DirectoryDescriptor>>().CountAsync(token);
    }

    public async Task<List<DirectoryDescriptor>> GetChildrenListAsync(string containerName, Guid? parentId, int skipCount = 0, int maxResultCount = int.MaxValue, CancellationToken cancellationToken = default)
    {
        var token = GetCancellationToken(cancellationToken);

        var query = await GetListQueryAsync(
            containerName, parentId,token);

        return await query.OrderBy(dd=>dd.Order)
                  .As<IMongoQueryable<DirectoryDescriptor>>()
                  .PageBy<DirectoryDescriptor, IMongoQueryable<DirectoryDescriptor>>(skipCount, maxResultCount)
                  .ToListAsync(token);
    }

    public async Task<bool> NameExistsAsync(string containerName, string name, Guid? parentId, Guid? ignoredId = null, CancellationToken cancellationToken = default)
    {
        var token = GetCancellationToken(cancellationToken);
        return await (await GetMongoQueryableAsync(token))
            .AnyAsync(x => x.ContainerName == containerName && x.ParentId == parentId && x.Name==name);
    }

    protected virtual async Task<IQueryable<DirectoryDescriptor>> GetListQueryAsync(
        string containerName,
        Guid? parentId,
        CancellationToken cancellationToken = default)
    {
        return (await GetMongoQueryableAsync(cancellationToken))
            .Where(dd => dd.ContainerName == containerName && dd.ParentId == parentId);
    }
}
