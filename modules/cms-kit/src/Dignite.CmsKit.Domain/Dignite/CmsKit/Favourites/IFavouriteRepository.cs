using JetBrains.Annotations;
using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Dignite.CmsKit.Favourites;

public interface IFavouriteRepository : IBasicRepository<Favourite, Guid>
{
    Task<Favourite> GetCurrentUserFavouriteAsync(
        [NotNull] string entityType,
        [NotNull] string entityId,
        Guid userId,
        CancellationToken cancellationToken = default
    );
}
