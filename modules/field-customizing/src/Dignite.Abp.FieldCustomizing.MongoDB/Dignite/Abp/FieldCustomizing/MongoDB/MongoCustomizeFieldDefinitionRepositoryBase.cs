using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;

namespace Dignite.Abp.FieldCustomizing.MongoDB;

public abstract class MongoCustomizeFieldDefinitionRepositoryBase<TDbContext, TFieldDefinition> : MongoDbRepository<TDbContext, TFieldDefinition, Guid>, ICustomizeFieldDefinitionRepository<TFieldDefinition>
    where TDbContext : IAbpMongoDbContext
    where TFieldDefinition : CustomizeFieldDefinitionBase
{
    protected MongoCustomizeFieldDefinitionRepositoryBase(IMongoDbContextProvider<TDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public async Task<bool> NameExistsAsync(string name, Guid? ignoredId = null, CancellationToken cancellationToken = default)
    {
        cancellationToken = GetCancellationToken(cancellationToken);
        return await (await GetMongoQueryableAsync(cancellationToken))
            .WhereIf(ignoredId != null, ct => ct.Id != ignoredId)
            .As<IMongoQueryable<TFieldDefinition>>()
            .AnyAsync(x => x.Name == name);
    }

    public virtual async Task<List<TFieldDefinition>> GetListAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {
        cancellationToken = GetCancellationToken(cancellationToken);
        return await (await GetMongoQueryableAsync(cancellationToken))
            .Where(u => ids.Contains(u.Id))
            .ToListAsync(cancellationToken);
    }
}