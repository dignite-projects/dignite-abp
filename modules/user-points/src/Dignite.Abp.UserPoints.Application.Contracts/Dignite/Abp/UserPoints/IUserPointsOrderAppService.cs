using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Dignite.Abp.UserPoints;

public interface IUserPointsOrderAppService : IApplicationService
{
    Task<PagedResultDto<UserPointsOrderDto>> GetMyOrdersAsync(GetMyOrdersInput input);
}
