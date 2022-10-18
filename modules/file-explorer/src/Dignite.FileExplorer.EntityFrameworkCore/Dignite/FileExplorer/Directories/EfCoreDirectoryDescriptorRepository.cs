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

    public async Task<bool> AnyChildrenAsync(string containerName, Guid? parentId, CancellationToken cancellationToken = default)
    {
        return await (await GetDbSetAsync())
                   .AnyAsync(b => b.ContainerName == containerName && b.ParentId == parentId, GetCancellationToken(cancellationToken));
    }

    public async Task<DirectoryDescriptor> FindByNameAsync(string containerName, string name, Guid? parentId, CancellationToken cancellationToken = default)
    {
        return await base.FindAsync(dd => dd.ContainerName == containerName && dd.ParentId == parentId,true,GetCancellationToken(cancellationToken));
    }

    public async Task<List<DirectoryDescriptor>> GetAllChildrenAsync(string containerName, CancellationToken cancellationToken = default)
    {
        return await (await GetDbSetAsync()).Where(dd => dd.ContainerName == containerName).ToListAsync(GetCancellationToken(cancellationToken));
    }

    public async Task<int> GetChildrenCountAsync(string containerName, Guid? parentId, CancellationToken cancellationToken = default)
    {
        cancellationToken = GetCancellationToken(cancellationToken);
        var query = await GetListQueryAsync(
            containerName, parentId
        );

        return await query.CountAsync(cancellationToken);
    }

    public async Task<List<DirectoryDescriptor>> GetChildrenListAsync(string containerName, Guid? parentId, int skipCount = 0, int maxResultCount = int.MaxValue, CancellationToken cancellationToken = default)
    {
        cancellationToken = GetCancellationToken(cancellationToken);

        var query = await GetListQueryAsync(
            containerName, parentId
        );

        return await query.OrderBy(dd=>dd.Order)
            .PageBy(skipCount, maxResultCount)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> NameExistsAsync(string containerName, string name, Guid? parentId, Guid? ignoredId = null, CancellationToken cancellationToken = default)
    {
        return await (await GetDbSetAsync())
            .WhereIf(ignoredId.HasValue, dd => dd.Id != ignoredId)
            .AnyAsync(b => b.ContainerName == containerName && b.Name == name, GetCancellationToken(cancellationToken));
    }

    protected virtual async Task<IQueryable<DirectoryDescriptor>> GetListQueryAsync(
        string containerName,
        Guid? parentId)
    {
        return (await GetDbSetAsync()).AsNoTracking()
            .Where(dd => dd.ContainerName == containerName && dd.ParentId == parentId);
    }
}