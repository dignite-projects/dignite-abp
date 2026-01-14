using Dignite.Cms.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.Cms.Sections
{
    public class EfCoreSectionRepository : EfCoreRepository<ICmsDbContext, Section,Guid>, ISectionRepository
    {

        public EfCoreSectionRepository(
            IDbContextProvider<ICmsDbContext> dbContextProvider
            )
            : base(dbContextProvider)
        {
        }

        public async Task<Section> GetDefaultAsync(bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            return await(await GetQueryableAsync()).AsNoTracking()
                .IncludeDetails(includeDetails)
                .FirstOrDefaultAsync(s => s.IsActive && s.IsDefault && s.Type== SectionType.Single, GetCancellationToken(cancellationToken));
        }

        public async Task<bool> NameExistsAsync(string name, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync()).AsNoTracking()
                       .AnyAsync(s => s.Name == name, GetCancellationToken(cancellationToken));
        }

        public async Task<bool> RouteExistsAsync(string route, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync()).AsNoTracking()
                       .AnyAsync(s => s.Route == route, GetCancellationToken(cancellationToken));
        }

        public async Task<Section> FindByNameAsync( string name, bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            return await ((await GetQueryableAsync()).AsNoTracking()
                .IncludeDetails(includeDetails))
                .FirstOrDefaultAsync(s => s.Name == name, GetCancellationToken(cancellationToken));
        }
        public async Task<int> GetCountAsync(
            string filter = null,
            bool? isActive = null,
            CancellationToken cancellationToken = default
            )
        {
            return await (await GetQueryableAsync(filter, isActive)).CountAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<List<Section>> GetListAsync(
            string filter = null,
            bool? isActive = null, 
            bool includeDetails = true,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            string sorting = null,
            CancellationToken cancellationToken = default)
        {
            return await (await GetQueryableAsync( filter, isActive)).AsNoTracking()
                .IncludeDetails(includeDetails)
                .OrderBy(sorting.IsNullOrEmpty() ? $"{nameof(Section.CreationTime)} asc" : sorting)
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }


        public override async Task<IQueryable<Section>> WithDetailsAsync()
        {
            return (await GetQueryableAsync()).IncludeDetails();
        }

        protected virtual async Task< IQueryable<Section>> GetQueryableAsync(
            string filter = null,
            bool? isActive = null)
        {
            return (await GetDbSetAsync())
                .WhereIf(!filter.IsNullOrEmpty(), et => et.DisplayName.Contains(filter))
                .WhereIf(isActive.HasValue, s => s.IsActive == isActive);
        }
    }
}
