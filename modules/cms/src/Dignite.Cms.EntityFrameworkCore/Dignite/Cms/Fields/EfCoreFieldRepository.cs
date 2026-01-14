using Dignite.Cms.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Dignite.Cms.Fields
{
    public class EfCoreFieldRepository : EfCoreRepository<ICmsDbContext, Field,Guid>, IFieldRepository
    {
        public EfCoreFieldRepository(
            IDbContextProvider<ICmsDbContext> dbContextProvider
            )
            : base(dbContextProvider)
        {
        }

        public async Task<Field> FindByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync()).AsNoTracking().FirstOrDefaultAsync(s => s.Name == name, GetCancellationToken(cancellationToken));
        }

        public async Task<int> GetCountAsync(Guid? groupId, string filter, CancellationToken cancellationToken = default)
        {
            return await (await GetQueryableAsync(groupId, filter)).AsNoTracking()
                .CountAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<List<Field>> GetListAsync(Guid? groupId, string filter, int maxResultCount = int.MaxValue, int skipCount = 0, string sorting = null, CancellationToken cancellationToken = default)
        {
            return await (await GetQueryableAsync(groupId, filter)).AsNoTracking()
                .OrderBy(sorting.IsNullOrEmpty() ? $"{nameof(Field.CreationTime)} asc" : sorting)                
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<List<Field>> GetListAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync()).AsNoTracking().Where(f => ids.Contains(f.Id))
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<bool> NameExistsAsync(string name, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync()).AsNoTracking()
                .AnyAsync(f => f.Name == name, GetCancellationToken(cancellationToken));
        }

        protected virtual async Task<IQueryable<Field>> GetQueryableAsync(
             Guid? groupId, string filter)
        {
            return (await GetDbSetAsync())
                .WhereIf(groupId.HasValue, f => f.GroupId == groupId.Value)
                .WhereIf(!filter.IsNullOrEmpty(), f => f.Name.Contains(filter) || f.DisplayName.Contains(filter));
        }
    }
}
