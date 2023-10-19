using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Dignite.Abp.UserPoints.EntityFrameworkCore;

public static class UserPointsEfCoreQueryableExtensions
{
    public static IQueryable<UserPointsItem> IncludeDetails(this IQueryable<UserPointsItem> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.PointsBlocks);
    }

    public static IQueryable<UserPointsBlock> IncludeDetails(this IQueryable<UserPointsBlock> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.UserPointsItem);
    }
}
