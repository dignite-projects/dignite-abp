using System;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;

namespace Dignite.Abp.UserPoints.MongoDB;

public class MongoUserPointAccountRepository : MongoDbRepository<IUserPointsMongoDbContext, UserPointAccount, Guid>, IUserPointAccountRepository
{
    public MongoUserPointAccountRepository(IMongoDbContextProvider<IUserPointsMongoDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}
