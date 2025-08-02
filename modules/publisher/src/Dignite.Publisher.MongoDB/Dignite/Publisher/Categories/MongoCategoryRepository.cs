using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Dignite.Publisher.MongoDB;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;

namespace Dignite.Publisher.Categories;
public class MongoCategoryRepository : MongoDbRepository<IPublisherMongoDbContext, Category, Guid>, ICategoryRepository
{
    public MongoCategoryRepository(IMongoDbContextProvider<IPublisherMongoDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public virtual async Task<List<Category>> GetListAsync(string? locale, CancellationToken cancellationToken = default)
    {
        var token = GetCancellationToken(cancellationToken);

        return await (await GetQueryableAsync(token))
                .Where(b => b.Locale == locale)
                .ToListAsync(token);
    }

    public virtual async Task<bool> NameExistsAsync(Guid? parentId, string name, CancellationToken cancellationToken = default)
    {
        var token = GetCancellationToken(cancellationToken);
        return await(await GetQueryableAsync(token)).AnyAsync(x => x.ParentId == parentId && x.Name == name, token);
    }
}
