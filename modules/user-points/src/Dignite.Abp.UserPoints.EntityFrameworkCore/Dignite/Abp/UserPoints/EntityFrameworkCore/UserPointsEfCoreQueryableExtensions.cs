using System.Linq;

namespace Dignite.Abp.UserPoints.EntityFrameworkCore;

public static class UserPointsEfCoreQueryableExtensions
{
    public static IQueryable<UserPoint> IncludeDetails(this IQueryable<UserPoint> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable;
    }
}
