using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Dignite.CmsKit.Favourites;

public interface IFavouriteRepository : IBasicRepository<Favourite, Guid>
{
    Task<Favourite> GetCurrentUserAsync(
        [NotNull] string entityType,
        [NotNull] string entityId,
        Guid userId,
        CancellationToken cancellationToken = default
    );
    Task<List<Favourite>> GetListByUserAsync(
        [NotNull] string entityType,
        Guid userId,
        CancellationToken cancellationToken = default
    );
}
