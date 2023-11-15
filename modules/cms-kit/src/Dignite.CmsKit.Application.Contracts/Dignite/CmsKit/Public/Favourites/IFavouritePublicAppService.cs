using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Dignite.CmsKit.Public.Favourites;

public interface IFavouritePublicAppService : IApplicationService
{
    Task<FavouriteDto> CreateAsync(string entityType, string entityId);

    Task DeleteAsync(string entityType, string entityId);
}
