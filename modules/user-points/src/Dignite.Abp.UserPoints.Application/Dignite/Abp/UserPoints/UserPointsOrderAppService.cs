using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;

namespace Dignite.Abp.UserPoints;

public class UserPointsOrderAppService : UserPointsAppService, IUserPointsOrderAppService
{
    private readonly IUserPointsOrderRepository _userPointsOrderRepository;

    public UserPointsOrderAppService(IUserPointsOrderRepository userPointsOrderRepository)
    {
        _userPointsOrderRepository = userPointsOrderRepository;
    }

    [Authorize]
    public async Task<PagedResultDto<UserPointsOrderDto>> GetMyOrdersAsync(GetMyOrdersInput input)
    {
        var count = await _userPointsOrderRepository.GetCountAsync(
            CurrentUser.Id.Value,
            input.StartTime,
            input.EndTime);
        var result = await _userPointsOrderRepository.GetListAsync(
            CurrentUser.Id.Value,
            input.StartTime,
            input.EndTime,
            input.MaxResultCount,
            input.SkipCount);

        return new PagedResultDto<UserPointsOrderDto>(
            count, 
            ObjectMapper.Map<List<UserPointsOrder>, List<UserPointsOrderDto>>(result)
            );
    }
}
