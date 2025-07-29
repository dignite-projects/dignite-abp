using System.Linq;
using Dignite.Publisher.Posts;
using Microsoft.EntityFrameworkCore;

namespace Dignite.Publisher;
public static class PublisherEntityFrameworkCoreQueryableExtensions
{
    public static IQueryable<Post> IncludeDetails(this IQueryable<Post> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.PostCategories);
    }
}
