using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.Abp.FieldCustomizing.EntityFrameworkCore;
public abstract class EfCoreCustomizeFieldDefinitionRepositoryBase<TDbContext, TFieldDefinition> : EfCoreRepository<TDbContext, TFieldDefinition, Guid>, ICustomizeFieldDefinitionRepository<TFieldDefinition>
    where TDbContext : IEfCoreDbContext
    where TFieldDefinition : CustomizeFieldDefinitionBase
{
    protected EfCoreCustomizeFieldDefinitionRepositoryBase(IDbContextProvider<TDbContext> dbContextProvider)
        : base(dbContextProvider)
    {

    }

    public async Task<bool> NameExistsAsync(string name, Guid? ignoredId = null, CancellationToken cancellationToken = default)
    {
        return await (await GetDbSetAsync())
                   .WhereIf(ignoredId != null, ct => ct.Id != ignoredId)
                   .AnyAsync(b => b.Name == name, GetCancellationToken(cancellationToken));
    }

    public virtual async Task<List<TFieldDefinition>> GetListAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {
        return await (await GetDbSetAsync())
            .Where(u => ids.Contains(u.Id))
            .ToListAsync(GetCancellationToken(cancellationToken));
    }

}
