using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Dignite.Publisher.Categories;
public interface ICategoryRepository : IBasicRepository<Category, Guid>
{
    Task<List<Category>> GetListAsync(
        string? local,
        CancellationToken cancellationToken = default);

    Task<bool> NameExistsAsync(Guid? parentId, string name, CancellationToken cancellationToken = default);
}
