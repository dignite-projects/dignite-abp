using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Dignite.Abp.UserPoints;

public interface IUserPointsItemAppService : IApplicationService
{
    /// <summary>
    /// Get my points breakdown
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<PagedResultDto<UserPointsItemDto>> GetListAsync(GetUserPointsItemsInput input);

    /// <summary>
    /// Calculate the number of points that have expired within a specified time period
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<int> GetTotalPointsAsync(GetUserTotalPointsInput input);
}
