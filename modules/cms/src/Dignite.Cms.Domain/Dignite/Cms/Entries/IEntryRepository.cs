using Dignite.Abp.Data;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Dignite.Cms.Entries
{
    public interface IEntryRepository : IBasicRepository<Entry, Guid>
    {
        Task<List<Entry>> GetLocalizedEntriesBySlugAsync(Guid sectionId, string slug, CancellationToken cancellationToken = default);

        Task<bool> SlugExistsAsync(string culture, Guid sectionId, string slug, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method is only needed if the section type is a single type
        /// </summary>
        /// <param name="culture"></param>
        /// <param name="sectionId"></param>
        /// <param name="entryTypeId"></param>
        /// <returns></returns>
        Task<bool> CultureExistWithSingleSectionAsync(string culture, Guid sectionId, Guid entryTypeId);


        Task<List<Entry>> GetListAsync(
            string culture,
            Guid sectionId,
            Guid? entryTypeId=null,
            Guid? creatorId = null,
            EntryStatus? status = null,
            string filter = null,
            DateTime? start = null,
            DateTime? end = null,
            IList<QueryingByField> queryingByCustomFields = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            string sorting = null,
            CancellationToken cancellationToken = default);
        Task<int> GetCountAsync(
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
            );

        Task<List<Entry>> GetListAsync(Guid sectionId, IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get a list of revisions
        /// </summary>
        /// <param name="initialVersionId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<Entry>> GetVisionListAsync(Guid initialVersionId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Find entries using slug
        /// </summary>
        /// <param name="culture"></param>
        /// <param name="sectionId"></param>
        /// <param name="slug"></param>
        /// <param name="includeDetails"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>
        /// This method gets the active revision
        /// </returns>
        Task<Entry> FindBySlugAsync(string culture,Guid sectionId,  string slug, bool includeDetails = true, CancellationToken cancellationToken = default);

        Task<Entry> FindPrevAsync(Guid id, bool includeDetails = false, CancellationToken cancellationToken = default);

        Task<Entry> FindNextAsync(Guid id, bool includeDetails = false, CancellationToken cancellationToken = default);

        Task<int> GetMaxOrderAsync( string culture,Guid sectionId, Guid? parentId, CancellationToken cancellationToken = default);
    }
}
