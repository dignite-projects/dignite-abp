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

namespace Dignite.FileExplorer.Directories;

public class EfCoreDirectoryDescriptorRepository : EfCoreRepository<IFileExplorerDbContext, DirectoryDescriptor, Guid>, IDirectoryDescriptorRepository
{
    public EfCoreDirectoryDescriptorRepository(
        IDbContextProvider<IFileExplorerDbContext> dbContextProvider
        )
        : base(dbContextProvider)
    {
    }

    public async Task<bool> AnyChildrenAsync(Guid creatorId, string containerName, Guid? parentId, CancellationToken cancellationToken = default)
    {
        return await (await GetDbSetAsync())
                   .AnyAsync(b => b.ContainerName == containerName && b.CreatorId == creatorId && b.ParentId == parentId, GetCancellationToken(cancellationToken));
    }

    public async Task<DirectoryDescriptor> FindByNameAsync(Guid creatorId, string containerName, Guid? parentId, string name, CancellationToken cancellationToken = default)
    {
        return await base.FindAsync(dd => dd.ContainerName == containerName && dd.CreatorId == creatorId && dd.ParentId == parentId && dd.Name == name, true, GetCancellationToken(cancellationToken));
    }

    public async Task<int> GetCountAsync(Guid creatorId, string containerName, Guid? parentId, CancellationToken cancellationToken = default)
    {
        cancellationToken = GetCancellationToken(cancellationToken);
        var query = await GetListQueryAsync(
            creatorId, containerName, parentId
        );

        return await query.CountAsync(cancellationToken);
    }

    public async Task<List<DirectoryDescriptor>> GetListAsync(Guid creatorId, string containerName, Guid? parentId, int skipCount = 0, int maxResultCount = int.MaxValue, CancellationToken cancellationToken = default)
    {
        cancellationToken = GetCancellationToken(cancellationToken);

        var query = await GetListQueryAsync(
            creatorId, containerName, parentId
        );

        return await query.OrderBy(dd => dd.Order)
            .PageBy(skipCount, maxResultCount)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> GetMaxOrderAsync(Guid creatorId, string containerName, Guid? parentId, CancellationToken cancellationToken = default)
    {
        cancellationToken = GetCancellationToken(cancellationToken);

        var query = await GetListQueryAsync(
            creatorId, containerName, parentId
        );

        return await query.DefaultIfEmpty().MaxAsync(d=> (int?)d.Order)??0;
    }

    public async Task<bool> NameExistsAsync(Guid creatorId, string containerName, string name, Guid? parentId, Guid? ignoredId = null, CancellationToken cancellationToken = default)
    {
        return await (await GetDbSetAsync())
            .WhereIf(ignoredId.HasValue, dd => dd.Id != ignoredId)
            .AnyAsync(b => b.ContainerName == containerName && b.CreatorId == creatorId && b.ParentId == parentId && b.Name == name, GetCancellationToken(cancellationToken));
    }

    protected virtual async Task<IQueryable<DirectoryDescriptor>> GetListQueryAsync(
        Guid creatorId,
        string containerName,
        Guid? parentId)
    {
        return (await GetDbSetAsync()).AsNoTracking()
            .Where(dd => dd.ContainerName == containerName && dd.CreatorId == creatorId && dd.ParentId == parentId);
    }
}