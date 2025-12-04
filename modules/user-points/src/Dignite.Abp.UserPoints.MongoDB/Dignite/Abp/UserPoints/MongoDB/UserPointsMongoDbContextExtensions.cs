using Volo.Abp;
using Volo.Abp.MongoDB;

namespace Dignite.Abp.UserPoints.MongoDB;

public static class UserPointsMongoDbContextExtensions
{
    public static void ConfigureUserPoints(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<UserPoint>(b =>
        {
            b.CollectionName = UserPointsDbProperties.DbTablePrefix + "UserPoints";
        });
    }
}
