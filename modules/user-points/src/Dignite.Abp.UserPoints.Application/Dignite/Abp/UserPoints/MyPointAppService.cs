using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;

namespace Dignite.Abp.UserPoints;

public class MyPointAppService : UserPointsAppService, IMyPointAppService
{
    private readonly IUserPointRepository _userPointRepository;

    public MyPointAppService(IUserPointRepository userPointsRepository)
    {
        _userPointRepository = userPointsRepository;
    }

    /// <summary>
    /// Asynchronously retrieves the current available point balance for the authenticated user.
    /// </summary>
    /// <remarks>This method requires the caller to be authenticated. 
    /// The returned balance reflects any recent calibrations or adjustments to the user's point total.</remarks>
    /// <returns>A task that represents the asynchronous operation. The task result contains the available point balance for the
    /// current user.</returns>
    [Authorize]
    public async Task<int> GetAvailableAsync()
    {
        var userPoint = await _userPointRepository.CalibrateBalanceAsync(CurrentUser.Id.Value);
        return userPoint == null ? 0 : userPoint.Balance;
    }

    /// <summary>
    /// Retrieves a paged list of user points that match the specified filter criteria.
    /// </summary>
    /// <param name="input">An object containing the filter and paging options to apply when retrieving user points. Must not be null.</param>
    /// <returns>A <see cref="PagedResultDto{UserPointDto}"/> containing the total count and a list of user points that match the
    /// specified criteria. The list may be empty if no user points are found.</returns>
    [Authorize]
    public async Task<PagedResultDto<UserPointDto>> GetListAsync(GetUserPointListInput input)
    {
        var count = await _userPointRepository.GetCountAsync(
            CurrentUser.Id.Value,
            pointType : input.PointType,
            entityType : input.EntityType,
            entityId : input.EntityId
            );
        var result = await _userPointRepository.GetListAsync(
            CurrentUser.Id.Value,
            pointType: input.PointType,
            entityType: input.EntityType,
            entityId: input.EntityId,
            skipCount : input.SkipCount,
            maxResultCount : input.MaxResultCount
            );

        return new PagedResultDto<UserPointDto>(
            count, 
            ObjectMapper.Map<List<UserPoint>, List<UserPointDto>>(result)
            );
    }
}
