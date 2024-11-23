using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.CmsKit.MarkedItems;

namespace Dignite.CmsKit.Public.MarkedItems;
public class MarkedItemPublicAppService : CmsKitPublicAppServiceBase, IMarkedItemPublicAppService
{
    protected IUserMarkedItemRepository UserMarkedItemRepository { get; }

    public MarkedItemPublicAppService(IUserMarkedItemRepository userMarkedItemRepository)
    {
        UserMarkedItemRepository = userMarkedItemRepository;
    }

    [Authorize]
    public async Task<ListResultDto<string>> GetListForUserAsync([NotNull] string entityType)
    {
        var result = await UserMarkedItemRepository.GetListForUserAsync(CurrentUser.Id.Value, entityType);
        return new ListResultDto<string> (
            result.OrderByDescending(mi=>mi.CreationTime).Select(mi=>mi.EntityId).ToList()
        );
    }
}
