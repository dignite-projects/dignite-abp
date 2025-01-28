using Dignite.Abp.Data;
using Dignite.Abp.DynamicForms;
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

namespace Dignite.Cms.Entries
{
    public class EfCoreEntryRepository : EfCoreRepository<ICmsDbContext, Entry,Guid>, IEntryRepository
    {
        private readonly IFormControlSelector _formControlSelector;
        private readonly IEnumerable<IFieldQuerying> _fieldQueryings;

        public EfCoreEntryRepository(
            IFormControlSelector formControlSelector,
            IEnumerable<IFieldQuerying> fieldQueryings,
            IDbContextProvider<ICmsDbContext> dbContextProvider
            )
            : base(dbContextProvider)
        {
            _formControlSelector = formControlSelector;
            _fieldQueryings = fieldQueryings;
        }


        public async Task<bool> SlugExistsAsync(string culture,Guid sectionId,  string slug, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                       .AnyAsync(e =>e.Culture==culture &&  e.SectionId== sectionId && e.Slug == slug && e.IsActivatedVersion, GetCancellationToken(cancellationToken));
        }

        public async Task<bool> CultureExistWithSingleSectionAsync(string culture, Guid sectionId, Guid entryTypeId)
        {
            return await (await GetQueryableAsync(culture, sectionId, entryTypeId)).AnyAsync();
        }

        public async Task<List<Entry>> GetListAsync(
            string culture,
            Guid sectionId,
            Guid? entryTypeId = null,
            Guid? creatorId = null, 
            EntryStatus? status = null,
            string filter = null,
            DateTime? start = null,
            DateTime? end = null,
            IList<QueryingByField> queryingByCustomFields = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            string sorting = null,
            CancellationToken cancellationToken = default)
        {
            if (queryingByCustomFields == null || !queryingByCustomFields.Any())
            {
                return await (await GetQueryableAsync(culture, sectionId, entryTypeId, creatorId, status, filter, start, end))
                    .OrderBy(sorting.IsNullOrEmpty() ? $"{nameof(Entry.PublishTime)} desc" : sorting)
                    .PageBy(skipCount, maxResultCount)
                    .ToListAsync(GetCancellationToken(cancellationToken));
            }
            else
            {                
                var enumerable = (await GetQueryableAsync(culture, sectionId, entryTypeId, creatorId, status, filter, start, end))
                    .OrderByDescending(e => e.PublishTime).AsEnumerable<Entry>();
                enumerable = await QueryingByFields(enumerable, queryingByCustomFields);

                return enumerable.Skip(skipCount).Take(maxResultCount).ToList();
            }

        }

        public async Task<int> GetCountAsync(
            string culture,
            Guid sectionId,
            Guid? entryTypeId = null,
            Guid? creatorId = null,
            EntryStatus? status = null,
            string filter = null,
            DateTime? start = null,
            DateTime? end = null,
            IList<QueryingByField> queryingByCustomFields = null,
            CancellationToken cancellationToken = default
            )
        {
            if (queryingByCustomFields == null || !queryingByCustomFields.Any())
            {
                return await (await GetQueryableAsync(culture, sectionId, entryTypeId, creatorId, status, filter, start, end))
                .CountAsync(GetCancellationToken(cancellationToken));
            }
            else
            {
                var enumerable = (await GetQueryableAsync(culture, sectionId, entryTypeId, creatorId, status, filter, start, end))
                    .AsEnumerable<Entry>();
                enumerable = await QueryingByFields(enumerable, queryingByCustomFields);

                return enumerable.Count();
            }
        }

        public async Task<List<Entry>> GetListAsync(Guid sectionId, IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .Where(e => e.SectionId == sectionId && ids.Contains(e.Id))
                .ToListAsync(GetCancellationToken(cancellationToken));
        }


        /// <summary>
        /// Get a list of revisions
        /// </summary>
        /// <param name="initialVersionId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<Entry>> GetVisionListAsync(Guid initialVersionId, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .Where(e => e.InitialVersionId == initialVersionId || e.Id == initialVersionId)
                .OrderByDescending(e => e.CreationTime)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }


