
using System.Linq;
using Dignite.Cms.Entries;
using Microsoft.EntityFrameworkCore;

namespace Dignite.Cms
{
    public static class CmsEntityFrameworkCoreQueryableExtensions
    {
        public static IQueryable<Sections.Section> IncludeDetails(this IQueryable<Sections.Section> queryable, bool include = true)
        {
            if (!include)
            {
                return queryable;
            }

            return queryable
                .Include(s => s.EntryTypes);
        }
        public static IQueryable<Entry> IncludeDetails(this IQueryable<Entry> queryable, bool include = true)
        {
            if (!include)
            {
                return queryable;
            }

            return queryable;
        }
    }
}
