using Volo.Abp;
using Volo.Abp.MongoDB;

namespace Dignite.Abp.UserPoints.MongoDB;

public static class UserPointsMongoDbContextExtensions
{
    public static void ConfigureUserPoints(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<UserPointsItem>(b =>
        {
            b.CollectionName = UserPointsDbProperties.DbTablePrefix + "UserPointsItems";
        });

        builder.Entity<UserPointsOrder>(b =>
        {
            b.CollectionName = UserPointsDbProperties.DbTablePrefix + "UserPointsOrders";
        });

        builder.Entity<UserPointsBlock>(b =>
        {
            b.CollectionName = UserPointsDbProperties.DbTablePrefix + "UserPointsBlocks";
        });
    }
}
