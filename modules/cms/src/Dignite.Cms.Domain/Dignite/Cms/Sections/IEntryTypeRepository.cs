using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Dignite.Cms.Sections
{
    public interface IEntryTypeRepository : IBasicRepository<EntryType, Guid>
    {
        Task<bool> NameExistsAsync(Guid sectionId, string name, CancellationToken cancellationToken = default);
    }
}