        public async Task<Entry> FindBySlugAsync(string culture,Guid sectionId,  string slug, bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .FirstOrDefaultAsync(e => e.SectionId == sectionId && e.Culture==culture && e.Status== EntryStatus.Published && e.Slug == slug && e.IsActivatedVersion, GetCancellationToken(cancellationToken));
        }

        public async Task<Entry> FindPrevAsync(Guid id, bool includeDetails = false, CancellationToken cancellationToken = default)
        {
            var dbSet = await GetDbSetAsync();
            var currentEntry = await dbSet.FirstAsync(e => e.Id == id, GetCancellationToken(cancellationToken));
            return await dbSet
                    .Where(e => e.SectionId == currentEntry.SectionId && e.Culture==currentEntry.Culture && e.PublishTime < currentEntry.PublishTime && e.Status == EntryStatus.Published && e.IsActivatedVersion)
                    .OrderByDescending(e => e.CreationTime)
                    .FirstOrDefaultAsync(GetCancellationToken(cancellationToken));           

        }

        public async Task<Entry> FindNextAsync(Guid id, bool includeDetails = false, CancellationToken cancellationToken = default)
        {
            var dbSet = await GetDbSetAsync();
            var currentEntry = await dbSet.FirstAsync(e => e.Id == id, GetCancellationToken(cancellationToken));
            return await dbSet
                    .Where(e => e.SectionId == currentEntry.SectionId && e.Culture == currentEntry.Culture && e.PublishTime > currentEntry.PublishTime && e.Status == EntryStatus.Published && e.IsActivatedVersion)
                    .OrderBy(e => e.CreationTime)
                    .FirstOrDefaultAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<int> GetMaxOrderAsync(string culture, Guid sectionId, Guid? parentId, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync()).Where(e => e.SectionId == sectionId && e.Culture == culture && e.ParentId==parentId)
                .MaxAsync(e=>(int?)e.Order, GetCancellationToken(cancellationToken))??0;
        }

        public override async Task<IQueryable<Entry>> WithDetailsAsync()
        {
            return (await GetQueryableAsync()).IncludeDetails();
        }


        protected virtual async Task<IQueryable<Entry>> GetQueryableAsync(
            string culture,
            Guid sectionId,
            Guid? entryTypeId = null,
            Guid? creatorId = null,
            EntryStatus? status = null,
            string filter = null,
            DateTime? start = null,
            DateTime? end = null)
        {
            return (await GetDbSetAsync()).Where(e =>e.Culture == culture &&  e.SectionId == sectionId && e.IsActivatedVersion)
                .WhereIf(entryTypeId.HasValue, e => e.EntryTypeId == entryTypeId.Value)
                .WhereIf(creatorId.HasValue, e => e.CreatorId == creatorId.Value)
                .WhereIf(status.HasValue, e => e.Status == status.Value)
                .WhereIf(!filter.IsNullOrEmpty(), e => e.Title.Contains(filter))
                .WhereIf(start.HasValue, e => e.PublishTime>=start.Value)
                .WhereIf(end.HasValue, e => e.PublishTime<end.Value);
        }

        protected virtual async Task<IEnumerable<Entry>> QueryingByFields(IEnumerable<Entry> source, IList<QueryingByField> queryingByCustomFields)
        {
            var dbContext = await GetDbContextAsync();
            var fields = await dbContext.Fields
                .Where(f => queryingByCustomFields.Select(p => p.Name).Contains(f.Name))
                .ToListAsync();

            foreach (var param in queryingByCustomFields)
            {
                foreach (var querying in _fieldQueryings)
                {
                    var field = fields.FirstOrDefault(f => f.Name == param.Name);
                    if (field == null)
                        continue;

                    var form = _formControlSelector.Get(field.FormControlName);
                    if (form.GetType() == querying.FormControlType)
                    {
                        source = querying.Query(source, param);
                        continue;
                    }
                }
            }

            return source;
        }
    }
}
