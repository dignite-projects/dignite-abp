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

    public async Task<int> GetMaxOrderAsync(Guid creatorId, string containerName, Guid? parentId, CancellationToken cancellationToken = default)
    {
        cancellationToken = GetCancellationToken(cancellationToken);

        var query = await GetListQueryAsync(
            creatorId, containerName, parentId
        );

        return await query.DefaultIfEmpty().MaxAsync(d=> (int?)d.Order)??0;
    }

    public async Task<bool> NameExistsAsync(Guid creatorId, string containerName, string name, Guid? parentId, CancellationToken cancellationToken = default)
    {
        return await (await GetDbSetAsync())
            .AnyAsync(b => b.ContainerName == containerName && b.CreatorId == creatorId && b.ParentId == parentId && b.Name == name, GetCancellationToken(cancellationToken));
    }

    public async Task<List<DirectoryDescriptor>> GetListAsync(Guid creatorId, string containerName, Guid? parentId, CancellationToken cancellationToken = default(CancellationToken))
    {
        return await (await GetListQueryAsync(
            creatorId, containerName, parentId
        )).ToListAsync();
    }

    public async Task<List<DirectoryDescriptor>> GetAllByUserAsync(Guid creatorId, string containerName, CancellationToken cancellationToken = default(CancellationToken))
    {
        return await (await GetDbSetAsync())
            .Where(dd => dd.ContainerName == containerName && dd.CreatorId == creatorId)
            .OrderBy(dd=>dd.ParentId)
            .ThenBy(m=>m.Order)
            .ToListAsync(cancellationToken);
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