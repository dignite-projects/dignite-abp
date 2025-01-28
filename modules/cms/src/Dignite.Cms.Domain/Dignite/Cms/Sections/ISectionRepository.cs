using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Dignite.Cms.Sections
{
    public interface ISectionRepository : IBasicRepository<Section, Guid>
    {
        Task<bool> NameExistsAsync(string name, CancellationToken cancellationToken = default);
        Task<bool> RouteExistsAsync(string route, CancellationToken cancellationToken = default);

        Task<Section> FindByNameAsync(string name, bool includeDetails = true, CancellationToken cancellationToken = default);

        Task<Section> GetDefaultAsync(bool includeDetails = true, CancellationToken cancellationToken = default);

        Task<int> GetCountAsync(
            string filter = null,
            bool? isActive = null,
            CancellationToken cancellationToken = default
            );
        Task<List<Section>> GetListAsync(
            string filter = null,
            bool? isActive= null,
            bool includeDetails = true,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            string sorting = null,
            CancellationToken cancellationToken = default);
    }
}
