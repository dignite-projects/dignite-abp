using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Dignite.Abp.UserPoints.MongoDB;

[ConnectionStringName(UserPointsDbProperties.ConnectionStringName)]
public interface IUserPointsMongoDbContext : IAbpMongoDbContext
{
    IMongoCollection<UserPointAccount> Accounts { get; }
    IMongoCollection<UserPointTransaction> Transactions { get; }
}
