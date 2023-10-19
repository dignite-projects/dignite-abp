using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Dignite.Abp.UserPoints.MongoDB;

[ConnectionStringName(UserPointsDbProperties.ConnectionStringName)]
public interface IUserPointsMongoDbContext : IAbpMongoDbContext
{
    IMongoCollection<UserPointsItem> UserPointsItems { get; }
    IMongoCollection<UserPointsOrder> UserPointsOrders { get; }
    IMongoCollection<UserPointsBlock> UserPointsBlocks { get; }
}
