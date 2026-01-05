using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Dignite.Abp.UserPoints.MongoDB;

[ConnectionStringName(UserPointsDbProperties.ConnectionStringName)]
public class UserPointsMongoDbContext : AbpMongoDbContext, IUserPointsMongoDbContext
{
    public IMongoCollection<UserPointAccount> Accounts => Collection<UserPointAccount>();
    public IMongoCollection<UserPointTransaction> Transactions => Collection<UserPointTransaction>();

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureUserPoints();
    }
}
