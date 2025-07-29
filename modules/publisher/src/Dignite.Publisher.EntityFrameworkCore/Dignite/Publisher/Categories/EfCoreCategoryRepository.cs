using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dignite.Publisher.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.Publisher.Categories;
public class EfCoreCategoryRepository : EfCoreRepository<IPublisherDbContext, Category, Guid>, ICategoryRepository
{
    public EfCoreCategoryRepository(IDbContextProvider<IPublisherDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public virtual async Task<List<Category>> GetListAsync(string? local, CancellationToken cancellationToken = default)
    {
        return await (await GetQueryableAsync())
            .Where(x=>x.Local==local)
            .ToListAsync(GetCancellationToken(cancellationToken));
    }

    public virtual async Task<bool> NameExistsAsync(Guid? parentId, string name, CancellationToken cancellationToken = default)
    {
        return await (await GetQueryableAsync()).AnyAsync(x => x.ParentId == parentId && x.Name == name, GetCancellationToken(cancellationToken));
    }
}
