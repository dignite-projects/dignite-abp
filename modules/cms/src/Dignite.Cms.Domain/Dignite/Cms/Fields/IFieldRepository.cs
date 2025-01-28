using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Dignite.Cms.Fields
{
    public interface IFieldRepository : IBasicRepository<Field, Guid>
    {
        Task<bool> NameExistsAsync(string name, CancellationToken cancellationToken = default);

        Task<Field> FindByNameAsync(string name, CancellationToken cancellationToken = default);

        Task<List<Field>> GetListAsync(Guid? groupId, string filter,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            string sorting = null,
            CancellationToken cancellationToken = default);
        Task<int> GetCountAsync(
             Guid? groupId, string filter,
             CancellationToken cancellationToken = default
            );

        Task<List<Field>> GetListAsync(IEnumerable<Guid> ids,
            CancellationToken cancellationToken = default);
    }
}
