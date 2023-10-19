using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Dignite.Abp.UserPoints.MongoDB;

[ConnectionStringName(UserPointsDbProperties.ConnectionStringName)]
public class UserPointsMongoDbContext : AbpMongoDbContext, IUserPointsMongoDbContext
{
    public IMongoCollection<UserPointsItem> UserPointsItems => Collection<UserPointsItem>();
    public IMongoCollection<UserPointsOrder> UserPointsOrders => Collection<UserPointsOrder>();
    public IMongoCollection<UserPointsBlock> UserPointsBlocks => Collection<UserPointsBlock>();

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureUserPoints();
    }
}
